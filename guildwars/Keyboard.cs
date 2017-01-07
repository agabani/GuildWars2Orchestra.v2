using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using guildwars.Extern;

namespace guildwars
{
    public class Keyboard
    {
        private static readonly Dictionary<int, ScanCodeShort> ScanCodeShorts = new Dictionary<int, ScanCodeShort>
        {
            {1, ScanCodeShort.KEY_1},
            {2, ScanCodeShort.KEY_2},
            {3, ScanCodeShort.KEY_3},
            {4, ScanCodeShort.KEY_4},
            {5, ScanCodeShort.KEY_5},
            {6, ScanCodeShort.KEY_6},
            {7, ScanCodeShort.KEY_7},
            {8, ScanCodeShort.KEY_8},
            {9, ScanCodeShort.KEY_9},
            {0, ScanCodeShort.KEY_0}
        };

        private static readonly Dictionary<int, VirtualKeyShort> VirtualKeyShorts = new Dictionary<int, VirtualKeyShort>
        {
            {1, VirtualKeyShort.KEY_1},
            {2, VirtualKeyShort.KEY_2},
            {3, VirtualKeyShort.KEY_3},
            {4, VirtualKeyShort.KEY_4},
            {5, VirtualKeyShort.KEY_5},
            {6, VirtualKeyShort.KEY_6},
            {7, VirtualKeyShort.KEY_7},
            {8, VirtualKeyShort.KEY_8},
            {9, VirtualKeyShort.KEY_9},
            {0, VirtualKeyShort.KEY_0}
        };

        public Keyboard()
        {
            var mainWindowHandle = Process.GetProcesses()
                .First(
                    p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                         p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase)).MainWindowHandle;

//            var mainWindowHandle = Process.GetProcesses()
//                .First(
//                    p => p.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase)).MainWindowHandle;

            PInvoke.SetForegroundWindow(mainWindowHandle);
        }

        public void Press(int key)
        {
            var nInputs = new[]
            {
                new Input
                {
                    type = InputType.KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KeybdInput
                        {
                            wScan = ScanCodeShorts[key],
                            wVk = VirtualKeyShorts[key]
                        }
                    }
                }
            };

            PInvoke.SendInput((uint) nInputs.Length, nInputs, Input.Size);
        }

        public void Release(int key)
        {
            var nInputs = new[]
            {
                new Input
                {
                    type = InputType.KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KeybdInput
                        {
                            wScan = ScanCodeShorts[key],
                            wVk = VirtualKeyShorts[key],
                            dwFlags = KeyEventF.KEYUP
                        }
                    }
                }
            };

            PInvoke.SendInput((uint)nInputs.Length, nInputs, Input.Size);
        }
    }
}