using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ScriptJsonConverter))]
	public interface IScript
	{
		[JsonProperty("params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysPreservingNullJsonConverter<string, object>))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty("lang")]
		string Lang { get; set; }
	}

	public abstract class ScriptBase : IScript
	{
		public Dictionary<string, object> Params { get; set; }

		public string Lang { get; set; }

		public static implicit operator ScriptBase(string inline) => new InlineScript(inline);
	}

	public abstract class ScriptDescriptorBase<TDescriptor, TInterface> : DescriptorBase<TDescriptor, TInterface>, IScript
		where TDescriptor : ScriptDescriptorBase<TDescriptor, TInterface>, TInterface, IScript
		where TInterface : class, IScript
	{
		Dictionary<string, object> IScript.Params { get; set; }
		string IScript.Lang { get; set; }

		public TDescriptor Params(Dictionary<string, object> scriptParams) => Assign(a => a.Params = scriptParams);

		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector?.Invoke(new FluentDictionary<string, object>()));

		public TDescriptor Lang(string lang) => Assign(a => a.Lang = lang);
	}

	public class ScriptDescriptor : DescriptorBase<ScriptDescriptor, IDescriptor>
	{
		public IndexedScriptDescriptor Id(string id) => new IndexedScriptDescriptor(id);

		[Obsolete("Indexed() sets a property named id, this is confusing and thats why we intent to remove this in NEST 7.x please use Id()")]
		public IndexedScriptDescriptor Indexed(string id) => new IndexedScriptDescriptor(id);

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public InlineScriptDescriptor Inline(string script) => new InlineScriptDescriptor(script);

		public InlineScriptDescriptor Source(string script) => new InlineScriptDescriptor(script);
	}
}
