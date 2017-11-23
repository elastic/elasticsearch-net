using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IInlineScriptCondition : IScriptCondition
	{
		[JsonProperty("source")]
		string Source { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }
	}

	public class InlineScriptCondition : ScriptConditionBase, IInlineScriptCondition
	{
		public InlineScriptCondition(string script)
		{
			this.Source = script;
		}

		public string Source { get; set; }
		public string Inline { get => this.Source; set => this.Source = value; }
	}

	public class InlineScriptConditionDescriptor :
		ScriptConditionDescriptorBase<InlineScriptConditionDescriptor, IInlineScriptCondition>, IInlineScriptCondition
	{
		public InlineScriptConditionDescriptor(string script)
		{
			Self.Source = script;
		}

		string IInlineScriptCondition.Inline { get => Self.Source; set => Self.Source = value; }
		string IInlineScriptCondition.Source { get; set; }
	}
}
