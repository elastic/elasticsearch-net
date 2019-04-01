using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutAliasRequest
	{
		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty("index_routing")]
		Routing IndexRouting { get; set; }

		/// <inheritdoc cref="AliasAddOperation.IsWriteIndex" />
		[JsonProperty("is_write_index")]
		bool? IsWriteIndex { get; set; }

		[JsonProperty("routing")]
		Routing Routing { get; set; }

		[JsonProperty("search_routing")]
		Routing SearchRouting { get; set; }
	}

	public partial class PutAliasRequest
	{
		public QueryContainer Filter { get; set; }
		public Routing IndexRouting { get; set; }

		/// <inheritdoc cref="AliasAddOperation.IsWriteIndex" />
		public bool? IsWriteIndex { get; set; }

		public Routing Routing { get; set; }
		public Routing SearchRouting { get; set; }
	}

	[DescriptorFor("IndicesPutAlias")]
	public partial class PutAliasDescriptor
	{
		QueryContainer IPutAliasRequest.Filter { get; set; }
		Routing IPutAliasRequest.IndexRouting { get; set; }
		bool? IPutAliasRequest.IsWriteIndex { get; set; }
		Routing IPutAliasRequest.Routing { get; set; }
		Routing IPutAliasRequest.SearchRouting { get; set; }

		public PutAliasDescriptor Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);

		public PutAliasDescriptor IndexRouting(Routing routing) => Assign(routing, (a, v) => a.IndexRouting = v);

		public PutAliasDescriptor SearchRouting(Routing routing) => Assign(routing, (a, v) => a.SearchRouting = v);

		/// <inheritdoc cref="AliasAddOperation.IsWriteIndex" />
		public PutAliasDescriptor IsWriteIndex(bool? isWriteIndex = true) => Assign(isWriteIndex, (a, v) => a.IsWriteIndex = v);

		public PutAliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) where T : class =>
			Assign(filterSelector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
