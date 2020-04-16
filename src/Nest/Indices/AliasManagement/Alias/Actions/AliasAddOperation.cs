using System.Runtime.Serialization;

namespace Nest
{
	public class AliasAddOperation
	{
		/// <summary>
		/// The name of the alias
		/// </summary>
		[DataMember(Name ="alias")]
		public string Alias { get; set; }

		/// <summary>
		/// Filter query used to limit the index alias.
		/// If specified, the index alias only applies to documents returned by the filter.
		/// </summary>
		[DataMember(Name ="filter")]
		public QueryContainer Filter { get; set; }

		/// <summary>
		/// The index to which to add the alias
		/// </summary>
		[DataMember(Name ="index")]
		public IndexName Index { get; set; }

		/// <inheritdoc cref="IAlias.IndexRouting"/>
		[DataMember(Name ="index_routing")]
		public string IndexRouting { get; set; }

		/// <inheritdoc cref="IAlias.IsWriteIndex"/>
		[DataMember(Name ="is_write_index")]
		public bool? IsWriteIndex { get; set; }

		/// <inheritdoc cref="IAlias.IsHidden"/>
		[DataMember(Name ="is_hidden")]
		public bool? IsHidden { get; set; }

		/// <inheritdoc cref="IAlias.Routing"/>
		[DataMember(Name ="routing")]
		public string Routing { get; set; }

		/// <inheritdoc cref="IAlias.SearchRouting"/>
		[DataMember(Name ="search_routing")]
		public string SearchRouting { get; set; }
	}
}
