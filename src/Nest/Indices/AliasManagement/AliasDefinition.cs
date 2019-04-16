using System.Runtime.Serialization;

namespace Nest
{
	public class AliasDefinition
	{
		[DataMember(Name ="filter")]
		public IQueryContainer Filter { get; internal set; }

		[DataMember(Name ="index_routing")]
		public string IndexRouting { get; internal set; }

		[DataMember(Name ="is_write_index")]
		public bool? IsWriteIndex { get; internal set; }

		[DataMember(Name ="routing")]
		public string Routing { get; internal set; }

		[DataMember(Name ="search_routing")]
		public string SearchRouting { get; internal set; }
	}
}
