using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmoniousYJSWS
{
    static class StringUtils
    {
        public static string Format(this string self,params object[] argus)
        {
            return string.Format(self, argus);
        }
    }
}
