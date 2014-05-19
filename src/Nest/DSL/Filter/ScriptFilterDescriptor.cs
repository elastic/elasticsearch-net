
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;
using Newtonsoft.Json.Converters;

namespace Nest
{

	[JsonConverter(typeof(ReadAsTypeConverter<ScriptFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IScriptFilter : IFilterBase
	{
		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }
	}

	/// <summary>
	/// A filter allowing to define scripts as filters.
	/// Ex: "doc['num1'].value > 1"
	/// </summary>
	public class ScriptFilterDescriptor : FilterBase, IScriptFilter
	{
		string IScriptFilter.Script { get; set; }

		Dictionary<string, object> IScriptFilter.Params { get; set; }

		string IScriptFilter.Lang { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IScriptFilter)this).Script.IsNullOrEmpty();
			}
		}

		/// <summary>
		/// Filter script.
		/// </summary>
		/// <param name="script">script</param>
		/// <returns>this</returns>
		public ScriptFilterDescriptor Script(string script)
		{
			((IScriptFilter)this).Script = script;
			return this;
		}

		/// <summary>
		/// Scripts are compiled and cached for faster execution.
		/// If the same script can be used, just with different parameters provider,
		/// it is preferable to use the ability to pass parameters to the script itself.
		/// Ex:
		///		Script: "doc['num1'].value > param1"
		///		param: "param1" = 5
		/// </summary>
		/// <param name="paramDictionary">param</param>
		/// <returns>this</returns>
		public ScriptFilterDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			((IScriptFilter)this).Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public ScriptFilterDescriptor Lang(string lang)
		{
			lang.ThrowIfNull("lang");
			((IScriptFilter)this).Lang = lang;
			return this;
		}
	}
}
