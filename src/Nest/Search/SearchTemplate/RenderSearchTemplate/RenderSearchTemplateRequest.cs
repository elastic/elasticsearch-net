using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRenderSearchTemplateRequest
	{
		[JsonProperty("file")]
		string File { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }

		[JsonProperty("params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty("source")]
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
		public RenderSearchTemplateDescriptor Inline(string inline) => Assign(inline, (a, v) => a.Inline = v);

		public RenderSearchTemplateDescriptor Source(string source) => Assign(source, (a, v) => a.Source = v);

		public RenderSearchTemplateDescriptor File(string file) => Assign(file, (a, v) => a.File = v);

		public RenderSearchTemplateDescriptor Params(Dictionary<string, object> scriptParams) => Assign(scriptParams, (a, v) => a.Params = v);

		public RenderSearchTemplateDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(paramsSelector, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
