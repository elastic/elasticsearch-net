using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class HttpInputResponseResult
	{
		[JsonProperty("status")]
		public int StatusCode { get; set; }

		[JsonProperty("headers")]
		public IDictionary<string, string[]> Headers { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }
	}
}
