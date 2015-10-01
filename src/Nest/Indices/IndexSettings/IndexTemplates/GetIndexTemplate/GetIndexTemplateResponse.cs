using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexTemplateResponse : IResponse
	{
		string Name { get; }
		TemplateMapping TemplateMapping { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetIndexTemplateResponse : BaseResponse, IGetIndexTemplateResponse
	{
		public string Name { get; internal set; }
		
		public TemplateMapping TemplateMapping { get; internal set; }
	}
}
