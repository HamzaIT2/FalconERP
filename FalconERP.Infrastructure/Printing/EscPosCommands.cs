using System.Text;

namespace FalconERP.Infrastructure.Printing;

public static class EscPosCommands
{
    public static readonly byte[] Initialize =
    {
        0x1B, 0x40
    };

    public static readonly byte[] AlignLeft =
    {
        0x1B, 0x61, 0
    };

    public static readonly byte[] AlignCenter =
    {
        0x1B, 0x61, 1
    };

    public static readonly byte[] AlignRight =
    {
        0x1B, 0x61, 2
    };

    public static readonly byte[] BoldOn =
    {
        0x1B, 0x45, 1
    };

    public static readonly byte[] BoldOff =
    {
        0x1B, 0x45, 0
    };

    public static readonly byte[] DoubleHeightWidth =
    {
        0x1D, 0x21, 0x11
    };

    public static readonly byte[] NormalSize =
    {
        0x1D, 0x21, 0x00
    };

    public static readonly byte[] FeedLines =
    {
        0x1B, 0x64, 3
    };

    public static readonly byte[] CutPaper =
    {
        0x1D, 0x56, 0x41, 0x10
    };

    public static byte[] Text(string value)
    {
        return Encoding.UTF8.GetBytes(value + Environment.NewLine);
    }
}