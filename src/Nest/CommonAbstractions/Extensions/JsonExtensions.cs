using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal static class JsonExtensions
	{
		public static IConnectionSettingsValues GetConnectionSettings(this JsonSerializer serializer)
		{
			var contract = serializer.ContractResolver as ElasticContractResolver;
			if (contract?.ConnectionSettings == null)
				throw new Exception("If you use a custom contract resolver be sure to subclass from " + nameof(ElasticContractResolver));
			return contract.ConnectionSettings;
		}

		public static TConverter GetStatefulConverter<TConverter>(this JsonSerializer serializer)
			where TConverter : JsonConverter
		{
			var resolver = serializer.ContractResolver as ElasticContractResolver;
			var realConverter = resolver?.PiggyBackState?.ActualJsonConverter as TConverter;
			return realConverter;
		}

		public static void WriteProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, object value)
		{
			if (value == null) return;
			writer.WritePropertyName(propertyName);
			serializer.Serialize(writer, value);
		}
	}
}
