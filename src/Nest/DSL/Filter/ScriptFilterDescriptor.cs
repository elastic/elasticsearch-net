
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
		string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		[JsonConverter(typeof (StringEnumConverter))]
		Lang? _Lang { get; set; }
	}

	/// <summary>
	/// A filter allowing to define scripts as filters.
	/// Ex: "doc['num1'].value > 1"
	/// </summary>
	public class ScriptFilterDescriptor : FilterBase, IScriptFilter
	{
		string IScriptFilter._Script { get; set; }

		Dictionary<string, object> IScriptFilter._Params { get; set; }

		Lang? IScriptFilter._Lang { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IScriptFilter)this)._Script.IsNullOrEmpty();
			}
		}

		/// <summary>
		/// Filter script.
		/// </summary>
		/// <param name="script">script</param>
		/// <returns>this</returns>
		public ScriptFilterDescriptor Script(string script)
		{
			((IScriptFilter)this)._Script = script;
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
			((IScriptFilter)this)._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public ScriptFilterDescriptor Lang(Lang lang)
		{
			lang.ThrowIfNull("lang");
			((IScriptFilter)this)._Lang = lang;
			return this;
		}
	}
}
