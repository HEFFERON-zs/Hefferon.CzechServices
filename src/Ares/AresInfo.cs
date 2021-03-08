using System;
using System.Collections.Generic;
using System.Text;

namespace Hefferon.CzechServices.Ares
{
    /// <summary>
    /// ARES (Access to Registers of Economic Subjects) informations.
    /// </summary>
    public class AresInfo
    {
        /// <summary>
        /// Bussiness subject first name.
        /// </summary>
        public string BussinessSubjectFirstName { get; set; }

        /// <summary>
        /// Bussiness subject last name.
        /// </summary>
        public string BussinessSubjectLastName { get; set; }

        /// <summary>
        /// Gets or sets company registration number.
        /// </summary>
        public string CRN { get; set; }

        /// <summary>
        /// Gets or sets VAT registration number.
        /// </summary>
        public string VATRegNo { get; set; }

        /// <summary>
        /// Gets or sets company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets company registration date.
        /// </summary>
        public string CompanyStartDate { get; set; }

        /// <summary>
        /// Gets or sets district.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets city part.
        /// </summary>
        public string CityPart { get; set; }

        /// <summary>
        /// Gets or sets city part 2.
        /// </summary>
        public string CityPart2 { get; set; }

        /// <summary>
        /// Gets or sets street.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets building number.
        /// </summary>
        public string BuildingNo { get; set; }

        /// <summary>
        /// Gets or sets orientation number.
        /// </summary>
        public string OrientationNo { get; set; }

        /// <summary>
        /// Gets or sets postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets region code.
        /// </summary>
        public int RegionCode { get; set; }

        /// <summary>
        /// Gets or sets district code.
        /// </summary>
        public int DistrictCode { get; set; }

        /// <summary>
        /// Gets or sets city code.
        /// </summary>
        public int CityCode { get; set; }

        /// <summary>
        /// Gets or sets city part code.
        /// </summary>
        public int CityPartCode { get; set; }

        /// <summary>
        /// Gets or sets street code.
        /// </summary>
        public int StreetCode { get; set; }

        /// <summary>
        /// Gets or sets building number code.
        /// </summary>
        public int BuildingNumberCode { get; set; }

        /// <summary>
        /// Gets or sets orientation number code.
        /// </summary>
        public int OrientationNumberCode { get; set; }

        /// <summary>
        /// Gets or sets postal code code.
        /// </summary>
        public int PostalCodeCode { get; set; }

        /// <summary>
        /// Gets or sets file number.
        /// </summary>
        public string FileNo { get; set; }

        /// <summary>
        /// Gets or sets registered by.
        /// </summary>
        public string RegisteredBy { get; set; }
    }
}
