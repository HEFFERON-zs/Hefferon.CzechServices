using Newtonsoft.Json;

namespace Hefferon.CzechServices.CsobPayment.Communication.DataObjects
{
    public abstract class SignBase : ApiBase
    {
        [JsonProperty("dttm")]
        public string DateTime { get; set; }
        
        [JsonProperty("signature")]
        public string Signature { get; set; }

        public abstract string ToSign();
    }
}
