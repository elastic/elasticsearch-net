using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch.Net.Extensions
{
	internal static class Fluent
	{
		internal static TDescriptor Assign<TDescriptor, TInterface>(TDescriptor self, Action<TInterface> assign)
			where TDescriptor : class, TInterface
		{
			assign(self);
			return self;
		}
	}

}