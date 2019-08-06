using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toras.UI;
using System.Windows.Forms;
using System.Windows.Input;

namespace Toras.Core
{
    public static class KeyModifier
    {

        public static int GetModifier()
        {
            int activeModifier = -1;

            if (NoModifier())
                activeModifier = 0; // No Modifier

            else if (ShiftModifier())
                activeModifier = 1; // Shift Modifier

            else if (CtrlModifier()) 
                activeModifier = 2; // Ctrl Modifier

            else if (AltModifier())
                activeModifier = 3; // Alt Modifier

            else if (FtpModifier())
                activeModifier = 4; // Alt Modifier

            return activeModifier;
        }

        private static bool NoModifier()
        {
            return !(ShiftModifier() || CtrlModifier() || AltModifier() || FtpModifier());
        }

        private static bool ShiftModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
        }

        private static bool CtrlModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
        }

        private static bool AltModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt);
        }

        private static bool FtpModifier()
        {
            return Keyboard.IsKeyDown(Key.F);
        }
    }
}
