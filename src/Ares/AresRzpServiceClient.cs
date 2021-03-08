using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace Hefferon.CzechServices.Ares
{
    /// <summary>
    /// ARES RZP service client functionality using the ARES RZP service
    /// (for more information see http://wwwinfo.mfcr.cz/ares/ares_xml_rzp.html.cz).
    /// </summary>
    public class AresRzpServiceClient
    {
        /// <summary>
        /// Gets ARES informations of the bussines subject specified by <paramref name="crn"/>.
        /// </summary>
        /// <param name="crn">Company registration number.</param>
        /// <returns><see cref="AresInfo"/> of the bussines subject specified by <paramref name="crn"/>, if there was found exactly one subject; otherwise empty <see cref="AresInfo"/>.</returns>
        public AresInfo GetInfoByCrn(string crn)
        {
            WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
            string data = string.Empty;

            try
            {
                data = webClient.DownloadString("http://wwwinfo.mfcr.cz/cgi-bin/ares/darv_rzp.cgi?ico=" + crn);
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
            xmlNamespaceManager.AddNamespace("are", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/ares/ares_answer_rzp/v_1.0.5");
            xmlNamespaceManager.AddNamespace("D", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/ares/ares_datatypes/v_1.0.5");
            xmlNamespaceManager.AddNamespace("U", "http://wwwinfo.mfcr.cz/ares/xml_doc/schemas/uvis_datatypes/v_1.0.3");

            XmlAttribute recordsCountNode = (XmlAttribute)xmlDocument.SelectSingleNode("/are:Ares_odpovedi", xmlNamespaceManager).Attributes.GetNamedItem("odpoved_pocet");

            if (recordsCountNode == null)
                return new AresInfo();

            if (!int.TryParse(recordsCountNode.Value, out int recordsCount))
                return new AresInfo();

            if (recordsCount == 0 || recordsCount > 1)
                return new AresInfo();

            return new AresInfo
            {
                CompanyName = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:ZAU/D:OF", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:ZAU/D:OF", xmlNamespaceManager).InnerText,
                CRN = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:ZAU/D:ICO", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:ZAU/D:ICO", xmlNamespaceManager).InnerText,
                CompanyStartDate = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:UVOD/D:DVY", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:UVOD/D:DVY", xmlNamespaceManager).InnerText,
                City = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:N", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:N", xmlNamespaceManager).InnerText,
                CityPart = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:NCO", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:NCO", xmlNamespaceManager).InnerText,
                Street = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:NU", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:NU", xmlNamespaceManager).InnerText,
                BuildingNo = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:CD", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:CD", xmlNamespaceManager).InnerText,
                OrientationNo = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:CO", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:CO", xmlNamespaceManager).InnerText,
                PostalCode = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:PSC", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Adresy/D:A/D:PSC", xmlNamespaceManager).InnerText,
                RegisteredBy = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:SD/D:T", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:SD/D:T", xmlNamespaceManager).InnerText,
                FileNo = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:OV", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:ROR/D:SZ/D:OV", xmlNamespaceManager).InnerText,
                BussinessSubjectFirstName = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Osoby/D:Osoba/D:J", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Osoby/D:Osoba/D:J", xmlNamespaceManager).InnerText,
                BussinessSubjectLastName = xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Osoby/D:Osoba/D:P", xmlNamespaceManager) == null ? string.Empty : xmlDocument.SelectSingleNode("/are:Ares_odpovedi/are:Odpoved/D:Vypis_RZP/D:Osoby/D:Osoba/D:P", xmlNamespaceManager).InnerText
            };
        }
    }
}
