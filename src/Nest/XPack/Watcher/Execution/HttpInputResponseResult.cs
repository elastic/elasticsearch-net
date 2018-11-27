using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class HttpInputResponseResult
	{
		[DataMember(Name ="body")]
		public string Body { get; set; }

		[DataMember(Name ="headers")]
		public IDictionary<string, string[]> Headers { get; set; }

		[DataMember(Name ="status")]
		public int StatusCode { get; set; }
	}
}
