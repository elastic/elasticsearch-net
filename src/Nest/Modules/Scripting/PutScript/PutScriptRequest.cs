using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("put_script.json")]
	public partial interface IPutScriptRequest
	{
		[DataMember(Name ="script")]
		IStoredScript Script { get; set; }
	}

	public partial class PutScriptRequest
	{
		public IStoredScript Script { get; set; }
	}

	public partial class PutScriptDescriptor
	{
		IStoredScript IPutScriptRequest.Script { get; set; }

		public PutScriptDescriptor Script(Func<StoredScriptDescriptor, IStoredScript> selector) =>
			Assign(a => a.Script = selector?.Invoke(new StoredScriptDescriptor()));

		/// <summary>
		/// A Painless language script
		/// </summary>
		public PutScriptDescriptor Painless(string source) => Assign(a => a.Script = new PainlessScript(source));

		/// <summary>
		/// A Lucene expression language script
		/// </summary>
		public PutScriptDescriptor LuceneExpression(string source) => Assign(a => a.Script = new LuceneExpressionScript(source));

		/// <summary>
		/// A Mustache template language script
		/// </summary>
		public PutScriptDescriptor Mustache(string source) => Assign(a => a.Script = new MustacheScript(source));
	}
}
