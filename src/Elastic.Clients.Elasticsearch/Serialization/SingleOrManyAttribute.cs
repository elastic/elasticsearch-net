// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch
{
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class SingleOrManyAttribute : Attribute
	{
		public SingleOrManyAttribute(Type itemType) => ItemType = itemType;

		public Type ItemType { get; }
	}
}
