using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IInlineScriptTransform : IScriptTransform
	{
		[JsonProperty("source")]
		string Source { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }
	}

	public class InlineScriptTransform : ScriptTransformBase, IInlineScriptTransform
	{
		public InlineScriptTransform(string script)
		{
			this.Source = script;
		}
		public string Source { get; set; }
		public string Inline { get => this.Source; set => this.Source = value; }
	}

	public class InlineScriptTransformDescriptor
		: ScriptTransformDescriptorBase<InlineScriptTransformDescriptor, IInlineScriptTransform>, IInlineScriptTransform
	{
		public InlineScriptTransformDescriptor(string inline)
		{
			Self.Source = inline;
		}

		//TODO is this constructor needed?
		public InlineScriptTransformDescriptor() {}

		string IInlineScriptTransform.Inline { get => Self.Source; set => Self.Source = value; }
		string IInlineScriptTransform.Source { get; set; }
	}
}
