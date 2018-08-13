using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IExecutePainlessScriptRequest
	{
		[JsonProperty("script")]
		IInlineScript Script { get; set; }
	}

	public partial class ExecutePainlessScriptRequest
	{
		public IInlineScript Script { get; set; }
	}

	[DescriptorFor("ScriptsPainlessExecute")]
	public partial class ExecutePainlessScriptDescriptor
	{
		IInlineScript IExecutePainlessScriptRequest.Script { get; set; }

		public ExecutePainlessScriptDescriptor Script(Func<InlineScriptDescriptor, IInlineScript> selector) =>
			Assign(a => a.Script = selector?.Invoke(new InlineScriptDescriptor()));
	}
}
