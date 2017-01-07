using System.Runtime.InteropServices;

namespace guildwars.Extern
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct HardwareInput
    {
        internal int uMsg;
        internal short wParamL;
        internal short wParamH;
    }
}