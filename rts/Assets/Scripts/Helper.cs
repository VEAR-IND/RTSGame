using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
  internal static class Helper
    {
        internal static string MakeColor(int stat)
        {
            string temp = "";
            if (stat > 0)
            {
                temp += string.Format("<color=#62ed05>+{0}</color>", stat);
            }
            return temp;
        }
    }
}
