// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	public class AliasAddOperation
	{
		/// <summary>
		/// An alias to add.
		/// Multiple aliases can be specified with <see cref="Aliases"/>
		/// </summary>
		[DataMember(Name ="alias")]
		public string Alias { get; set; }

		/// <summary>
		/// A collection of aliases to add
		/// </summary>
		[DataMember(Name ="aliases")]
		public IEnumerable<string> Aliases { get; set; }

		/// <summary>
		/// Filter query used to limit the index alias.
		/// If specified, the index alias only applies to documents returned by the filter.
		/// </summary>
		[DataMember(Name ="filter")]
		public QueryContainer Filter { get; set; }

		/// <summary>
		/// The index to which to add the alias.
		/// Multiple indices can be specified with <see cref="Indices"/>
		/// </summary>
		[DataMember(Name ="index")]
		public IndexName Index { get; set; }

		/// <summary>
		/// The indices to which to add the alias
		/// </summary>
		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		public Indices Indices { get; set; }

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
