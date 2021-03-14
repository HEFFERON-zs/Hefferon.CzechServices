using System.Text;
using Newtonsoft.Json;

namespace Hefferon.CzechServices.CsobPayment.Communication.DataObjects
{

    public class EchoRequest : SignBaseRequest
    {
        public override string ToSign()
        {
            var sb = new StringBuilder();
            Add(sb, MerchantId);
            Add(sb, DateTime);
            DeleteLast(sb);
            return sb.ToString();
        }
    }
}
