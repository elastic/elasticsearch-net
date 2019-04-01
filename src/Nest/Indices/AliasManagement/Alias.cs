using System;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(Alias))]
	public interface IAlias
	{
		[DataMember(Name ="filter")]
		QueryContainer Filter { get; set; }

		[DataMember(Name ="index_routing")]
		Routing IndexRouting { get; set; }

		[DataMember(Name ="routing")]
		Routing Routing { get; set; }

		[DataMember(Name ="search_routing")]
		Routing SearchRouting { get; set; }
	}

	public class Alias : IAlias
	{
		public QueryContainer Filter { get; set; }
		public Routing IndexRouting { get; set; }
		public Routing Routing { get; set; }
		public Routing SearchRouting { get; set; }
	}

	public class AliasDescriptor : DescriptorBase<AliasDescriptor, IAlias>, IAlias
	{
		QueryContainer IAlias.Filter { get; set; }
		Routing IAlias.IndexRouting { get; set; }
		Routing IAlias.Routing { get; set; }
		Routing IAlias.SearchRouting { get; set; }

		public AliasDescriptor Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);

		public AliasDescriptor IndexRouting(Routing indexRouting) => Assign(indexRouting, (a, v) => a.IndexRouting = v);

		public AliasDescriptor SearchRouting(Routing searchRouting) => Assign(searchRouting, (a, v) => a.SearchRouting = v);

		public AliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) where T : class =>
			Assign(filterSelector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
