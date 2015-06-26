
using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(ReadAsTypeConverter<ScriptQueryDescriptor>))]
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
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }
	}

	public class ScriptFilter : PlainQuery, IScriptQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Script = this;
		}

		public string Script { get; set; }
	    public string ScriptId { get; set; }
		public string ScriptFile { get; set; }
	    public Dictionary<string, object> Params { get; set; }
		public string Lang { get; set; }

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
	}

	/// <summary>
	/// A filter allowing to define scripts as filters.
	/// Ex: "doc['num1'].value > 1"
	/// </summary>
	public class ScriptQueryDescriptor : IScriptQuery
	{
		private IScriptQuery Self { get { return this; } }

        string IScriptQuery.Script { get; set; }

        string IScriptQuery.ScriptId { get; set; }

		string IScriptQuery.ScriptFile { get; set; }

        string IScriptQuery.Lang { get; set; }

		Dictionary<string, object> IScriptQuery.Params { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		
		/// <summary>
		/// Inline script to execute
		/// </summary>
		public ScriptQueryDescriptor Script(string script)
		{
			this.Self.Script = script;
			return this;
		}

        /// <summary>
        /// Id of an indexed script to execute
        /// </summary
	    public ScriptQueryDescriptor ScriptId(string scriptId)
        {
            scriptId.ThrowIfNull("scriptId");
            this.Self.ScriptId = scriptId;
            return this;
	    }

		/// <summary>
		/// File name of a script to execute
		/// </summary>
		public ScriptQueryDescriptor ScriptFile(string scriptFile)
		{
			scriptFile.ThrowIfNull("scriptFile");
			this.Self.ScriptFile = scriptFile;	
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
		public ScriptQueryDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this.Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// Language of script.
		/// </summary>
		/// <param name="lang">language</param>
		/// <returns>this</returns>
		public ScriptQueryDescriptor Lang(string lang)
		{
			lang.ThrowIfNull("lang");
			this.Self.Lang = lang;
			return this;
		}

        /// <summary>
        /// Language of script.
        /// </summary>
        /// <param name="lang">language</param>
        /// <returns>this</returns>
        public ScriptQueryDescriptor Lang(ScriptLang lang)
        {
            lang.ThrowIfNull("lang");
            this.Self.Lang = lang.GetStringValue();
            return this;
        }
	}
}
