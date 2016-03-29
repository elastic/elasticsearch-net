using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutScriptRequest 
	{
		[JsonProperty("script")]
		string Script { get; set; }
	}

	public partial class PutScriptRequest 
	{
		public string Script { get; set; }
	}

	[DescriptorFor("ScriptPut")]
	public partial class PutScriptDescriptor 
	{
		string IPutScriptRequest.Script { get; set; }

		public PutScriptDescriptor Script(string script) => Assign(a => a.Script = script);
	}
}