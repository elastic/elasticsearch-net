using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nest
{
	internal static class AggregationMetadata
	{
		public static ReadOnlyDictionary<string, Type> Map { get; private set; }

		static AggregationMetadata()
		{
			var map = new Dictionary<string, Type>();
			var types = Assembly
				.GetCallingAssembly()
				.GetTypes()
				.Where(t => t.IsInterface && typeof(IAggregation).IsAssignableFrom(t));

			foreach (var type in types)
			{
				var attribute = type.GetTypeInfo()
					.GetCustomAttributes(typeof(AggregateTypeAttribute), true)
					.FirstOrDefault() as AggregateTypeAttribute;
				if (attribute != null)
					map.Add(FormatTypeName(type), attribute.Type);
			}
			Map = new ReadOnlyDictionary<string, Type>(map);
		}

		public const string Key = "_type";

		public static KeyValuePair<string, object> GetMetadataEntry(IAggregation aggregation)
		{
			var interfaces = aggregation.GetType().GetInterfaces();
			foreach (var i in interfaces)
			{
				var value = FormatTypeName(i);
				if (Map.ContainsKey(value))
					return new KeyValuePair<string, object>(Key, value);
			}
			throw new Exception($"Did not resolve an aggregate mapping for type {aggregation.GetType().FullName}.");
		}

		private static string FormatTypeName(Type type)
		{
			// IDateHistogramAggregation => DateHistogram
			// ITermsAggregation => Terms
			var root = type.Name.Substring(1).Replace("Aggregation", "");

			// DateHistogram => Date_Histogram
			// Terms => Terms
			var value = Regex.Replace(root, @"((?<=.)[A-Z][a-zA-Z]*)|((?<=[a-zA-Z])\d+)", "_$1$2");

			// Date_Histogram => date_histogram
			// Terms => terms
			return value.ToLower();
		}
	}
}
