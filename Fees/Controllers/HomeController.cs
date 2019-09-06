using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fees.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fees.Controllers
{
    public class HomeController : Controller
    {
        private const int SatoshiDivideToBtc = 100000000;

        public async Task<IActionResult> Index()
        {
     
            var httpClient = new HttpClient();
            var blocks = new[] {2, 6, 48};
            var list = new List<BlockFee>();
            foreach (var block in blocks)
            {
                var response = await httpClient.GetAsync($"https://estimatefee.com/n/{block}");
                var results = await response.Content.ReadAsStringAsync();
                var satoshiPerByte = float.Parse(results, CultureInfo.InvariantCulture.NumberFormat);
                var btc = (satoshiPerByte* 100000 * 226)/100000000;
                var btcSegwit = (satoshiPerByte* 100000 * 146)/100000000;
                var btcResponse = await httpClient.GetAsync("https://blockchain.info/ticker");
                var btcResults = await btcResponse.Content.ReadAsAsync<BitCoinFee>();
                var nisResponse = await httpClient.GetAsync("https://api.exchangeratesapi.io/latest");
                var nisResults = await nisResponse.Content.ReadAsAsync<RootObject>();

                list.Add(new BlockFee(){NumBlock = block,
                    SatoshiPerByte = satoshiPerByte,
                    Btc = btc.ToString("F6").TrimEnd('0'),
                    BtcSegwit = btcSegwit.ToString("F6").TrimEnd('0'),
                    DollarSegwit = (btcResults.USD.last*btcSegwit).ToString("F6").TrimEnd('0'),
                    Dollar = (btcResults.USD.last*btc).ToString("F6").TrimEnd('0'),
                    Nis = (nisResults.rates.ILS * btcResults.USD.last * btc).ToString("F6").TrimEnd('0'),
                    NisSegwit = (nisResults.rates.ILS * btcResults.USD.last * btcSegwit).ToString("F6").TrimEnd('0')
                });
            }
            return View(new IndexViewModel(){BlockFees = list});
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}