using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IExecutePainlessScriptRequest
	{
		[JsonProperty("script")]
		PainlessScript Script { get; set; }
	}

	public partial class ExecutePainlessScriptRequest
	{
		public PainlessScript Script { get; set; }
	}

	[DescriptorFor("ScriptsPainlessExecute")]
	public partial class ExecutePainlessScriptDescriptor
	{
		PainlessScript IExecutePainlessScriptRequest.Script { get; set; }

		/// <summary>
		/// A Painless language script
		/// </summary>
		public ExecutePainlessScriptDescriptor Painless(string source) => Assign(a => a.Script = new PainlessScript(source));
	}
}
