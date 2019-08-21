using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toras.UI;
using System.Windows.Forms;
using System.Windows.Input;

using Toras.Utilities;

namespace Toras.Core
{
    public static class KeyModifier
    {

        /* Used to get the current key modifiers being held.
         * @return activeModifier, the current modifier */
        public static int GetModifier()
        {
            int activeModifier = -1;

            if (NoModifier())
                activeModifier = 0; // No Modifier

            else if (ShiftModifier() && !CtrlModifier())
                activeModifier = 1; // Shift Modifier

            else if (CtrlModifier() && !ShiftModifier())
                activeModifier = 2; // Ctrl Modifier

            else if (ShiftModifier() && CtrlModifier())
                activeModifier = 4; // Ctrl + Shift Modifier

            return activeModifier;
        }

        /* If no modifiers are being held.
         * @return true, no modifiers active */
        private static bool NoModifier()
        {
            return !(ShiftModifier() || CtrlModifier());
        }

        /* If shift modifiers are being held.
         * @return true, shift modifiers active */
        private static bool ShiftModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) && !CtrlModifier();
        }

        /* If ctrl modifiers are being held.
         * @return true, ctrl modifiers active */
        private static bool CtrlModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) && !ShiftModifier();
        }

    }
}
