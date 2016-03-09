using System;
using Newtonsoft.Json;

namespace Nest
{
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAliasAddAction : IAliasAction
	{
		[JsonProperty("add")]
		AliasAddOperation Add { get; set; }
	}

	public class AliasAddAction : IAliasAddAction
	{
		public AliasAddOperation Add { get; set; }
	}

	public class AliasAddDescriptor : DescriptorBase<AliasAddDescriptor, IAliasAddAction>, IAliasAddAction
	{
		public AliasAddDescriptor()
		{
			Self.Add = new AliasAddOperation();
		}

		AliasAddOperation IAliasAddAction.Add { get; set; }

		public AliasAddDescriptor Index(string index)
		{
			Self.Add.Index = index;
			return this;
		}
		public AliasAddDescriptor Index(Type index)
		{
			Self.Add.Index = index;
			return this;
		}
		public AliasAddDescriptor Index<T>() where T : class
		{
			Self.Add.Index = typeof(T);
			return this;
		}
		public AliasAddDescriptor Alias(string alias)
		{
			Self.Add.Alias = alias;
			return this;
		}
		public AliasAddDescriptor Routing(string routing)
		{
			Self.Add.Routing = routing;
			return this;
		}
		public AliasAddDescriptor IndexRouting(string indexRouting)
		{
			Self.Add.IndexRouting = indexRouting;
			return this;
		}
		public AliasAddDescriptor SearchRouting(string searchRouting)
		{
			Self.Add.SearchRouting = searchRouting;
			return this;
		}
		public AliasAddDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector)
			where T : class
		{
			Self.Add.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>());
			return this;
		}
	}
}