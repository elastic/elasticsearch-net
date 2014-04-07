using System;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public class CreateAliasDescriptor 
	{

		[JsonProperty("filter")]
		internal FilterBase _Filter { get; set; }
		[JsonProperty("routing")]
		internal string _Routing { get; set; }
		[JsonProperty("index_routing")]
		internal string _IndexRouting { get; set; }
		[JsonProperty("search_routing")]
		internal string _SearchRouting { get; set; }
		
		public CreateAliasDescriptor Routing(string routing)
		{
			this._Routing = routing;
			return this;
		}
		public CreateAliasDescriptor IndexRouting(string indexRouting)
		{
			this._IndexRouting = indexRouting;
			return this;
		}
		public CreateAliasDescriptor SearchRouting(string searchRouting)
		{
			this._SearchRouting = searchRouting;
			return this;
		}
		public CreateAliasDescriptor Filter<T>(Func<FilterDescriptor<T>, FilterBase> filterSelector)
			where T : class
		{
			filterSelector.ThrowIfNull("filterSelector");

			this._Filter = filterSelector(new FilterDescriptor<T>());
			return this;
		}

	}
}