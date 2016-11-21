using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IInlineScriptTransform : IScriptTransform
	{
		[JsonProperty("inline")]
		string Inline { get; set; }
	}

	public class InlineScriptTransform : ScriptTransformBase, IInlineScriptTransform
	{
		public InlineScriptTransform(string script)
		{
			this.Inline = script;
		}

		public string Inline { get; set; }
	}

	public class InlineScriptTransformDescriptor
		: ScriptTransformDescriptorBase<InlineScriptTransformDescriptor, IInlineScriptTransform>, IInlineScriptTransform
	{
		public InlineScriptTransformDescriptor(string inline)
		{
			Self.Inline = inline;
		}

		public InlineScriptTransformDescriptor() {}

		string IInlineScriptTransform.Inline { get; set; }
	}
}
