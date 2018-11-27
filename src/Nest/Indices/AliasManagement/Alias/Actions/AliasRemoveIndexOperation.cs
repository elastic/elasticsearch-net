using System.Runtime.Serialization;

namespace Nest
{
	public class AliasRemoveIndexOperation
	{
		[DataMember(Name ="index")]
		public IndexName Index { get; set; }
	}
}
