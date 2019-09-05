using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fees.Models
{
    public class BitCoinFee
    {
        [JsonProperty("fastestFee")]
        public int FastestFee { get; set; }


        [JsonProperty("halfHourFee")]
        public int HalfHourFee { get; set; }


        [JsonProperty("hourFee")]
        public int HourFee { get; set; }
    }
}
