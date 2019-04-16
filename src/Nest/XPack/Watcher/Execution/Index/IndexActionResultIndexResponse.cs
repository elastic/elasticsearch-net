using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class IndexActionResultIndexResponse
	{
		[DataMember(Name ="created")]
		public bool? Created { get; set; }

		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="index")]
		public IndexName Index { get; set; }

		[DataMember(Name ="result")]
		public Result Result { get; set; }

		[DataMember(Name = "version")]
		public int Version { get; set; }
	}
}
