using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutScriptRequest
	{
		[JsonProperty("script")]
		IStoredScript Script { get; set; }
	}

	public partial class PutScriptRequest
	{
		public IStoredScript Script { get; set; }
	}

	[DescriptorFor("ScriptPut")]
	public partial class PutScriptDescriptor
	{
		IStoredScript IPutScriptRequest.Script { get; set; }

		/// <summary>
		///     A Lucene expression language script
		/// </summary>
		public PutScriptDescriptor LuceneExpression(string source) => Assign(a => a.Script = new LuceneExpressionScript(source));

		/// <summary>
		///     A Mustache template language script
		/// </summary>
		public PutScriptDescriptor Mustache(string source) => Assign(a => a.Script = new MustacheScript(source));

		/// <summary>
		///     A Painless language script
		/// </summary>
		public PutScriptDescriptor Painless(string source) => Assign(a => a.Script = new PainlessScript(source));

		public PutScriptDescriptor Script(Func<StoredScriptDescriptor, IStoredScript> selector) =>
			Assign(a => a.Script = selector?.Invoke(new StoredScriptDescriptor()));
	}
}
