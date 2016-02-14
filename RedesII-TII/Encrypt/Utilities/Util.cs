using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedesII_TII.Utilities
{
    public class Util
    {
        public static ushort GetExponencial( ushort expBase, int exp, ushort numberToModule )
        {
            uint value  = expBase;

            uint y      = 1;

            // primary case
            if (exp == 0) return 1;

            while (exp > 1)
            {

                // Initial case when exp is odd
                if( exp % 2 != 0 )
                {
                    y   = (y * value) % (uint)numberToModule;
                    exp = exp - 1;
                }

                value   = (value * value) % (uint)numberToModule;
                exp     = exp / 2;

            }

            value = ((value * y) % numberToModule);
            return (ushort)value;
        }    

    }
}
