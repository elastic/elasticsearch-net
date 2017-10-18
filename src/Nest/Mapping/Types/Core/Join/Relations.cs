using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Relations, RelationName, Children>))]
	public interface IRelations : IIsADictionary<RelationName, Children> { }

	public class Relations : IsADictionaryBase<RelationName, Children>, IRelations
	{
		public Relations() {}
		public Relations(IDictionary<RelationName, Children> container) : base(container) { }
		public Relations(Dictionary<RelationName, Children> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(RelationName type, Children children) => BackingDictionary.Add(type, children);
		public void Add(RelationName type, RelationName child, params RelationName[] moreChildren) =>
			BackingDictionary.Add(type, new Children(child, moreChildren));
	}

	public class RelationsDescriptor : IsADictionaryDescriptorBase<RelationsDescriptor,	IRelations, RelationName, Children>
	{
		public RelationsDescriptor() : base(new Relations()) { }
		internal RelationsDescriptor(IRelations relations) : base(relations) { }

		public RelationsDescriptor Join(RelationName parent, RelationName child, params RelationName[] moreChildren) =>
			Assign(parent, new Children(child, moreChildren));
		public RelationsDescriptor Join<TParent>(RelationName child, params RelationName[] moreChildren) =>
			Assign(typeof(TParent), new Children(child, moreChildren));
		public RelationsDescriptor Join<TParent, TChild>() => Assign(typeof(TParent), typeof(TChild));
	}
}
