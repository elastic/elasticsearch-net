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
