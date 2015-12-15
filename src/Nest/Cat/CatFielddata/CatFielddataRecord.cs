using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(CatFielddataRecordJsonConverter))]
	public class CatFielddataRecord : ICatRecord
	{
		public string Id { get; set; }
		public string Host { get; set; }
		public string Ip { get; set; }
		public string Node { get; set; }
		public string Total { get; set; }

		public IDictionary<string, string> FieldSizes { get; set; }
	}
	
}