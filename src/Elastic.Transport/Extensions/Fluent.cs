// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elasticsearch.Net.Extensions
{
	internal static class Fluent
	{
		internal static TDescriptor Assign<TDescriptor, TInterface, TValue>(TDescriptor self, TValue value, Action<TInterface, TValue> assign)
			where TDescriptor : class, TInterface
		{
			assign(self, value);
			return self;
		}
	}
}
