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
		internal StoredScript()
		{
		}

		/// <summary>
		/// Stored Script constructor
		/// </summary>
		/// <param name="lang">Used only for Mustache and Lucene Expression templates.</param>
		/// <param name="source">Script source</param>
		protected StoredScript(string lang, string source)
		{
			((IStoredScript) this).Lang = lang;
			((IStoredScript) this).Source = source;
		}
	}

	public class PainlessScript : StoredScript
	{
		public PainlessScript(string source) : base(null, source)
		{
		}
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
		string IStoredScript.Source { get; set; }
		string IStoredScript.Lang { get; set; }

		public StoredScriptDescriptor Source(string source) => Assign(a => a.Source = source);
	}
}
