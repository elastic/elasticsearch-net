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

		public PutScriptDescriptor Script(Func<StoredScriptDescriptor, IStoredScript> selector) =>
			Assign(a => a.Script = selector?.Invoke(new StoredScriptDescriptor()));

		public PutScriptDescriptor Painless(string source) => Assign(a => a.Script = new PainlessScript(source));
		public PutScriptDescriptor Groovy(string source) => Assign(a => a.Script = new GroovyScript(source));
		public PutScriptDescriptor JavaScript(string source) => Assign(a => a.Script = new JavaScriptScript(source));
		public PutScriptDescriptor Python(string source) => Assign(a => a.Script = new PythonScript(source));
		public PutScriptDescriptor LuceneExpression(string source) => Assign(a => a.Script = new LuceneExpressionScript(source));
		public PutScriptDescriptor Mustache(string source) => Assign(a => a.Script = new MustacheScript(source));
	}
}
