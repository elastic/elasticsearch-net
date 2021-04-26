/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Nest.Utf8Json;

namespace Nest
{
	// This does not extend from Union because its intended to be used on folk's _source's
	// And the union serialization will bleed into their own JSON.NET serializer should they
	// have one configured and then it will blow up because their contractresolver do not extend ours
	// from which we can snoop ConnectionSettings.
	//
	// ContractJsonResolverAttribute works as well but I rather keep this class contained as much as possible
	/// <summary>
	/// The field on which a parent-child relationship is joined
	/// </summary>
	[JsonFormatter(typeof(JoinFieldFormatter))]
	public class JoinField
	{
		internal Child ChildOption { get;  }
		internal Parent ParentOption { get; }
		internal int Tag { get; }

		public JoinField(Parent parentName)
		{
			ParentOption = parentName;
			Tag = 0;
		}

		public JoinField(Child child)
		{
			ChildOption = child;
			Tag = 1;
		}

		public static JoinField Root<TParent>() => new Parent(typeof(TParent));

		public static JoinField Root(RelationName parent) => new Parent(parent);

		public static JoinField Link(RelationName child, Id parentId) => new Child(child, parentId);

		public static JoinField Link<TChild, TParentDocument>(TParentDocument parent) where TParentDocument : class =>
			new Child(typeof(TChild), Id.From(parent));

		public static JoinField Link<TChild>(Id parentId) => new Child(typeof(TChild), parentId);

		public static implicit operator JoinField(Parent parent) => new JoinField(parent);

		public static implicit operator JoinField(string parentName) => new JoinField(new Parent(parentName));

		public static implicit operator JoinField(Type parentType) => new JoinField(new Parent(parentType));

		public static implicit operator JoinField(Child child) => new JoinField(child);

		public T Match<T>(Func<Parent, T> first, Func<Child, T> second)
		{
			switch (Tag)
			{
				case 0:
					return first(ParentOption);
				case 1:
					return second(ChildOption);
				default: throw new Exception($"Unrecognized tag value: {Tag}");
			}
		}

		public void Match(Action<Parent> first, Action<Child> second)
		{
			switch (Tag)
			{
				case 0:
					first(ParentOption);
					break;
				case 1:
					second(ChildOption);
					break;
				default: throw new Exception($"Unrecognized tag value: {Tag}");
			}
		}

		public class Parent
		{
			public Parent(RelationName name) => Name = name;

			public RelationName Name { get; }
		}

		public class Child
		{
			public Child(RelationName name, Id parent)
			{
				Name = name;
				ParentId = parent;
			}

			public RelationName Name { get; }
			public Id ParentId { get; }
		}
	}
}
