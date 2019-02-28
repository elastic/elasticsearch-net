using System.Collections.Generic;
using Elasticsearch.Net;


namespace Nest
{
	internal class ScriptQueryFormatter : IJsonFormatter<IScriptQuery>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "_name", 0 },
			{ "boost", 1 },
			{ "script", 2 }
		};

		private static readonly AutomataDictionary ScriptFields = new AutomataDictionary
		{
			{ "id", 0 },
			{ "source", 1 },
			{ "inline", 1 },
			{ "lang", 2 },
			{ "params", 3 }
		};

		public IScriptQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var query = new ScriptQuery();

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							query.Name = reader.ReadString();
							break;
						case 1:
							query.Boost = reader.ReadDouble();
							break;
						case 2:
							var scriptCount = 0;
							while (reader.ReadIsInObject(ref scriptCount))
							{
								var scriptProperty = reader.ReadPropertyNameSegmentRaw();
								if (ScriptFields.TryGetValue(scriptProperty, out var scriptValue))
								{
									switch (scriptValue)
									{
										case 0:
											query.Id = reader.ReadString();
											break;
										case 1:
											query.Source = reader.ReadString();
											break;
										case 2:
											query.Lang = reader.ReadString();
											break;
										case 3:
											query.Params = formatterResolver.GetFormatter<Dictionary<string, object>>()
												.Deserialize(ref reader, formatterResolver);
											break;
									}
								}
							}
							break;
					}
				}
			}

			return query;
		}

		public void Serialize(ref JsonWriter writer, IScriptQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) return;

			writer.WriteBeginObject();

			var written = false;
			if (!value.Name.IsNullOrEmpty())
			{
				writer.WritePropertyName("_name");
				writer.WriteString(value.Name);
				written = true;
			}
			if (value.Boost != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("boost");
				writer.WriteDouble(value.Boost.Value);
				written = true;
			}

			if (written)
			{
				writer.WriteValueSeparator();
				written = false;
			}

			writer.WritePropertyName("script");
			writer.WriteBeginObject();

			if (value.Id != null)
			{
				writer.WritePropertyName("id");
				formatterResolver.GetFormatter<Id>().Serialize(ref writer, value.Id, formatterResolver);
				written = true;
			}
			if (value.Source != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("source");
				writer.WriteString(value.Source);
				written = true;
			}

			if (value.Lang != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("lang");
				writer.WriteString(value.Lang);
				written = true;
			}
			if (value.Params != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("params");
				var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
				formatter.Serialize(ref writer, value.Params, formatterResolver);
			}
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
