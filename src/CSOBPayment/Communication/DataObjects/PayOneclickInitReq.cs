using System.Text;
using Newtonsoft.Json;

namespace Hefferon.CzechServices.CsobPayment.Communication.DataObjects
{
    public class PayOneclickInitReq : SignBaseRequest
    {
        [JsonProperty("origPayId")]
        public string OrigPayId { get; set; }

        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }

        [JsonProperty("totalAmount")]
        public long TotalAmount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public override string ToSign()
        {
            var sb = new StringBuilder();
            Add(sb, MerchantId);
            Add(sb, OrigPayId);
            Add(sb, OrderNo);
            Add(sb, DateTime);
            Add(sb, TotalAmount);
            Add(sb, Currency);
            Add(sb, Description);
            DeleteLast(sb);
            return sb.ToString();
        }
    }
}
