using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Nest
{
	[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
	public class ElasticsearchTypeAttribute : Attribute
	{
		private static readonly ConcurrentDictionary<Type, ElasticsearchTypeAttribute> CachedTypeLookups =
			new ConcurrentDictionary<Type, ElasticsearchTypeAttribute>();

		public string Name { get; set; }
		public string IdProperty { get; set; }

		public static ElasticsearchTypeAttribute From(Type type)
		{
			ElasticsearchTypeAttribute attr;
			if (CachedTypeLookups.TryGetValue(type, out attr))
				return attr;

			var attributes = type.GetTypeInfo().GetCustomAttributes(typeof(ElasticsearchTypeAttribute), true);
			if (attributes.HasAny())
				attr = ((ElasticsearchTypeAttribute)attributes.First());
			CachedTypeLookups.TryAdd(type, attr);
			return attr;
		}
	}
}