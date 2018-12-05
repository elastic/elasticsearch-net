using System;
using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class PipelineFormatter : IJsonFormatter<Pipeline>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "description", 0 },
			{ "processors", 1 },
			{ "on_failure", 2 }
		};

		private static readonly AutomataDictionary Processors = new AutomataDictionary
		{
			{ "attachment", 0 },
			{ "append", 1 },
			{ "convert", 2 },
			{ "date", 3 },
			{ "date_index_name", 4 },
			{ "dot_expander", 5 },
			{ "fail", 6 },
			{ "foreach", 7 },
			{ "json", 8 },
			{ "user_agent", 9 },
			{ "kv", 10 },
			{ "geoip", 11 },
			{ "grok", 12 },
			{ "gsub", 13 },
			{ "join", 14 },
			{ "lowercase", 15 },
			{ "remove", 16 },
			{ "rename", 17 },
			{ "script", 18 },
			{ "set", 19 },
			{ "sort", 20 },
			{ "split", 21 },
			{ "trim", 22 },
			{ "uppercase", 23 },
			{ "urldecode", 24 },
			{ "bytes", 25 }
		};

		public Pipeline Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var count = 0;
			var pipeline = new Pipeline();

			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							pipeline.Description = reader.ReadString();
							break;
						case 1:
							pipeline.Processors = GetProcessors(ref reader, formatterResolver);
							break;
						case 2:
							pipeline.OnFailure = GetProcessors(ref reader, formatterResolver);
							break;
					}
				}
			}

			return pipeline;
		}

		public void Serialize(ref JsonWriter writer, Pipeline value, IJsonFormatterResolver formatterResolver) => throw new NotSupportedException();

		private List<IProcessor> GetProcessors(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var processors = new List<IProcessor>();
			var count = 0;
			while (reader.ReadIsInArray(ref count))
			{
				var propertyCount = 0;
				while (reader.ReadIsInObject(ref propertyCount))
				{
					var processorName = reader.ReadPropertyNameSegmentRaw();
					if (Processors.TryGetValue(processorName, out var value))
					{
						switch (value)
						{
							case 0:
								processors.Add(Deserialize<AttachmentProcessor>(ref reader, formatterResolver));
								break;
							case 1:
								processors.Add(Deserialize<AppendProcessor>(ref reader, formatterResolver));
								break;
							case 2:
								processors.Add(Deserialize<ConvertProcessor>(ref reader, formatterResolver));
								break;
							case 3:
								processors.Add(Deserialize<DateProcessor>(ref reader, formatterResolver));
								break;
							case 4:
								processors.Add(Deserialize<DateIndexNameProcessor>(ref reader, formatterResolver));
								break;
							case 5:
								processors.Add(Deserialize<DotExpanderProcessor>(ref reader, formatterResolver));
								break;
							case 6:
								processors.Add(Deserialize<FailProcessor>(ref reader, formatterResolver));
								break;
							case 7:
								processors.Add(Deserialize<ForeachProcessor>(ref reader, formatterResolver));
								break;
							case 8:
								processors.Add(Deserialize<JsonProcessor>(ref reader, formatterResolver));
								break;
							case 9:
								processors.Add(Deserialize<UserAgentProcessor>(ref reader, formatterResolver));
								break;
							case 10:
								processors.Add(Deserialize<KeyValueProcessor>(ref reader, formatterResolver));
								break;
							case 11:
								processors.Add(Deserialize<GeoIpProcessor>(ref reader, formatterResolver));
								break;
							case 12:
								processors.Add(Deserialize<GrokProcessor>(ref reader, formatterResolver));
								break;
							case 13:
								processors.Add(Deserialize<GsubProcessor>(ref reader, formatterResolver));
								break;
							case 14:
								processors.Add(Deserialize<JoinProcessor>(ref reader, formatterResolver));
								break;
							case 15:
								processors.Add(Deserialize<LowercaseProcessor>(ref reader, formatterResolver));
								break;
							case 16:
								processors.Add(Deserialize<RemoveProcessor>(ref reader, formatterResolver));
								break;
							case 17:
								processors.Add(Deserialize<RenameProcessor>(ref reader, formatterResolver));
								break;
							case 18:
								processors.Add(Deserialize<ScriptProcessor>(ref reader, formatterResolver));
								break;
							case 19:
								processors.Add(Deserialize<SetProcessor>(ref reader, formatterResolver));
								break;
							case 20:
								processors.Add(Deserialize<SortProcessor>(ref reader, formatterResolver));
								break;
							case 21:
								processors.Add(Deserialize<SplitProcessor>(ref reader, formatterResolver));
								break;
							case 22:
								processors.Add(Deserialize<TrimProcessor>(ref reader, formatterResolver));
								break;
							case 23:
								processors.Add(Deserialize<UppercaseProcessor>(ref reader, formatterResolver));
								break;
							case 24:
								processors.Add(Deserialize<UrlDecodeProcessor>(ref reader, formatterResolver));
								break;
							case 25:
								processors.Add(Deserialize<BytesProcessor>(ref reader, formatterResolver));
								break;
						}
					}
				}
			}

			return processors;
		}

		private static TProcessor Deserialize<TProcessor>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TProcessor : IProcessor
		{
			var formatter = formatterResolver.GetFormatter<TProcessor>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
