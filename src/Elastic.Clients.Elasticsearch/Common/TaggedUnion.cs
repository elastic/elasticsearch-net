// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elastic.Clients.Elasticsearch;

public interface ITaggedUnion<T>
{
	public abstract string Type { get; }

	public abstract T Value { get; }

	public bool Is(string type);
}

public class TaggedUnionUtils
{
	public static T Get<T, TUnion>(TUnion union, string type) where TUnion : ITaggedUnion<T>
	{
		if (union.Is(type))
			return union.Value;

		throw new InvalidCastException("TODO");
	}
}

public interface IUnionVariant
{
	/// <summary>
	/// Get the type of this object when used as a variant.
	/// </summary>
	string VariantType { get; }
}

//public interface IContainer { }

//public abstract class ContainerBase : IContainer
//{
//	internal void WrapInContainer(IContainer container) => InternalWrapInContainer(container);

//	internal abstract void InternalWrapInContainer(IContainer container);
//}
