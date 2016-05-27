using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Alias>))]
	public interface IAlias
	{
		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty("routing")]
		string Routing { get; set; }

		[JsonProperty("index_routing")]
		string IndexRouting { get; set; }

		[JsonProperty("search_routing")]
		string SearchRouting { get; set; }
	}

	public class Alias : IAlias
	{
		public QueryContainer Filter { get; set; }
		public string Routing { get; set; }
		public string IndexRouting { get; set; }
		public string SearchRouting { get; set; }
	}

	public class AliasDescriptor : DescriptorBase<AliasDescriptor, IAlias>, IAlias
	{
		QueryContainer IAlias.Filter { get; set; }
		string IAlias.Routing { get; set; }
		string IAlias.IndexRouting { get; set; }
		string IAlias.SearchRouting { get; set; }

		public AliasDescriptor Routing(string routing) => Assign(a => a.Routing = routing);
		public AliasDescriptor IndexRouting(string indexRouting) => Assign(a => a.IndexRouting = indexRouting);
		public AliasDescriptor SearchRouting(string searchRouting) => Assign(a => a.SearchRouting = searchRouting);
		public AliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) where T : class => 
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}