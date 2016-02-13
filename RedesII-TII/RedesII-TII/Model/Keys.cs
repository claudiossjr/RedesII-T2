using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedesII_TII.Model
{
    public class PublicKey
    {
        public int n { get; set; }
        public int d { get; set; }
    }

    public class PrivateKey
    {
        public int n { get; set; }
        public int e { get; set; }
    }

}
