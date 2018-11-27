using System.Runtime.Serialization;

namespace Nest
{
	public class AliasRemoveOperation
	{
		[DataMember(Name ="alias")]
		public string Alias { get; set; }

		[DataMember(Name ="index")]
		public IndexName Index { get; set; }
	}
}
