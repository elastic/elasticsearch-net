using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatRepositoriesRecord : ICatRecord
	{
		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="type")]
		public string Type { get; set; }
	}
}
