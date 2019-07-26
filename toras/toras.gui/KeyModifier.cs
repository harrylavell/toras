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
        public static bool NoModifier()
        {
            return !(ShiftModifier() || CtrlModifier() || AltModifier());
        }

        public static bool ShiftModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
        }

        public static bool CtrlModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
        }

        public static bool AltModifier()
        {
            return Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt);
        }
    }
}
