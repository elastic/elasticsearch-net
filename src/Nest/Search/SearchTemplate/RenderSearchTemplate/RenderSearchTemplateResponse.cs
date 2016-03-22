using Newtonsoft.Json;

namespace Nest
{
	public interface IRenderSearchTemplateResponse : IResponse
	{
		ILazyDocument TemplateOutput { get; set; }
	}

	[JsonObject]
	public class RenderSearchTemplateResponse : ResponseBase, IRenderSearchTemplateResponse
	{
		[JsonProperty("template_output")]
		public ILazyDocument TemplateOutput { get; set; }
	}
}
