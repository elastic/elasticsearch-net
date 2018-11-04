using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutAliasRequest
	{
		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty("routing")]
		string Routing { get; set; }
	}

	public partial class PutAliasRequest
	{
		public QueryContainer Filter { get; set; }
		public string Routing { get; set; }
	}

	[DescriptorFor("IndicesPutAlias")]
	public partial class PutAliasDescriptor
	{
		QueryContainer IPutAliasRequest.Filter { get; set; }
		string IPutAliasRequest.Routing { get; set; }

		public PutAliasDescriptor Routing(string routing) => Assign(a => a.Routing = routing);

		public PutAliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector)
			where T : class =>
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
