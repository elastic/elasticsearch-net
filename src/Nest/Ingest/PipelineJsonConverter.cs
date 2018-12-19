using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PipelineJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var root = JObject.Load(reader);
			var pipeline = new Pipeline { Description = root["description"]?.ToString() };
			if (root["processors"] != null)
				pipeline.Processors = GetProcessors(root["processors"], serializer);
			if (root["on_failure"] != null)
				pipeline.OnFailure = GetProcessors(root["on_failure"], serializer);
			return pipeline;
		}

		private List<IProcessor> GetProcessors(JToken jsonProcessors, JsonSerializer serializer)
		{
			var processors = new List<IProcessor>();
			foreach (var jsonProcessor in jsonProcessors.ToArray())
			{
				var processorName = jsonProcessor.ToObject<JObject>().Properties().First().Name;
				switch (processorName)
				{
					case "attachment":
						processors.Add(jsonProcessor.ToObject<AttachmentProcessor>(serializer));
						break;
					case "append":
						processors.Add(jsonProcessor.ToObject<AppendProcessor>(serializer));
						break;
					case "convert":
						processors.Add(jsonProcessor.ToObject<ConvertProcessor>(serializer));
						break;
					case "date":
						processors.Add(jsonProcessor.ToObject<DateProcessor>(serializer));
						break;
					case "date_index_name":
						processors.Add(jsonProcessor.ToObject<DateIndexNameProcessor>(serializer));
						break;
					case "dot_expander":
						processors.Add(jsonProcessor.ToObject<DotExpanderProcessor>(serializer));
						break;
					case "fail":
						processors.Add(jsonProcessor.ToObject<FailProcessor>(serializer));
						break;
					case "foreach":
						processors.Add(jsonProcessor.ToObject<ForeachProcessor>(serializer));
						break;
					case "json":
						processors.Add(jsonProcessor.ToObject<JsonProcessor>(serializer));
						break;
					case "user_agent":
						processors.Add(jsonProcessor.ToObject<UserAgentProcessor>(serializer));
						break;
					case "kv":
						processors.Add(jsonProcessor.ToObject<KeyValueProcessor>(serializer));
						break;
					case "geoip":
						processors.Add(jsonProcessor.ToObject<GeoIpProcessor>(serializer));
						break;
					case "grok":
						processors.Add(jsonProcessor.ToObject<GrokProcessor>(serializer));
						break;
					case "gsub":
						processors.Add(jsonProcessor.ToObject<GsubProcessor>(serializer));
						break;
					case "join":
						processors.Add(jsonProcessor.ToObject<JoinProcessor>(serializer));
						break;
					case "lowercase":
						processors.Add(jsonProcessor.ToObject<LowercaseProcessor>(serializer));
						break;
					case "remove":
						processors.Add(jsonProcessor.ToObject<RemoveProcessor>(serializer));
						break;
					case "rename":
						processors.Add(jsonProcessor.ToObject<RenameProcessor>(serializer));
						break;
					case "script":
						processors.Add(jsonProcessor.ToObject<ScriptProcessor>(serializer));
						break;
					case "set":
						processors.Add(jsonProcessor.ToObject<SetProcessor>(serializer));
						break;
					case "sort":
						processors.Add(jsonProcessor.ToObject<SortProcessor>(serializer));
						break;
					case "split":
						processors.Add(jsonProcessor.ToObject<SplitProcessor>(serializer));
						break;
					case "trim":
						processors.Add(jsonProcessor.ToObject<TrimProcessor>(serializer));
						break;
					case "uppercase":
						processors.Add(jsonProcessor.ToObject<UppercaseProcessor>(serializer));
						break;
					case "urldecode":
						processors.Add(jsonProcessor.ToObject<UrlDecodeProcessor>(serializer));
						break;
					case "bytes":
						processors.Add(jsonProcessor.ToObject<BytesProcessor>(serializer));
						break;
					case "dissect":
						processors.Add(jsonProcessor.ToObject<DissectProcessor>(serializer));
						break;
				}
			}
			return processors;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
