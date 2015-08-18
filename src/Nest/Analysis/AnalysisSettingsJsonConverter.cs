using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class AnalysisSettingsJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var analysisValue = (AnalysisSettings)value;
			writer.WriteStartObject();
			{
				if (analysisValue.Analyzers.Count > 0)
				{
					writer.WritePropertyName("analyzer");
					serializer.Serialize(writer, analysisValue.Analyzers);
				}

				if (analysisValue.TokenFilters.Count > 0)
				{
					writer.WritePropertyName("filter");
					serializer.Serialize(writer, analysisValue.TokenFilters);
				}

				if (analysisValue.Tokenizers.Count > 0)
				{
					writer.WritePropertyName("tokenizer");
					serializer.Serialize(writer, analysisValue.Tokenizers);
				}

				if (analysisValue.CharFilters.Count > 0)
				{
					writer.WritePropertyName("char_filter");
					serializer.Serialize(writer, analysisValue.CharFilters);
				}
			}
			writer.WriteEndObject();

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);
			var result = existingValue as AnalysisSettings ?? new AnalysisSettings();

			foreach (var rootProperty in o.Children<JProperty>())
			{
				if (rootProperty.Name.Equals("analyzer", StringComparison.InvariantCultureIgnoreCase))
				{
					result.Analyzers = serializer.Deserialize<IDictionary<string,AnalyzerBase>>(rootProperty.Value.CreateReader());
				}
				else if (rootProperty.Name.Equals("filter", StringComparison.InvariantCultureIgnoreCase))
				{
					result.TokenFilters = serializer.Deserialize<IDictionary<string, TokenFilterBase>>(rootProperty.Value.CreateReader());
				}
				else if (rootProperty.Name.Equals("tokenizer", StringComparison.InvariantCultureIgnoreCase))
				{
					result.Tokenizers = serializer.Deserialize<IDictionary<string, TokenizerBase>>(rootProperty.Value.CreateReader());
				}
				else if (rootProperty.Name.Equals("char_filter", StringComparison.InvariantCultureIgnoreCase))
				{
					result.CharFilters = serializer.Deserialize<IDictionary<string, CharFilterBase>>(rootProperty.Value.CreateReader());
				}
			}

			return result;
		}

		private static Type _type = typeof(AnalysisSettings);
		public override bool CanConvert(Type objectType)
		{
			return objectType == _type;
		}

		public override bool CanRead
		{
			get { return true; }
		}
	}
}