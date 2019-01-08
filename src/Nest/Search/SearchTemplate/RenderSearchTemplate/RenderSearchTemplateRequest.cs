using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public partial interface IRenderSearchTemplateRequest
	{
		[DataMember(Name = "file")]
		string File { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[IgnoreDataMember]
		string Inline { get; set; }

		[DataMember(Name = "params")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatterBase<Dictionary<string, object>, string, object>))]
		Dictionary<string, object> Params { get; set; }

		[DataMember(Name = "source")]
		string Source { get; set; }
	}

	public partial class RenderSearchTemplateRequest
	{
		public string File { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public string Inline
		{
			get => Source;
			set => Source = value;
		}

		public Dictionary<string, object> Params { get; set; }
		public string Source { get; set; }
	}

	public partial class RenderSearchTemplateDescriptor
	{
		string IRenderSearchTemplateRequest.File { get; set; }

		string IRenderSearchTemplateRequest.Inline
		{
			get => Self.Source;
			set => Self.Source = value;
		}

		Dictionary<string, object> IRenderSearchTemplateRequest.Params { get; set; }
		string IRenderSearchTemplateRequest.Source { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public RenderSearchTemplateDescriptor Inline(string inline) => Assign(a => a.Inline = inline);

		public RenderSearchTemplateDescriptor Source(string source) => Assign(a => a.Source = source);

		public RenderSearchTemplateDescriptor File(string file) => Assign(a => a.File = file);

		public RenderSearchTemplateDescriptor Params(Dictionary<string, object> scriptParams) => Assign(a => a.Params = scriptParams);

		public RenderSearchTemplateDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector?.Invoke(new FluentDictionary<string, object>()));
	}
}
