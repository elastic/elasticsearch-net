using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IInlineScript : IScript
	{
		[JsonProperty("source")]
		string Source { get; set; }

		[Obsolete("Use Source. Inline is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }
	}

	public class InlineScript : ScriptBase, IInlineScript
	{
		public InlineScript(string script) => this.Source = script;

		public string Source { get; set; }
		public string Inline { get => this.Source; set => this.Source = value; }

		public static implicit operator InlineScript(string script) => new InlineScript(script);
	}

	public class InlineScriptDescriptor
		: ScriptDescriptorBase<InlineScriptDescriptor, IInlineScript>, IInlineScript
	{
		string IInlineScript.Inline { get => Self.Source; set => Self.Source = value; }
		string IInlineScript.Source { get; set; }

		public InlineScriptDescriptor() { }

		public InlineScriptDescriptor(string script) => Self.Source = script;

		[Obsolete("Use Source(). Inline() is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		public InlineScriptDescriptor Inline(string script) => Assign(a => a.Source = script);

		public InlineScriptDescriptor Source(string script) => Assign(a => a.Source = script);
	}
}
