using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class AddressModel
	{
		[Required]
		[Key]
		public int Address_ID { get; set; }
		[Required]
		[MaxLength(75)]
		public string Address_Line_1 { get; set; }
		[MaxLength(75)]
		public string Address_Line_2 { get; set; }
		[MaxLength(50)]
		public string City { get; set; }
		[MaxLength(50)]
		[JsonProperty(PropertyName = "State/Region")]
		public string State_Region { get; set; }
		[MaxLength(15)]
		public string Postal_Code { get; set; }
		[MaxLength(50)]
		public string Foreign_Country { get; set; }
		[MaxLength(25)]
		public string Country_Code { get; set; }
		[MaxLength(10)]
		public string Carrier_Route { get; set; }
		[MaxLength(10)]
		public string Lot_Number { get; set; }
		[MaxLength(3)]
		public string Delivery_Point_Code { get; set; }
		public string Delivery_Point_Check_Digit { get; set; }
		[MaxLength(15)]
		public string Latitude { get; set; }
		[MaxLength(15)]
		public string Longitude { get; set; }
		[MaxLength(15)]
		public string Altitude { get; set; }
		[MaxLength(50)]
		public string Time_Zone { get; set; }
		[MaxLength(50)]
		public string Bar_Code { get; set; }
		[MaxLength(50)]
		public string Area_Code { get; set; }
		public DateTime? Last_Validation_Attempt { get; set; }
		[MaxLength(50)]
		public string County { get; set; }
		public bool? Validated { get; set; }
		[Required]
		public bool Do_Not_Validate { get; set; }
		public DateTime? Last_GeoCode_Attempt { get; set; }
		[MaxLength(100)]
		public string Country { get; set; }

		public const string TableName = "Addresses";
	}
}
