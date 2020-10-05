// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport.Extensions;
using Nest.Utf8Json;

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
		public TDescriptor Params(Dictionary<string, object> scriptParams) => Assign(scriptParams, (a, v) => a.Params = v);

		/// <inheritdoc cref="IScript.Params" />
		public TDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IScript.Lang" />
		public TDescriptor Lang(string lang) => Assign(lang, (a, v) => a.Lang = v);

		/// <inheritdoc cref="IScript.Lang" />
		public TDescriptor Lang(ScriptLang lang) => Assign(lang.GetStringValue(), (a, v) => a.Lang = v);
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
