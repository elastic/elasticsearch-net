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
	[JsonFormatter(typeof(ScriptConditionFormatter))]
	public interface IScriptCondition : ICondition
	{
		[DataMember(Name = "lang")]
		string Lang { get; set; }

		[DataMember(Name = "params")]
		IDictionary<string, object> Params { get; set; }
	}

	public abstract class ScriptConditionBase : ConditionBase, IScriptCondition
	{
		public string Lang { get; set; }
		public IDictionary<string, object> Params { get; set; }

		internal override void WrapInContainer(IConditionContainer container) => container.Script = this;
	}

	public class ScriptConditionDescriptor : DescriptorBase<ScriptConditionDescriptor, IDescriptor>
	{
		public IndexedScriptConditionDescriptor Id(string id) => new IndexedScriptConditionDescriptor(id);

		public InlineScriptConditionDescriptor Source(string source) => new InlineScriptConditionDescriptor(source);
	}

	public abstract class ScriptConditionDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IScriptCondition
		where TDescriptor : ScriptConditionDescriptorBase<TDescriptor, TInterface>, TInterface, IScriptCondition
		where TInterface : class, IScriptCondition
	{
		string IScriptCondition.Lang { get; set; }
		IDictionary<string, object> IScriptCondition.Params { get; set; }

		public TDescriptor Lang(string lang) => Assign(lang, (a, v) => a.Lang = v);

		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(paramsDictionary(new FluentDictionary<string, object>()), (a, v) => a.Params = v);

		public TDescriptor Params(Dictionary<string, object> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Params = v);
	}

	internal class ScriptConditionFormatter : IJsonFormatter<IScriptCondition>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "source", 0 },
			{ "id", 1 },
			{ "lang", 2 },
			{ "params", 3 }
		};

		public IScriptCondition Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			IScriptCondition scriptCondition = null;
			string language = null;
			Dictionary<string, object> parameters = null;

			while (reader.ReadIsInObject(ref count))
			{
				if (AutomataDictionary.TryGetValue(reader.ReadPropertyNameSegmentRaw(), out var value))
				{
					switch (value)
					{
						case 0:
							scriptCondition = new InlineScriptCondition(reader.ReadString());
							break;
						case 1:
							scriptCondition = new IndexedScriptCondition(reader.ReadString());
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

			if (scriptCondition == null)
				return null;

			scriptCondition.Lang = language;
			scriptCondition.Params = parameters;
			return scriptCondition;
		}

		public void Serialize(ref JsonWriter writer, IScriptCondition value, IJsonFormatterResolver formatterResolver)
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
				case IIndexedScriptCondition indexedScriptCondition:
					writer.WritePropertyName("id");
					writer.WriteString(indexedScriptCondition.Id);
					written = true;
					break;
				case IInlineScriptCondition inlineScriptCondition:
					writer.WritePropertyName("source");
					writer.WriteString(inlineScriptCondition.Source);
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
				var formatter = formatterResolver.GetFormatter<IDictionary<string, object>>();
				formatter.Serialize(ref writer, value.Params, formatterResolver);
			}

			writer.WriteEndObject();
		}
	}
}
