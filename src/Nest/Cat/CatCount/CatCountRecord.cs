using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatCountRecord : ICatRecord
	{
		[DataMember(Name ="count")]
		public string Count { get; set; }

		[DataMember(Name ="epoch")]
		public string Epoch { get; set; }

		[DataMember(Name ="timestamp")]
		public string Timestamp { get; set; }
	}
}
