using Hefferon.CzechServices.CNB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hefferon.CzechServices.Test
{
    [TestClass]
    public class CNBExchangeRateClientTest
    {
        [TestMethod]
        public void GetExchangeRatesTest()
        {
            CNBExchangeRateClient cnbClient = new CNBExchangeRateClient();

            var result = cnbClient.GetExchangeRates();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void DownloadDataTest()
        {
            CNBExchangeRateClient cnbClient = new CNBExchangeRateClient();

            string result = cnbClient.DownloadData();

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void ParseTxtTest()
        {
            CNBExchangeRateClient cnbClient = new CNBExchangeRateClient();

            string data = @"26.02.2021 #40
zemì|mìna|množství|kód|kurz
Austrálie|dolar|1|AUD|16,784
Brazílie|real|1|BRL|3,929
Bulharsko|lev|1|BGN|13,391
Èína|žen-min-pi|1|CNY|3,345
Dánsko|koruna|1|DKK|3,523
EMU|euro|1|EUR|26,195
Filipíny|peso|100|PHP|44,332
Hongkong|dolar|1|HKD|2,787
Chorvatsko|kuna|1|HRK|3,456
Indie|rupie|100|INR|29,244
Indonesie|rupie|1000|IDR|1,516
Island|koruna|100|ISK|17,132
Izrael|nový šekel|1|ILS|6,537
Japonsko|jen|100|JPY|20,331
Jižní Afrika|rand|1|ZAR|1,447
Kanada|dolar|1|CAD|17,087
Korejská republika|won|100|KRW|1,916
Maïarsko|forint|100|HUF|7,248
Malajsie|ringgit|1|MYR|5,336
Mexiko|peso|1|MXN|1,036
MMF|ZPÈ|1|XDR|31,265
Norsko|koruna|1|NOK|2,518
Nový Zéland|dolar|1|NZD|15,755
Polsko|zlotý|1|PLN|5,797
Rumunsko|leu|1|RON|5,373
Rusko|rubl|100|RUB|28,889
Singapur|dolar|1|SGD|16,265
Švédsko|koruna|1|SEK|2,583
Švýcarsko|frank|1|CHF|23,841
Thajsko|baht|100|THB|71,184
Turecko|lira|1|TRY|2,905
USA|dolar|1|USD|21,612
Velká Británie|libra|1|GBP|30,091
";

            var result = cnbClient.ParseTxt(data);

            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.First().Code == "AUD");
            Assert.IsTrue(result.First().Rate == 16.784);
            Assert.IsTrue(result.First().Created.Day == 26);
            Assert.IsTrue(result.First().Created.Month == 2);
            Assert.IsTrue(result.First().Created.Year == 2021);
        }
    }
}
