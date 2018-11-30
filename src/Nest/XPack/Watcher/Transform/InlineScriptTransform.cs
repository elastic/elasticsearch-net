using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IInlineScriptTransform : IScriptTransform
	{
		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[IgnoreDataMember]
		string Inline { get; set; }

		[DataMember(Name ="source")]
		string Source { get; set; }
	}

	public class InlineScriptTransform : ScriptTransformBase, IInlineScriptTransform
	{
		public InlineScriptTransform(string script) => Source = script;

		public string Inline
		{
			get => Source;
			set => Source = value;
		}

		public string Source { get; set; }
	}

	public class InlineScriptTransformDescriptor
		: ScriptTransformDescriptorBase<InlineScriptTransformDescriptor, IInlineScriptTransform>, IInlineScriptTransform
	{
		public InlineScriptTransformDescriptor(string inline) => Self.Source = inline;

		string IInlineScriptTransform.Inline
		{
			get => Self.Source;
			set => Self.Source = value;
		}

		string IInlineScriptTransform.Source { get; set; }
	}
}
