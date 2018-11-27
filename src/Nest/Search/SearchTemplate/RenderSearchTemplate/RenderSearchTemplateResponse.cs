using System.Runtime.Serialization;

namespace Nest
{
	public interface IRenderSearchTemplateResponse : IResponse
	{
		ILazyDocument TemplateOutput { get; set; }
	}

	[DataContract]
	public class RenderSearchTemplateResponse : ResponseBase, IRenderSearchTemplateResponse
	{
		[DataMember(Name ="template_output")]
		public ILazyDocument TemplateOutput { get; set; }
	}
}
