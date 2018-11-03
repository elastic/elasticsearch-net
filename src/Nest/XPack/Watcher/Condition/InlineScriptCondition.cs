using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IInlineScriptCondition : IScriptCondition
	{
		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }

		[JsonProperty("source")]
		string Source { get; set; }
	}

	public class InlineScriptCondition : ScriptConditionBase, IInlineScriptCondition
	{
		public InlineScriptCondition(string script) => Source = script;

		public string Inline
		{
			get => Source;
			set => Source = value;
		}

		public string Source { get; set; }
	}

	public class InlineScriptConditionDescriptor
		: ScriptConditionDescriptorBase<InlineScriptConditionDescriptor, IInlineScriptCondition>, IInlineScriptCondition
	{
		public InlineScriptConditionDescriptor(string script) => Self.Source = script;

		string IInlineScriptCondition.Inline
		{
			get => Self.Source;
			set => Self.Source = value;
		}

		string IInlineScriptCondition.Source { get; set; }
	}
}
