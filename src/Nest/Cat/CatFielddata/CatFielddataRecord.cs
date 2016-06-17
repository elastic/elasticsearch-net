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
		public string Field { get; set; }
		public string Size { get; set; }
	}
}
