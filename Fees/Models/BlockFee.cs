using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fees.Models
{
    public class BlockFee
    {
        public int NumBlock { get; set; }
        public float SatoshiPerByte { get; set; }
        public string Btc { get; set; }
        public string BtcSegwit { get; set; }
        public string Dollar { get; set; }
        public string DollarSegwit { get; set; }
        public string Nis { get; set; }
        public string NisSegwit { get; set; }

    }
}
