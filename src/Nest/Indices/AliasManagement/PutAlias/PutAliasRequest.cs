using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("indices.put_alias.json")]
	public partial interface IPutAliasRequest
	{
		[DataMember(Name ="filter")]
		QueryContainer Filter { get; set; }

		[DataMember(Name ="index_routing")]
		Routing IndexRouting { get; set; }

		/// <inheritdoc cref="AliasAddOperation.IsWriteIndex" />
		[DataMember(Name ="is_write_index")]
		bool? IsWriteIndex { get; set; }

		[DataMember(Name ="routing")]
		Routing Routing { get; set; }

		[DataMember(Name ="search_routing")]
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

	public partial class PutAliasDescriptor
	{
		QueryContainer IPutAliasRequest.Filter { get; set; }
		Routing IPutAliasRequest.IndexRouting { get; set; }
		bool? IPutAliasRequest.IsWriteIndex { get; set; }
		Routing IPutAliasRequest.Routing { get; set; }
		Routing IPutAliasRequest.SearchRouting { get; set; }

		public PutAliasDescriptor Routing(Routing routing) => Assign(a => a.Routing = routing);

		public PutAliasDescriptor IndexRouting(Routing routing) => Assign(a => a.IndexRouting = routing);

		public PutAliasDescriptor SearchRouting(Routing routing) => Assign(a => a.SearchRouting = routing);

		/// <inheritdoc cref="AliasAddOperation.IsWriteIndex" />
		public PutAliasDescriptor IsWriteIndex(bool? isWriteIndex = true) => Assign(a => a.IsWriteIndex = isWriteIndex);

		public PutAliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) where T : class =>
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
