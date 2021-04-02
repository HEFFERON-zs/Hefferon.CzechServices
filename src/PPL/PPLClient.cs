using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefferon.CzechServices.PPL
{
    public class PPLClient
    {
        public PPLClient()
        {

        }

        public List<PPLServiceReference.KTMDetail> GetAllPickUpPlaces()
        {
            PPLServiceReference.KtmSoapClient client = new PPLServiceReference.KtmSoapClient(new PPLServiceReference.KtmSoapClient.EndpointConfiguration());
            var result = client.GetKTMListAsync("CZ").Result.ToList();
            return result;
        }
    }
}
