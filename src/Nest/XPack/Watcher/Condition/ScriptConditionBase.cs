using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ScriptConditionJsonConverter))]
	public interface IScriptCondition : ICondition
	{
		[JsonProperty("lang")]
		string Lang { get; set; }

		[JsonProperty("params")]
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

	internal class ScriptConditionJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var dict = o.Properties().ToDictionary(p => p.Name, p => p.Value);
			if (!dict.HasAny()) return null;

			IScriptCondition scriptCondition = null;
			if (dict.TryGetValue("inline", out JToken inlineToken))
			{
				var inline = inlineToken.ToString();
				scriptCondition = new InlineScriptCondition(inline);
			}
			if (dict.TryGetValue("source", out JToken sourceToken))
			{
				var inline = sourceToken.ToString();
				scriptCondition = new InlineScriptCondition(inline);
			}
			if (dict.TryGetValue("id", out JToken idToken))
			{
				var id = idToken.ToString();
				scriptCondition = new IndexedScriptCondition(id);
			}

			if (scriptCondition == null) return null;

			if (dict.TryGetValue("lang", out JToken langToken))
				scriptCondition.Lang = langToken.ToString();
			if (dict.TryGetValue("params", out JToken paramsToken))
				scriptCondition.Params = paramsToken.ToObject<Dictionary<string, object>>();

			return scriptCondition;
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
