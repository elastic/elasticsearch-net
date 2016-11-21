using Newtonsoft.Json;

namespace Nest
{
	public interface IInlineScriptCondition : IScriptCondition
	{
		[JsonProperty("inline")]
		string Inline { get; set; }
	}

	public class InlineScriptCondition : ScriptConditionBase, IInlineScriptCondition
	{
		public InlineScriptCondition(string script)
		{
			this.Inline = script;
		}

		public string Inline { get; set; }
	}

	public class InlineScriptConditionDescriptor :
		ScriptConditionDescriptorBase<InlineScriptConditionDescriptor, IInlineScriptCondition>, IInlineScriptCondition
	{
		public InlineScriptConditionDescriptor(string script)
		{
			Self.Inline = script;
		}

		string IInlineScriptCondition.Inline { get; set; }
	}
}
