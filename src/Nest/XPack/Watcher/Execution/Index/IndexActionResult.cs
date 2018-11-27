using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class IndexActionResult
	{
		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="response")]
		public IndexActionResultIndexResponse Response { get; set; }
	}
}
