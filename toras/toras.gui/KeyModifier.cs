using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toras.utilities;
using System.Windows.Forms;
using System.Windows.Input;

namespace toras.utilities
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

            return activeModifier;
        }

        private static bool NoModifier()
        {
            return !(ShiftModifier() || CtrlModifier() || AltModifier());
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
    }
}
