using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScriptQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IScriptQuery : IQuery
	{
		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }
		[JsonProperty(PropertyName = "script_id")]
		string ScriptId { get; set; }

		[JsonProperty("script_file")]
		string ScriptFile { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }
	}

	public class ScriptQuery : QueryBase, IScriptQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Script { get; set; }
		public string ScriptId { get; set; }
		public string ScriptFile { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public string Lang { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Script = this;
		internal static bool IsConditionless(IScriptQuery q) => q.Script.IsNullOrEmpty();
	}

	public class ScriptQueryDescriptor<T> 
		: QueryDescriptorBase<ScriptQueryDescriptor<T>, IScriptQuery>
		, IScriptQuery where T : class
	{
		bool IQuery.Conditionless => ScriptQuery.IsConditionless(this);
		string IScriptQuery.Script { get; set; }
		string IScriptQuery.ScriptId { get; set; }
		string IScriptQuery.ScriptFile { get; set; }
		string IScriptQuery.Lang { get; set; }
		Dictionary<string, object> IScriptQuery.Params { get; set; }

		/// <summary>
		/// Inline script to execute
		/// </summary>
		public ScriptQueryDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		/// <summary>
		/// Id of an indexed script to execute
		/// </summary
		public ScriptQueryDescriptor<T> ScriptId(string scriptId) => Assign(a => a.ScriptId = scriptId);

		/// <summary>
		/// File name of a script to execute
		/// </summary>
		public ScriptQueryDescriptor<T> ScriptFile(string scriptFile) => Assign(a => a.ScriptFile = scriptFile);

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
		public ScriptQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary(new FluentDictionary<string, object>()));

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public ScriptQueryDescriptor<T> Lang(string lang) => Assign(a => a.Lang = lang);

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public ScriptQueryDescriptor<T> Lang(ScriptLang lang) => Assign(a => a.Lang = lang.GetStringValue());
	}
}
