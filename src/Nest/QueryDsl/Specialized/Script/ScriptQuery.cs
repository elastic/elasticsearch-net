using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(ScriptQueryConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IScriptQuery : IQuery
	{
		[JsonProperty(PropertyName = "inline")]
		string Inline { get; set; }

		[JsonProperty(PropertyName = "id")]
		Id Id { get; set; }

		[JsonProperty("file")]
		string File { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }
	}

	public class ScriptQuery : QueryBase, IScriptQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public string Inline { get; set; }
		public Id Id { get; set; }
		public string File { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public string Lang { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Script = this;
		internal static bool IsConditionless(IScriptQuery q) =>
			q.Inline.IsNullOrEmpty() && q.Id == null && q.File.IsNullOrEmpty();

	}

	public class ScriptQueryDescriptor<T>
		: QueryDescriptorBase<ScriptQueryDescriptor<T>, IScriptQuery>
		, IScriptQuery where T : class
	{
		protected override bool Conditionless => ScriptQuery.IsConditionless(this);
		string IScriptQuery.Inline { get; set; }
		Id IScriptQuery.Id { get; set; }
		string IScriptQuery.File { get; set; }
		string IScriptQuery.Lang { get; set; }
		Dictionary<string, object> IScriptQuery.Params { get; set; }

		/// <summary>
		/// Inline script to execute
		/// </summary>
		public ScriptQueryDescriptor<T> Inline(string script) => Assign(a => a.Inline = script);

		/// <summary>
		/// Id of an indexed script to execute
		/// </summary
		public ScriptQueryDescriptor<T> Id(string scriptId) => Assign(a => a.Id = scriptId);

		/// <summary>
		/// File name of a script to execute
		/// </summary>
		public ScriptQueryDescriptor<T> File(string scriptFile) => Assign(a => a.File = scriptFile);

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
