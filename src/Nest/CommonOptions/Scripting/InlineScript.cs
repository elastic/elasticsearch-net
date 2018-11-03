using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IInlineScript : IScript
	{
		[Obsolete("Use Source. Inline is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }

		[JsonProperty("source")]
		string Source { get; set; }
	}

	public class InlineScript : ScriptBase, IInlineScript
	{
		public InlineScript(string script) => Source = script;

		public string Inline
		{
			get => Source;
			set => Source = value;
		}

		public string Source { get; set; }

		public static implicit operator InlineScript(string script) => new InlineScript(script);
	}

	public class InlineScriptDescriptor
		: ScriptDescriptorBase<InlineScriptDescriptor, IInlineScript>, IInlineScript
	{
		public InlineScriptDescriptor() { }

		public InlineScriptDescriptor(string script) => Self.Source = script;

		string IInlineScript.Inline
		{
			get => Self.Source;
			set => Self.Source = value;
		}

		string IInlineScript.Source { get; set; }

		[Obsolete("Use Source(). Inline() is deprecated and scheduled to be removed in Elasticsearch 7.0")]
		public InlineScriptDescriptor Inline(string script) => Assign(a => a.Source = script);

		public InlineScriptDescriptor Source(string script) => Assign(a => a.Source = script);
	}
}
