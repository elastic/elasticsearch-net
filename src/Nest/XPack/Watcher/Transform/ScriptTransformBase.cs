using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScriptTransformFormatter))]
	public interface IScriptTransform : ITransform
	{
		[DataMember(Name = "lang")]
		string Lang { get; set; }

		[DataMember(Name = "params")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, object>))]
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

		public TDescriptor Params(Dictionary<string, object> scriptParams) => Assign(a => a.Params = scriptParams);

		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector?.Invoke(new FluentDictionary<string, object>()));

		public TDescriptor Lang(string lang) => Assign(a => a.Lang = lang);
	}

	public class ScriptTransformDescriptor : DescriptorBase<ScriptTransformDescriptor, IDescriptor>
	{
		public IndexedScriptTransformDescriptor Id(string id) => new IndexedScriptTransformDescriptor(id);

		[Obsolete("Indexed() sets a property named id, this is confusing and thats why we intend to remove this in NEST 7.x please use Id()")]
		public IndexedScriptTransformDescriptor Indexed(string id) => new IndexedScriptTransformDescriptor(id);

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public InlineScriptTransformDescriptor Inline(string script) => new InlineScriptTransformDescriptor(script);

		public InlineScriptTransformDescriptor Source(string source) => new InlineScriptTransformDescriptor(source);
	}

	internal class ScriptTransformFormatter : IJsonFormatter<IScriptTransform>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "inline", 0 },
			{ "source", 1 },
			{ "id", 2 },
			{ "lang", 3 },
			{ "params", 4 }
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
						case 1:
							scriptTransform = new InlineScriptTransform(reader.ReadString());
							break;
						case 2:
							scriptTransform = new IndexedScriptTransform(reader.ReadString());
							break;
						case 3:
							language = reader.ReadString();
							break;
						case 4:
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
