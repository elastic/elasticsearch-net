using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IGetSearchTemplateResponse : IResponse
	{
		string Template { get; }
	}

	[JsonObject]
	public class GetSearchTemplateResponse : ResponseBase, IGetSearchTemplateResponse
	{
		[JsonProperty("template")]
		public string Template { get; internal set; }
	}
}
