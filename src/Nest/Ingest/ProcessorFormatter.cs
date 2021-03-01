// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	internal class ProcessorFormatter : IJsonFormatter<IProcessor>
	{
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
			{ "bytes", 25 },
			{ "dissect", 26 },
			{ "set_security_user", 27 },
			{ "pipeline", 28 },
			{ "drop", 29 },
			{ "circle", 30 },
			{ "enrich", 31 },
			{ "csv", 32 },
			{ "uri_parts", 33 },
			{ "fingerprint", 34 },
			{ "community_id", 35 },
			{ "network_direction", 36 }
		};

		public IProcessor Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			// read opening {
			reader.ReadNext();

			IProcessor processor = null;

			var processorName = reader.ReadPropertyNameSegmentRaw();
			if (Processors.TryGetValue(processorName, out var value))
			{
				switch (value)
				{
					case 0:
						processor = Deserialize<AttachmentProcessor>(ref reader, formatterResolver);
						break;
					case 1:
						processor = Deserialize<AppendProcessor>(ref reader, formatterResolver);
						break;
					case 2:
						processor = Deserialize<ConvertProcessor>(ref reader, formatterResolver);
						break;
					case 3:
						processor = Deserialize<DateProcessor>(ref reader, formatterResolver);
						break;
					case 4:
						processor = Deserialize<DateIndexNameProcessor>(ref reader, formatterResolver);
						break;
					case 5:
						processor = Deserialize<DotExpanderProcessor>(ref reader, formatterResolver);
						break;
					case 6:
						processor = Deserialize<FailProcessor>(ref reader, formatterResolver);
						break;
					case 7:
						processor = Deserialize<ForeachProcessor>(ref reader, formatterResolver);
						break;
					case 8:
						processor = Deserialize<JsonProcessor>(ref reader, formatterResolver);
						break;
					case 9:
						processor = Deserialize<UserAgentProcessor>(ref reader, formatterResolver);
						break;
					case 10:
						processor = Deserialize<KeyValueProcessor>(ref reader, formatterResolver);
						break;
					case 11:
						processor = Deserialize<GeoIpProcessor>(ref reader, formatterResolver);
						break;
					case 12:
						processor = Deserialize<GrokProcessor>(ref reader, formatterResolver);
						break;
					case 13:
						processor = Deserialize<GsubProcessor>(ref reader, formatterResolver);
						break;
					case 14:
						processor = Deserialize<JoinProcessor>(ref reader, formatterResolver);
						break;
					case 15:
						processor = Deserialize<LowercaseProcessor>(ref reader, formatterResolver);
						break;
					case 16:
						processor = Deserialize<RemoveProcessor>(ref reader, formatterResolver);
						break;
					case 17:
						processor = Deserialize<RenameProcessor>(ref reader, formatterResolver);
						break;
					case 18:
						processor = Deserialize<ScriptProcessor>(ref reader, formatterResolver);
						break;
					case 19:
						processor = Deserialize<SetProcessor>(ref reader, formatterResolver);
						break;
					case 20:
						processor = Deserialize<SortProcessor>(ref reader, formatterResolver);
						break;
					case 21:
						processor = Deserialize<SplitProcessor>(ref reader, formatterResolver);
						break;
					case 22:
						processor = Deserialize<TrimProcessor>(ref reader, formatterResolver);
						break;
					case 23:
						processor = Deserialize<UppercaseProcessor>(ref reader, formatterResolver);
						break;
					case 24:
						processor = Deserialize<UrlDecodeProcessor>(ref reader, formatterResolver);
						break;
					case 25:
						processor = Deserialize<BytesProcessor>(ref reader, formatterResolver);
						break;
					case 26:
						processor = Deserialize<DissectProcessor>(ref reader, formatterResolver);
						break;
					case 27:
						processor = Deserialize<SetSecurityUserProcessor>(ref reader, formatterResolver);
						break;
					case 28:
						processor = Deserialize<PipelineProcessor>(ref reader, formatterResolver);
						break;
					case 29:
						processor = Deserialize<DropProcessor>(ref reader, formatterResolver);
						break;
					case 30:
						processor = Deserialize<CircleProcessor>(ref reader, formatterResolver);
						break;
					case 31:
						processor = Deserialize<EnrichProcessor>(ref reader, formatterResolver);
						break;
					case 32:
						processor = Deserialize<CsvProcessor>(ref reader, formatterResolver);
						break;
					case 33:
						processor = Deserialize<UriPartsProcessor>(ref reader, formatterResolver);
						break;
					case 34:
						processor = Deserialize<FingerprintProcessor>(ref reader, formatterResolver);
						break;
					case 35:
						processor = Deserialize<NetworkCommunityIdProcessor>(ref reader, formatterResolver);
						break;
					case 36:
						processor = Deserialize<NetworkDirectionProcessor>(ref reader, formatterResolver);
						break;
				}
			}
			else
				reader.ReadNextBlock();

			reader.ReadIsEndObjectWithVerify();
			return processor;
		}

		public void Serialize(ref JsonWriter writer, IProcessor value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Name == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Name);

			switch (value.Name)
			{
				case "attachment":
					Serialize<IAttachmentProcessor>(ref writer, value, formatterResolver);
					break;
				case "append":
					Serialize<IAppendProcessor>(ref writer, value, formatterResolver);
					break;
				case "csv":
					Serialize<ICsvProcessor>(ref writer, value, formatterResolver);
					break;
				case "convert":
					Serialize<IConvertProcessor>(ref writer, value, formatterResolver);
					break;
				case "date": Serialize<IDateProcessor>(ref writer, value, formatterResolver);
					break;
				case "date_index_name":
					Serialize<IDateIndexNameProcessor>(ref writer, value, formatterResolver);
					break;
				case "dot_expander":
					Serialize<IDotExpanderProcessor>(ref writer, value, formatterResolver);
					break;
				case "enrich":
					Serialize<IEnrichProcessor>(ref writer, value, formatterResolver);
					break;
				case "fail":
					Serialize<IFailProcessor>(ref writer, value, formatterResolver);
					break;
				case "foreach":
					Serialize<IForeachProcessor>(ref writer, value, formatterResolver);
					break;
				case "json":
					Serialize<IJsonProcessor>(ref writer, value, formatterResolver);
					break;
				case "user_agent":
					Serialize<IUserAgentProcessor>(ref writer, value, formatterResolver);
					break;
				case "kv":
					Serialize<IKeyValueProcessor>(ref writer, value, formatterResolver);
					break;
				case "geoip":
					Serialize<IGeoIpProcessor>(ref writer, value, formatterResolver);
					break;
				case "grok":
					Serialize<IGrokProcessor>(ref writer, value, formatterResolver);
					break;
				case "gsub":
					Serialize<IGsubProcessor>(ref writer, value, formatterResolver);
					break;
				case "join":
					Serialize<IJoinProcessor>(ref writer, value, formatterResolver);
					break;
				case "lowercase":
					Serialize<ILowercaseProcessor>(ref writer, value, formatterResolver);
					break;
				case "remove":
					Serialize<IRemoveProcessor>(ref writer, value, formatterResolver);
					break;
				case "rename":
					Serialize<IRenameProcessor>(ref writer, value, formatterResolver);
					break;
				case "script":
					Serialize<IScriptProcessor>(ref writer, value, formatterResolver);
					break;
				case "set":
					Serialize<ISetProcessor>(ref writer, value, formatterResolver);
					break;
				case "sort":
					Serialize<ISortProcessor>(ref writer, value, formatterResolver);
					break;
				case "split":
					Serialize<ISplitProcessor>(ref writer, value, formatterResolver);
					break;
				case "trim":
					Serialize<ITrimProcessor>(ref writer, value, formatterResolver);
					break;
				case "uppercase":
					Serialize<IUppercaseProcessor>(ref writer, value, formatterResolver);
					break;
				case "urldecode":
					Serialize<IUrlDecodeProcessor>(ref writer, value, formatterResolver);
					break;
				case "bytes":
					Serialize<IBytesProcessor>(ref writer, value, formatterResolver);
					break;
				case "dissect":
					Serialize<IDissectProcessor>(ref writer, value, formatterResolver);
					break;
				case "set_security_user":
					Serialize<ISetSecurityUserProcessor>(ref writer, value, formatterResolver);
					break;
				case "pipeline":
					Serialize<IPipelineProcessor>(ref writer, value, formatterResolver);
					break;
				case "drop":
					Serialize<IDropProcessor>(ref writer, value, formatterResolver);
					break;
				case "circle":
					Serialize<ICircleProcessor>(ref writer, value, formatterResolver);
					break;
				case "uri_parts":
					Serialize<IUriPartsProcessor>(ref writer, value, formatterResolver);
					break;
				case "fingerprint":
					Serialize<IFingerprintProcessor>(ref writer, value, formatterResolver);
					break;
				case "community_id":
					Serialize<INetworkCommunityIdProcessor>(ref writer, value, formatterResolver);
					break;
				case "network_direction":
					Serialize<INetworkDirectionProcessor>(ref writer, value, formatterResolver);
					break;
				default:
					var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IProcessor>();
					formatter.Serialize(ref writer, value, formatterResolver);
					break;
			}

			writer.WriteEndObject();
		}

		private static TProcessor Deserialize<TProcessor>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TProcessor : IProcessor
		{
			var formatter = formatterResolver.GetFormatter<TProcessor>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		private static void Serialize<TProcessor>(ref JsonWriter writer, IProcessor value, IJsonFormatterResolver formatterResolver)
			where TProcessor : class, IProcessor
		{
			var processorFormatter = formatterResolver.GetFormatter<TProcessor>();
			processorFormatter.Serialize(ref writer, value as TProcessor, formatterResolver);
		}
	}
}
