using System.Runtime.Serialization;

namespace Nest
{
	public interface IInlineScriptCondition : IScriptCondition
	{
		[DataMember(Name ="source")]
		string Source { get; set; }
	}

	public class InlineScriptCondition : ScriptConditionBase, IInlineScriptCondition
	{
		public InlineScriptCondition(string script) => Source = script;

		public string Source { get; set; }
	}

	public class InlineScriptConditionDescriptor
		: ScriptConditionDescriptorBase<InlineScriptConditionDescriptor, IInlineScriptCondition>, IInlineScriptCondition
	{
		public InlineScriptConditionDescriptor(string source) => Self.Source = source;

		string IInlineScriptCondition.Source { get; set; }
	}
}
