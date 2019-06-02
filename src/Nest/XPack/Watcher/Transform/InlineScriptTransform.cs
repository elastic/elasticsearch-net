using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IInlineScriptTransform : IScriptTransform
	{
		[DataMember(Name ="source")]
		string Source { get; set; }
	}

	public class InlineScriptTransform : ScriptTransformBase, IInlineScriptTransform
	{
		public InlineScriptTransform(string source) => Source = source;

		public string Source { get; set; }
	}

	public class InlineScriptTransformDescriptor
		: ScriptTransformDescriptorBase<InlineScriptTransformDescriptor, IInlineScriptTransform>, IInlineScriptTransform
	{
		public InlineScriptTransformDescriptor(string inline) => Self.Source = inline;

		string IInlineScriptTransform.Source { get; set; }
	}
}
