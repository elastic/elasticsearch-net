using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ScriptJsonConverter))]
	public interface IScript
	{
		[JsonProperty("params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty("lang")]
		string Lang { get; set; }

	}

	public abstract class Script : IScript
	{
		public Dictionary<string, object> Params { get; set; }

		public string Lang { get; set; }

		public static implicit operator Script(string inline) => new InlineScript(inline);
	}

	public class ScriptDescriptorBase<TDescriptor, TInterface> : DescriptorBase<TDescriptor, TInterface>, IScript
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
		public IFileScript File(string file, Func<FileScriptDescriptor, IFileScript> fileScript = null) =>
			fileScript.InvokeOrDefault(new FileScriptDescriptor().File(file));

		public IIndexedScript Indexed(string id, Func<IndexedScriptDescriptor, IIndexedScript> indexedScript = null) =>
			indexedScript.InvokeOrDefault(new IndexedScriptDescriptor().Id(id));

		public IInlineScript Inline(string script, Func<InlineScriptDescriptor, IInlineScript> inlineScript = null) =>
			inlineScript.InvokeOrDefault(new InlineScriptDescriptor().Inline(script));
	}
}
