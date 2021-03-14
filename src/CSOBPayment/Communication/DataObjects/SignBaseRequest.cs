using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hefferon.CzechServices.CsobPayment.Communication.DataObjects
{
    public abstract class SignBaseRequest : SignBase, IBaseRequest
    {
        [JsonProperty("merchantId")]
        public string MerchantId { get; set; }

        public void FillDateTime()
        {
            this.DateTime = System.DateTime.Now.ToString(DTTM_FORMAT);
        }
    }
}
