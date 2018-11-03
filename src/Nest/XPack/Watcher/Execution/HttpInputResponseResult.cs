using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class HttpInputResponseResult
	{
		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("headers")]
		public IDictionary<string, string[]> Headers { get; set; }

		[JsonProperty("status")]
		public int StatusCode { get; set; }
	}
}
