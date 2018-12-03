using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(CatFielddataRecordFormatter))]
	public class CatFielddataRecord : ICatRecord
	{
		public string Field { get; set; }
		public string Host { get; set; }
		public string Id { get; set; }
		public string Ip { get; set; }
		public string Node { get; set; }
		public string Size { get; set; }
	}
}
