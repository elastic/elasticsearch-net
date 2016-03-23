using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRenderSearchTemplateRequest
	{
		[JsonProperty("inline")]
		string Inline { get; set; }

		[JsonProperty("file")]
		string File { get; set; }

		[JsonProperty("params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, object> Params { get; set; }

	}

	public partial class RenderSearchTemplateRequest
	{
		public string Inline { get; set; }
		public string File { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}


	public partial class RenderSearchTemplateDescriptor
	{
		string IRenderSearchTemplateRequest.Inline { get; set; }
		string IRenderSearchTemplateRequest.File { get; set; }
		Dictionary<string, object> IRenderSearchTemplateRequest.Params { get; set; }

		public RenderSearchTemplateDescriptor Inline(string inline) => Assign(a => a.Inline = inline);

		public RenderSearchTemplateDescriptor File(string file) => Assign(a => a.File = file);

		public RenderSearchTemplateDescriptor Params(Dictionary<string, object> scriptParams) => Assign(a => a.Params = scriptParams);

		public RenderSearchTemplateDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector?.Invoke(new FluentDictionary<string, object>()));

	}
}
