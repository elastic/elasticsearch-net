using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITemplateResponse : IResponse
	{
		string Name { get; }
		TemplateMapping TemplateMapping { get; }
	}

	[JsonObject]
	[JsonConverter(typeof(TemplateResponseConverter))]
	public class TemplateResponse : BaseResponse, ITemplateResponse
	{
		public string Name { get; internal set; }
		public TemplateMapping TemplateMapping { get; internal set; }
	}
}
