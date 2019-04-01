using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// A script to execute to provide custom computation
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScriptFormatter))]
	public interface IScript
	{
		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		[DataMember(Name = "lang")]
		string Lang { get; set; }

		/// <summary>
		///  Scripts are compiled and cached for faster execution.
		///  If the same script can be used, just with different parameters provided,
		///  it is preferable to use the ability to pass parameters to the script itself.
		/// </summary>
		/// <example>
		/// 	    script: "doc['num1'].value &gt; param1"
		/// 		param: "param1" = 5
		/// </example>
		/// <param name="paramsDictionary">param</param>
		/// <returns>this</returns>
		[DataMember(Name = "params")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysPreservingNullFormatter<string, object>))]
		Dictionary<string, object> Params { get; set; }
	}

	/// <inheritdoc cref="IScript"/>
	public abstract class ScriptBase : IScript
	{
		/// <inheritdoc />
		public string Lang { get; set; }

		/// <inheritdoc />
		public Dictionary<string, object> Params { get; set; }

		/// <summary>
		/// Implicit conversion from <see cref="string" /> to <see cref="InlineScript" />
		/// </summary>
		public static implicit operator ScriptBase(string script) => new InlineScript(script);
	}

	/// <inheritdoc cref="IScript"/>
	public abstract class ScriptDescriptorBase<TDescriptor, TInterface> : DescriptorBase<TDescriptor, TInterface>, IScript
		where TDescriptor : ScriptDescriptorBase<TDescriptor, TInterface>, TInterface, IScript
		where TInterface : class, IScript
	{
		string IScript.Lang { get; set; }
		Dictionary<string, object> IScript.Params { get; set; }

		/// <inheritdoc cref="IScript.Params" />
		public TDescriptor Params(Dictionary<string, object> scriptParams) => Assign(a => a.Params = scriptParams);

		/// <inheritdoc cref="IScript.Params" />
		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IScript.Lang" />
		public TDescriptor Lang(string lang) => Assign(a => a.Lang = lang);

		/// <inheritdoc cref="IScript.Lang" />
		public TDescriptor Lang(ScriptLang lang) => Assign(a => a.Lang = lang.GetStringValue());
	}

	/// <inheritdoc cref="IScript"/>
	public class ScriptDescriptor : DescriptorBase<ScriptDescriptor, IDescriptor>
	{
		/// <summary>
		/// A script that has been indexed in Elasticsearch with the specified id
		/// </summary>
		public IndexedScriptDescriptor Id(string id) => new IndexedScriptDescriptor(id);

		/// <summary>
		/// An inline script to execute
		/// </summary>
		public InlineScriptDescriptor Source(string script) => new InlineScriptDescriptor(script);
	}
}
