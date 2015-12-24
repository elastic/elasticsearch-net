
using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(ReadAsTypeConverter<ScriptFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IScriptFilter : IFilter
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

	public class ScriptFilter : PlainFilter, IScriptFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Script = this;
		}

		public string Script { get; set; }
	    public string ScriptId { get; set; }
		public string ScriptFile { get; set; }
	    public Dictionary<string, object> Params { get; set; }
		public string Lang { get; set; }
	}

	/// <summary>
	/// A filter allowing to define scripts as filters.
	/// Ex: "doc['num1'].value > 1"
	/// </summary>
	public class ScriptFilterDescriptor : FilterBase, IScriptFilter
	{
		private IScriptFilter Self { get { return this; } }

        string IScriptFilter.Script { get; set; }

        string IScriptFilter.ScriptId { get; set; }

		string IScriptFilter.ScriptFile { get; set; }

        string IScriptFilter.Lang { get; set; }

		Dictionary<string, object> IScriptFilter.Params { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Script.IsNullOrEmpty();
			}
		}

		/// <summary>
		/// Inline script to execute
		/// </summary>
		public ScriptFilterDescriptor Script(string script)
		{
			this.Self.Script = script;
			return this;
		}

        /// <summary>
        /// Id of an indexed script to execute
        /// </summary
	    public ScriptFilterDescriptor ScriptId(string scriptId)
        {
            scriptId.ThrowIfNull("scriptId");
            this.Self.ScriptId = scriptId;
            return this;
	    }

		/// <summary>
		/// File name of a script to execute
		/// </summary>
		public ScriptFilterDescriptor ScriptFile(string scriptFile)
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
		public ScriptFilterDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
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
		public ScriptFilterDescriptor Lang(string lang)
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
        public ScriptFilterDescriptor Lang(ScriptLang lang)
        {
            lang.ThrowIfNull("lang");
            this.Self.Lang = lang.GetStringValue();
            return this;
        }
	}
}
