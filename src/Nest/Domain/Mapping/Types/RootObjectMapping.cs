using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class MapRootObject : ObjectMapping
    {

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("dynamic_date_formats")]
		public IEnumerable<string> DynamicDateFormats { get; set; }

		[JsonProperty("date_detection")]
		public bool? DateDetection { get; set; }

		[JsonProperty("numeric_detection")]
		public bool? NumericDetection { get; set; }

		//NESTED TODO also map dynamic_templates
    }
}