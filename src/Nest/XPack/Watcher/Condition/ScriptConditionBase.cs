using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;
using Utf8Json.Internal;

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

		[Obsolete("Indexed() sets a property named id, this is confusing and thats why we intent to remove this in NEST 7.x please use Id()")]
		public IndexedScriptConditionDescriptor Indexed(string id) => new IndexedScriptConditionDescriptor(id);

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public InlineScriptConditionDescriptor Inline(string script) => new InlineScriptConditionDescriptor(script);

		public InlineScriptConditionDescriptor Source(string source) => new InlineScriptConditionDescriptor(source);
	}

	public abstract class ScriptConditionDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IScriptCondition
		where TDescriptor : ScriptConditionDescriptorBase<TDescriptor, TInterface>, TInterface, IScriptCondition
		where TInterface : class, IScriptCondition
	{
		string IScriptCondition.Lang { get; set; }
		IDictionary<string, object> IScriptCondition.Params { get; set; }

		public TDescriptor Lang(string lang) => Assign(a => a.Lang = lang);

		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary(new FluentDictionary<string, object>()));

		public TDescriptor Params(Dictionary<string, object> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary);
	}

	internal class ScriptConditionFormatter : IJsonFormatter<IScriptCondition>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "inline", 0 },
			{ "source", 1 },
			{ "id", 2 },
			{ "lang", 3 },
			{ "params", 4 }
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
						case 1:
							scriptCondition = new InlineScriptCondition(reader.ReadString());
							break;
						case 2:
							scriptCondition = new IndexedScriptCondition(reader.ReadString());
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

			if (scriptCondition == null)
				return null;

			scriptCondition.Lang = language;
			scriptCondition.Params = parameters;
			return scriptCondition;
		}

		public void Serialize(ref JsonWriter writer, IScriptCondition value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
