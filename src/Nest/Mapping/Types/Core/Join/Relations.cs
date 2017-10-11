using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Relations, TypeName, Children>))]
	public interface IRelations : IIsADictionary<TypeName, Children> { }

	public class Relations : IsADictionaryBase<TypeName, Children>, IRelations
	{
		public Relations() {}
		public Relations(IDictionary<TypeName, Children> container) : base(container) { }
		public Relations(Dictionary<TypeName, Children> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(TypeName type, Children children) => BackingDictionary.Add(type, children);
		public void Add(TypeName type, TypeName child, params TypeName[] moreChildren) =>
			BackingDictionary.Add(type, Types.Type(child).And(moreChildren));
	}

	public class RelationsDescriptor : IsADictionaryDescriptorBase<RelationsDescriptor,	IRelations, TypeName, Children>
	{
		public RelationsDescriptor() : base(new Relations()) { }
		internal RelationsDescriptor(IRelations relations) : base(relations) { }

		public RelationsDescriptor Join(TypeName parent, TypeName child, params TypeName[] moreChildren) =>
			Assign(parent, child.And(moreChildren));
		public RelationsDescriptor Join<TParent>(TypeName child, params TypeName[] moreChildren) =>
			Assign(typeof(TParent), child.And(moreChildren));
		public RelationsDescriptor Join<TParent, TChild>() => Assign(typeof(TParent), typeof(TChild));
	}
}
