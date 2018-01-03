using System;
using Newtonsoft.Json;

namespace Nest
{

	public partial interface IPutAliasRequest
	{
		[JsonProperty("routing")]
		Routing Routing { get; set; }

		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }
	}

	public partial class PutAliasRequest
	{
		public Routing Routing { get; set; }

		public QueryContainer Filter { get; set; }
	}

	[DescriptorFor("IndicesPutAlias")]
	public partial class PutAliasDescriptor
	{
		Routing IPutAliasRequest.Routing { get; set; }
		QueryContainer IPutAliasRequest.Filter { get; set; }

		public PutAliasDescriptor Routing(Routing routing) => Assign(a => a.Routing = routing);

		public PutAliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector)
			where T : class =>
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
