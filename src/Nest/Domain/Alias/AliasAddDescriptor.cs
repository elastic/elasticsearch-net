using System;
using Newtonsoft.Json;

namespace Nest
{
	public class AliasAddDescriptor : IAliasAction
	{
		public AliasAddDescriptor()
		{
			this.Add = new AliasAddOperation();
		}

		[JsonProperty("add")]
		internal AliasAddOperation Add { get; private set; }

		public AliasAddDescriptor Index(string index)
		{
			this.Add.Index = index;
			return this;
		}
		public AliasAddDescriptor Index(Type index)
		{
			this.Add.Index = index;
			return this;
		}
		public AliasAddDescriptor Index<T>() where T : class
		{
			this.Add.Index = typeof(T);
			return this;
		}
		public AliasAddDescriptor Alias(string alias)
		{
			this.Add.Alias = alias;
			return this;
		}
		public AliasAddDescriptor Routing(string routing)
		{
			this.Add.Routing = routing;
			return this;
		}
		public AliasAddDescriptor IndexRouting(string indexRouting)
		{
			this.Add.IndexRouting = indexRouting;
			return this;
		}
		public AliasAddDescriptor SearchRouting(string searchRouting)
		{
			this.Add.SearchRouting = searchRouting;
			return this;
		}
		public AliasAddDescriptor Filter<T>(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
			where T : class
		{
			filterSelector.ThrowIfNull("filterSelector");

			this.Add.FilterDescriptor = filterSelector(new FilterDescriptor<T>());
			return this;
		}
	}
}