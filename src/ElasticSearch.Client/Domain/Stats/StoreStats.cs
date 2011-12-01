using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Domain
{
	[JsonObject]
	public class StoreStats
	{
		[JsonProperty(PropertyName = "size")]
		public string Size { get; set; }
		[JsonProperty(PropertyName = "size_in_byes")]
		public double SizeInBytes { get; set; }
	}

}
