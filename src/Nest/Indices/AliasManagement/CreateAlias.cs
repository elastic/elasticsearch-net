using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateAliasOperation
	{
		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		QueryContainer Filter { get; set; }

		[JsonProperty("routing")]
		string Routing { get; set; }

		[JsonProperty("index_routing")]
		string IndexRouting { get; set; }

		[JsonProperty("search_routing")]
		string SearchRouting { get; set; }
	}

	public class CreateAliasOperation : ICreateAliasOperation
	{
		public QueryContainer Filter { get; set; }
		public string Routing { get; set; }
		public string IndexRouting { get; set; }
		public string SearchRouting { get; set; }
	}

	public class CreateAliasDescriptor : ICreateAliasOperation
	{
		private ICreateAliasOperation Self { get { return this; }}


		QueryContainer ICreateAliasOperation.Filter { get; set; }
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
		public CreateAliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector)
			where T : class
		{
			filterSelector.ThrowIfNull("filterSelector");

			Self.Filter = filterSelector(new QueryContainerDescriptor<T>());
			return this;
		}

	}
}