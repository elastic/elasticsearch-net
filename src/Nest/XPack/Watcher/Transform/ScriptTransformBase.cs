// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;
namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScriptTransformFormatter))]
	public interface IScriptTransform : ITransform
	{
		[DataMember(Name = "lang")]
		string Lang { get; set; }

		[DataMember(Name = "params")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, object>))]
		Dictionary<string, object> Params { get; set; }
	}

	public abstract class ScriptTransformBase : TransformBase, IScriptTransform
	{
		public string Lang { get; set; }
		public Dictionary<string, object> Params { get; set; }

		internal override void WrapInContainer(ITransformContainer container) => container.Script = this;
	}

	public abstract class ScriptTransformDescriptorBase<TDescriptor, TInterface> : DescriptorBase<TDescriptor, TInterface>, IScriptTransform
		where TDescriptor : ScriptTransformDescriptorBase<TDescriptor, TInterface>, TInterface, IScriptTransform
		where TInterface : class, IScriptTransform
	{
		string IScriptTransform.Lang { get; set; }
		Dictionary<string, object> IScriptTransform.Params { get; set; }

		public TDescriptor Params(Dictionary<string, object> scriptParams) => Assign(scriptParams, (a, v) => a.Params = v);

		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(paramsSelector, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, object>()));

		public TDescriptor Lang(string lang) => Assign(lang, (a, v) => a.Lang = v);
	}

	public class ScriptTransformDescriptor : DescriptorBase<ScriptTransformDescriptor, IDescriptor>
	{
		public IndexedScriptTransformDescriptor Id(string id) => new IndexedScriptTransformDescriptor(id);

		public InlineScriptTransformDescriptor Source(string source) => new InlineScriptTransformDescriptor(source);
	}

	internal class ScriptTransformFormatter : IJsonFormatter<IScriptTransform>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "source", 0 },
			{ "id", 1 },
			{ "lang", 2 },
			{ "params", 3 }
		};

		public IScriptTransform Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			IScriptTransform scriptTransform = null;
			string language = null;
			Dictionary<string, object> parameters = null;

			while (reader.ReadIsInObject(ref count))
			{
				if (AutomataDictionary.TryGetValue(reader.ReadPropertyNameSegmentRaw(), out var value))
				{
					switch (value)
					{
						case 0:
							scriptTransform = new InlineScriptTransform(reader.ReadString());
							break;
						case 1:
							scriptTransform = new IndexedScriptTransform(reader.ReadString());
							break;
						case 2:
							language = reader.ReadString();
							break;
						case 3:
							parameters = formatterResolver.GetFormatter<Dictionary<string, object>>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
			}

			if (scriptTransform == null)
				return null;

			scriptTransform.Lang = language;
			scriptTransform.Params = parameters;
			return scriptTransform;
		}

		public void Serialize(ref JsonWriter writer, IScriptTransform value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var written = false;

			switch (value)
			{
				case IIndexedScriptTransform indexedScriptTransform:
					writer.WritePropertyName("id");
					writer.WriteString(indexedScriptTransform.Id);
					written = true;
					break;
				case IInlineScriptTransform inlineScriptTransform:
					writer.WritePropertyName("source");
					writer.WriteString(inlineScriptTransform.Source);
					written = true;
					break;
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
		}
	}
}
