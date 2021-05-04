// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
