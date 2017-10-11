using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<StoredScript>))]
	public interface IStoredScript
	{
		[JsonProperty("lang")]
		string Lang { get; set; }

		[JsonProperty("source")]
		string Source { get; set; }
	}
	public class StoredScript : IStoredScript
	{
		[JsonProperty("lang")]
		string IStoredScript.Lang { get; set; }
		[JsonProperty("source")]
		string IStoredScript.Source { get; set; }

		//used for deserialization
		internal StoredScript() { }

		protected StoredScript(string lang, string source)
		{
			((IStoredScript) this).Lang = lang;
			((IStoredScript) this).Source = source;
		}
	}

	public class PainlessScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Painless.GetStringValue();
		public PainlessScript(string source) : base(Lang, source) { }
	}
	public class GroovyScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Groovy.GetStringValue();
		public GroovyScript(string source) : base(Lang, source) { }
	}
	public class JavaScriptScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.JS.GetStringValue();
		public JavaScriptScript(string source) : base(Lang, source) { }
	}
	public class PythonScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Python.GetStringValue();
		public PythonScript(string source) : base(Lang, source) { }
	}
	public class LuceneExpressionScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Expression.GetStringValue();
		public LuceneExpressionScript(string source) : base(Lang, source) { }
	}
	public class MustacheScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Mustache.GetStringValue();
		public MustacheScript(string source) : base(Lang, source) { }
	}

	public class StoredScriptDescriptor : DescriptorBase<StoredScriptDescriptor, IStoredScript>, IStoredScript
	{
		string IStoredScript.Lang { get; set; }
		string IStoredScript.Source { get; set; }

		public StoredScriptDescriptor Lang(string lang) => Assign(a => a.Lang = lang);

		public StoredScriptDescriptor Source(string source) => Assign(a => a.Source = source);
	}
}
