using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace Hefferon.CzechServices.Ares
{
    /// <summary>
    /// Provides ARES service client functionality using the Standard and Basic ARES services
    /// (for more information see http://wwwinfo.mfcr.cz/ares/ares_xml.html.cz).
    /// </summary>
    public class AresServiceClient
    {

        /// <summary>
        /// Gets ARES informations of the economic subject specified by <paramref name="companyName"/>.
        /// </summary>
        /// <param name="companyName">Company name.</param>
        /// <returns><see cref="AresInfo"/> of the economic subject specified by <paramref name="companyName"/>, if there was found exactly one subject; otherwise empty <see cref="AresInfo"/>.</returns>
        public AresInfo GetInfoByCompanyName(string companyName)
        {
            WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
            string data = string.Empty;

            try
            {
                data = webClient.DownloadString("http://wwwinfo.mfcr.cz/cgi-bin/ares/darv_std.cgi?obchodni_firma=" + companyName + "&max_pocet=10");
            }
            finally
            {
                webClient.Dispose();
                webClient = null;
            }

            if (string.IsNullOrEmpty(data))
                new AresInfo();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(data);

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            xmlNamespaceManager.AddNamespace("are", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/ares/ares_answer/v_1.0.1");
            xmlNamespaceManager.AddNamespace("dtt", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/ares/ares_datatypes/v_1.0.4");
            xmlNamespaceManager.AddNamespace("udt", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/uvis_datatypes/v_1.0.1");
            xmlNamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            XmlNode recordsCountNode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/are:Pocet_zaznamu", xmlNamespaceManager);

            if (recordsCountNode == null)
                return new AresInfo();

            if (!int.TryParse(recordsCountNode.InnerText, out int recordsCount))
                return new AresInfo();

            if (recordsCount == 0 || recordsCount > 1)
                return new AresInfo();

            var crn = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:ICO", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:ICO", xmlNamespaceManager).InnerText;

            if (string.IsNullOrEmpty(crn))
                return new AresInfo();

            return GetInfoByCrn(crn);
        }

        /// <summary>
        /// Gets ARES informations of the economic subject specified by <paramref name="crn"/>.
        /// </summary>
        /// <param name="crn">Company registration number.</param>
        /// <returns><see cref="AresInfo"/> of the economic subject specified by <paramref name="crn"/>, if there was found exactly one subject; otherwise empty <see cref="AresInfo"/>.</returns>
        public AresInfo GetInfoByCrn(string crn)
        {
            WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
            string data = string.Empty;

            try
            {
                data = webClient.DownloadString("http://wwwinfo.mfcr.cz/cgi-bin/ares/darv_bas.cgi?ico=" + crn + "&max_pocet=10");
            }
            finally
            {
                webClient.Dispose();
                webClient = null;
            }

            if (string.IsNullOrEmpty(data))
                return new AresInfo();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(data);

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            xmlNamespaceManager.AddNamespace("are", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/ares/ares_answer_basic/v_1.0.3");
            xmlNamespaceManager.AddNamespace("D", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/ares/ares_datatypes/v_1.0.3");
            xmlNamespaceManager.AddNamespace("U", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/uvis_datatypes/v_1.0.3");
            xmlNamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            XmlNode recordsCountNode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:PZA", xmlNamespaceManager);

            if (recordsCountNode == null)
                return new AresInfo();

            if (!int.TryParse(recordsCountNode.InnerText, out int recordsCount))
                return new AresInfo();

            if (recordsCount == 0 || recordsCount > 1)
                return new AresInfo();

            return new AresInfo
            {
                CompanyName = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:OF", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:OF", xmlNamespaceManager).InnerText,
                CRN = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ICO", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ICO", xmlNamespaceManager).InnerText,
                VATRegNo = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:DIC", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:DIC", xmlNamespaceManager).InnerText,
                CompanyStartDate = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:DV", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:DV", xmlNamespaceManager).InnerText,
                City = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:N", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:N", xmlNamespaceManager).InnerText,
                CityPart = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:NCO", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:NCO", xmlNamespaceManager).InnerText,
                CityPart2 = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:NMC", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:NMC", xmlNamespaceManager).InnerText,
                Street = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:NU", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:NU", xmlNamespaceManager).InnerText,
                BuildingNo = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:CD", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:CD", xmlNamespaceManager).InnerText,
                OrientationNo = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:CO", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:CO", xmlNamespaceManager).InnerText,
                PostalCode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:PSC", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:PSC", xmlNamespaceManager).InnerText,
                DistrictCode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KOK", xmlNamespaceManager) == null ? -1 : int.Parse(xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KOK", xmlNamespaceManager).InnerText),
                CityCode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KO", xmlNamespaceManager) == null ? -1 : int.Parse(xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KO", xmlNamespaceManager).InnerText),
                CityPartCode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KCO", xmlNamespaceManager) == null ? -1 : int.Parse(xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KCO", xmlNamespaceManager).InnerText),
                StreetCode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KUL", xmlNamespaceManager) == null ? -1 : int.Parse(xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:AU/U:KUL", xmlNamespaceManager).InnerText),
                RegisteredBy = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:SD/D:T", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:SD/D:T", xmlNamespaceManager).InnerText,
                FileNo = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:OV", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:OV", xmlNamespaceManager).InnerText

                // District is not included in the ARES basic response.
                //District = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:N", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:AA/D:N", xmlNamespaceManager).InnerText,
            };
        }
    }
}
