using Newtonsoft.Json;

namespace Nest
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
