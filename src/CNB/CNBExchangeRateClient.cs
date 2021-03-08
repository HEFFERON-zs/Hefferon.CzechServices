using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Hefferon.CzechServices.CNB
{
    public class CNBExchangeRateClient
    {
        public List<CNBExchangeRate> GetExchangeRates()
        {
            string data = DownloadData();

            var result = new List<CNBExchangeRate>();

            if (!string.IsNullOrEmpty(data))
            {
                result = ParseTxt(data);
            }
            return result;
        }

        public string DownloadData()
        {
            WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
            string data = string.Empty;

            try
            {
                data = webClient.DownloadString("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt");
            }
            finally
            {
                webClient.Dispose();
            }

            return data;
        }

        public List<CNBExchangeRate> ParseTxt(string data)
        {
            var lines = data.Split(new[] { '\r', '\n' }).ToList();
            var result = new List<CNBExchangeRate>();
            var date = DateTime.Parse(lines[0].Split(' ')[0].Trim());
            lines = lines.Where(p => !string.IsNullOrEmpty(p)).ToList();
            foreach (var line in lines.Skip(2))
            {
                var values = line.Split('|');
                var rate = new CNBExchangeRate();
                rate.Country = values[0];
                rate.Currency = values[1];
                rate.Amount = double.Parse(values[2]);
                rate.Code = values[3];
                rate.Rate = double.Parse(values[4]);
                rate.Created = date;
                result.Add(rate);
            }
            return result;
        }
    }
}
