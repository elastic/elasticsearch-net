using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateAliasOperation
	{
		[JsonProperty("filter")]
		FilterContainer Filter { get; set; }

		[JsonProperty("routing")]
		string Routing { get; set; }

		[JsonProperty("index_routing")]
		string IndexRouting { get; set; }

		[JsonProperty("search_routing")]
		string SearchRouting { get; set; }
	}

	public class CreateAliasOperation : ICreateAliasOperation
	{
		public FilterContainer Filter { get; set; }
		public string Routing { get; set; }
		public string IndexRouting { get; set; }
		public string SearchRouting { get; set; }
	}

	public class CreateAliasDescriptor : ICreateAliasOperation
	{
		private ICreateAliasOperation Self { get { return this; }}


		FilterContainer ICreateAliasOperation.Filter { get; set; }
		string ICreateAliasOperation.Routing { get; set; }
		string ICreateAliasOperation.IndexRouting { get; set; }
		string ICreateAliasOperation.SearchRouting { get; set; }
		
		public CreateAliasDescriptor Routing(string routing)
		{
			Self.Routing = routing;
			return this;
		}
		public CreateAliasDescriptor IndexRouting(string indexRouting)
		{
			Self.IndexRouting = indexRouting;
			return this;
		}
		public CreateAliasDescriptor SearchRouting(string searchRouting)
		{
			Self.SearchRouting = searchRouting;
			return this;
		}
		public CreateAliasDescriptor Filter<T>(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
			where T : class
		{
			filterSelector.ThrowIfNull("filterSelector");

			Self.Filter = filterSelector(new FilterDescriptor<T>());
			return this;
		}

	}
}