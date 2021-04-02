using Hefferon.CzechServices.CNB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hefferon.CzechServices.Test
{
    [TestClass]
    public class PPLClientTest
    {

        [TestMethod]
        public void GetPickupPlacesTest()
        {
            var client = new Hefferon.CzechServices.PPL.PPLClient();
            var result = client.GetAllPickUpPlaces();
            Assert.IsTrue(result.Any());
        }
    }
}
