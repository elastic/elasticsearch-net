using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ScriptQueryConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IScriptQuery : IQuery
	{
		[JsonProperty("source")]
		string Source { get; set; }

		[Obsolete("Use Source. Inline is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }

		[JsonProperty("id")]
		Id Id { get; set; }

		[JsonProperty("params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty("lang")]
		string Lang { get; set; }
	}

	public class ScriptQuery : QueryBase, IScriptQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public string Source { get; set; }
		[Obsolete("Use Source. Inline is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		public string Inline { get => this.Source; set => this.Source = value; }
		public Id Id { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public string Lang { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Script = this;
		internal static bool IsConditionless(IScriptQuery q) =>
			q.Source.IsNullOrEmpty() && q.Id == null;

	}

	public class ScriptQueryDescriptor<T>
		: QueryDescriptorBase<ScriptQueryDescriptor<T>, IScriptQuery>
		, IScriptQuery where T : class
	{
		protected override bool Conditionless => ScriptQuery.IsConditionless(this);
		string IScriptQuery.Inline { get => Self.Source; set => Self.Source = value; }
		string IScriptQuery.Source { get; set; }
		Id IScriptQuery.Id { get; set; }
		string IScriptQuery.Lang { get; set; }
		Dictionary<string, object> IScriptQuery.Params { get; set; }

		/// <summary> Inline script to execute </summary>
		[Obsolete("Use Source(). Inline() is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		public ScriptQueryDescriptor<T> Inline(string script) => Assign(a => a.Inline = script);

		/// <summary> Inline script to execute </summary>
		public ScriptQueryDescriptor<T> Source(string script) => Assign(a => a.Source = script);

		/// <summary> Id of an indexed script to execute </summary>
		public ScriptQueryDescriptor<T> Id(string scriptId) => Assign(a => a.Id = scriptId);

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
		public ScriptQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary?.Invoke(new FluentDictionary<string, object>()));

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
