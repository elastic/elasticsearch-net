using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
	public class ElasticTypeAttribute : Attribute
	{
		private static readonly ConcurrentDictionary<Type, ElasticTypeAttribute> CachedTypeLookups =
			new ConcurrentDictionary<Type, ElasticTypeAttribute>();

		public string Name { get; set; }
		public string IdProperty { get; set; }

		public static ElasticTypeAttribute From(Type type)
		{
			ElasticTypeAttribute attr = null;
			if (CachedTypeLookups.TryGetValue(type, out attr))
				return attr;

			var attributes = type.GetCustomAttributes(typeof(ElasticTypeAttribute), true);
			if (attributes.HasAny())
				attr = ((ElasticTypeAttribute)attributes.First());
			CachedTypeLookups.TryAdd(type, attr);
			return attr;
		}
	}
}