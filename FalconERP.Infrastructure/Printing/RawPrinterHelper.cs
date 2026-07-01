using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FalconERP.Infrastructure.Printing;

public static class RawPrinterHelper
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private class DOCINFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDocName = string.Empty;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pOutputFile = string.Empty;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDataType = "RAW";
    }

    [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", SetLastError = true)]
    private static extern bool OpenPrinter(
        string printerName,
        out IntPtr printerHandle,
        IntPtr defaults);

    [DllImport("winspool.drv", SetLastError = true)]
    private static extern bool ClosePrinter(
        IntPtr printerHandle);

    [DllImport("winspool.drv", SetLastError = true)]
    private static extern bool StartDocPrinter(
        IntPtr printerHandle,
        int level,
        [In] DOCINFO docInfo);

    [DllImport("winspool.drv", SetLastError = true)]
    private static extern bool EndDocPrinter(
        IntPtr printerHandle);

    [DllImport("winspool.drv", SetLastError = true)]
    private static extern bool StartPagePrinter(
        IntPtr printerHandle);

    [DllImport("winspool.drv", SetLastError = true)]
    private static extern bool EndPagePrinter(
        IntPtr printerHandle);

    [DllImport("winspool.drv", SetLastError = true)]
    private static extern bool WritePrinter(
        IntPtr printerHandle,
        IntPtr buffer,
        int count,
        out int written);

    public static void SendBytes(
        string printerName,
        byte[] bytes)
    {
        if (!OpenPrinter(printerName, out var printerHandle, IntPtr.Zero))
            throw new Win32Exception(Marshal.GetLastWin32Error());

        try
        {
            var doc = new DOCINFO
            {
                pDocName = "Falcon ERP Receipt"
            };

            if (!StartDocPrinter(printerHandle, 1, doc))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            try
            {
                StartPagePrinter(printerHandle);

                IntPtr unmanagedPointer =
                    Marshal.AllocCoTaskMem(bytes.Length);

                try
                {
                    Marshal.Copy(bytes, 0, unmanagedPointer, bytes.Length);

                    WritePrinter(
                        printerHandle,
                        unmanagedPointer,
                        bytes.Length,
                        out _);
                }
                finally
                {
                    Marshal.FreeCoTaskMem(unmanagedPointer);
                }

                EndPagePrinter(printerHandle);
            }
            finally
            {
                EndDocPrinter(printerHandle);
            }
        }
        finally
        {
            ClosePrinter(printerHandle);
        }
    }
}