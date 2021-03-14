using System;
using System.IO;

namespace Hefferon.CzechServices.CsobPayment.Common
{
    public static class Constants
    {
        public static readonly string PaymentInitBaseFilePath = Path.Combine(Environment.CurrentDirectory, @"Assets", "payment-init-base.json");
        public static readonly string PaymentOneclickBaseFilePath = Path.Combine(Environment.CurrentDirectory, @"Assets", "payment-oneclick-base.json");
        public const string GatewayUrl = "https://iapi.iplatebnibrana.csob.cz/api/v1.8";
    }
}
