using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IInlineScript : IScript
	{
		[JsonProperty("inline")]
		string Inline { get; set; }
	}

	public class InlineScript : ScriptBase, IInlineScript
	{
		public InlineScript(string script) => Inline = script;

		public string Inline { get; set; }

		public static implicit operator InlineScript(string script) => new InlineScript(script);
	}

	public class InlineScriptDescriptor
		: ScriptDescriptorBase<InlineScriptDescriptor, IInlineScript>, IInlineScript
	{
		public InlineScriptDescriptor() { }

		public InlineScriptDescriptor(string script) => Self.Inline = script;

		string IInlineScript.Inline { get; set; }

		public InlineScriptDescriptor Inline(string script) => Assign(a => a.Inline = script);
	}
}
