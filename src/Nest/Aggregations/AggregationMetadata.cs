using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nest
{
	internal static class AggregationMetadata
	{
		public static readonly string Key = "_type";

		public static string Value(IAggregation aggregation)
		{
			var interfaces = aggregation.GetType().GetInterfaces();
			foreach(var i in interfaces)
			{
				var value = GetElasticsearchName(i);
				if (Map.ContainsKey(value))
					return value;
			}
			return null;
		}

		private static Dictionary<string, Type> _map;
		public static Dictionary<string, Type> Map
		{
			get
			{
				if (_map == null)
					_map = CreateMap();
				return _map;
			}
		}

		private static string GetElasticsearchName(Type type)
		{
			// IDateHistogramAggregation => DateHistogram, ITermsAggregation => Terms
			var root = type.Name.Substring(1).Replace("Aggregation", "");

			// DateHistogram => Date_Histogram, Terms => Terms
			var value = Regex.Replace(root, @"((?<=.)[A-Z][a-zA-Z]*)|((?<=[a-zA-Z])\d+)", "_$1$2");

			// DateHistogram => date_histogram, Terms => terms
			return value.ToLower();
		}

		private static Dictionary<string, Type> CreateMap()
		{
			var map = new Dictionary<string, Type>();

			var types = Assembly
				.GetCallingAssembly()
				.GetTypes()
				.Where(t => t.IsInterface && typeof(IAggregation).IsAssignableFrom(t));

			foreach(var type in types)
			{
				var attribute = type.GetTypeInfo()
					.GetCustomAttributes(typeof(AggregateTypeAttribute), true)
					.FirstOrDefault() as AggregateTypeAttribute;
				if (attribute != null)
					map.Add(GetElasticsearchName(type), attribute.Type);
			}

			return map;
		}
	}
}
