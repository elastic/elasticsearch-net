using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ScriptJsonConverter))]
	public interface IScript
	{
		/// <summary>
		/// Scripts are compiled and cached for faster execution.
		/// If the same script can be used, just with different parameters provided,
		/// it is preferable to use the ability to pass parameters to the script itself.
		/// </summary>
		/// <example>
		///	    script: "doc['num1'].value &gt; param1"
		///		param: "param1" = 5
		/// </example>
		/// <param name="paramsDictionary">param</param>
		/// <returns>this</returns>
		[JsonProperty("params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysPreservingNullJsonConverter<string, object>))]
		Dictionary<string, object> Params { get; set; }

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		[JsonProperty("lang")]
		string Lang { get; set; }
	}

	public abstract class ScriptBase : IScript
	{
		public Dictionary<string, object> Params { get; set; }

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public string Lang { get; set; }

		/// <summary>
		/// Implicit conversion from <see cref="string"/> to <see cref="InlineScript"/>
		/// </summary>
		public static implicit operator ScriptBase(string script) => new InlineScript(script);
	}

	public abstract class ScriptDescriptorBase<TDescriptor, TInterface> : DescriptorBase<TDescriptor, TInterface>, IScript
		where TDescriptor : ScriptDescriptorBase<TDescriptor, TInterface>, TInterface, IScript
		where TInterface : class, IScript
	{
		Dictionary<string, object> IScript.Params { get; set; }
		string IScript.Lang { get; set; }

		/// <summary>
		/// Scripts are compiled and cached for faster execution.
		/// If the same script can be used, just with different parameters provided,
		/// it is preferable to use the ability to pass parameters to the script itself.
		/// </summary>
		/// <example>
		///	    script: "doc['num1'].value &gt; param1"
		///		param: "param1" = 5
		/// </example>
		/// <param name="paramsDictionary">param</param>
		/// <returns>this</returns>
		public TDescriptor Params(Dictionary<string, object> scriptParams) => Assign(a => a.Params = scriptParams);

		/// <summary>
		/// Scripts are compiled and cached for faster execution.
		/// If the same script can be used, just with different parameters provided,
		/// it is preferable to use the ability to pass parameters to the script itself.
		/// </summary>
		/// <example>
		///	    script: "doc['num1'].value &gt; param1"
		///		param: "param1" = 5
		/// </example>
		/// <param name="paramsDictionary">param</param>
		/// <returns>this</returns>
		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary?.Invoke(new FluentDictionary<string, object>()));

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public TDescriptor Lang(string lang) => Assign(a => a.Lang = lang);

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public TDescriptor Lang(ScriptLang lang) => Assign(a => a.Lang = lang.GetStringValue());
	}

	public class ScriptDescriptor : DescriptorBase<ScriptDescriptor, IDescriptor>
	{
		public IndexedScriptDescriptor Id(string id) => new IndexedScriptDescriptor(id);

		[Obsolete("Use Id(). Indexed() sets a property named id, which is confusing. Will be removed in NEST 7.x")]
		public IndexedScriptDescriptor Indexed(string id) => new IndexedScriptDescriptor(id);

		[Obsolete("Use Source(). Inline() is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		public InlineScriptDescriptor Inline(string script) => new InlineScriptDescriptor(script);

		public InlineScriptDescriptor Source(string script) => new InlineScriptDescriptor(script);
	}
}
