// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<Relations, IRelations, RelationName, Children>))]
	public interface IRelations : IIsADictionary<RelationName, Children> { }

	public class Relations : IsADictionaryBase<RelationName, Children>, IRelations
	{
		public Relations() { }

		public Relations(IDictionary<RelationName, Children> container) : base(container) { }

		public Relations(Dictionary<RelationName, Children> container) : base(container) { }

		public void Add(RelationName type, Children children)
		{
			if (BackingDictionary.ContainsKey(type))
				throw new ArgumentException($"{type} is already mapped as parent, you have to map all it's children as a single entry");

			BackingDictionary.Add(type, children);
		}

		public void Add(RelationName type, RelationName child, params RelationName[] moreChildren)
		{
			if (BackingDictionary.ContainsKey(type))
				throw new ArgumentException($"{type} is already mapped as parent, you have to map all it's children as a single entry");

			BackingDictionary.Add(type, new Children(child, moreChildren));
		}
	}

	public class RelationsDescriptor : IsADictionaryDescriptorBase<RelationsDescriptor, IRelations, RelationName, Children>
	{
		public RelationsDescriptor() : base(new Relations()) { }

		internal RelationsDescriptor(IRelations relations) : base(relations) { }

		public RelationsDescriptor Join(RelationName parent, RelationName child, params RelationName[] moreChildren) =>
			Assign(parent, new Children(child, moreChildren));

		public RelationsDescriptor Join<TParent>(RelationName child, params RelationName[] moreChildren)
		{
			if (PromisedValue.ContainsKey(typeof(TParent))) throw new ArgumentException(Message(typeof(TParent)));

			return Assign(typeof(TParent), new Children(child, moreChildren));
		}

		public RelationsDescriptor Join<TParent, TChild>()
		{
			if (PromisedValue.ContainsKey(typeof(TParent))) throw new ArgumentException(Message(typeof(TParent)));

			return Assign(typeof(TParent), typeof(TChild));
		}

		private static string Message(Type t) =>
			$"{t.Name} is already mapped. Use Join<TParent>(typeof(ChildA), typeof(ChildB), ..) to add multiple children in one go";
	}
}
