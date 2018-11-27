using System.Runtime.Serialization;

namespace Nest
{
	public class AliasAddOperation
	{
		[DataMember(Name ="alias")]
		public string Alias { get; set; }

		[DataMember(Name ="filter")]
		public QueryContainer Filter { get; set; }

		[DataMember(Name ="index")]
		public IndexName Index { get; set; }

		[DataMember(Name ="index_routing")]
		public string IndexRouting { get; set; }

		/// <summary>
		/// If an alias points to multiple indices elasticsearch will reject the write operations
		/// unless one is explicitly marked with as the write alias using this property.
		/// </summary>
		[DataMember(Name ="is_write_index")]
		public bool? IsWriteIndex { get; set; }

		[DataMember(Name ="routing")]
		public string Routing { get; set; }

		[DataMember(Name ="search_routing")]
		public string SearchRouting { get; set; }
	}
}
