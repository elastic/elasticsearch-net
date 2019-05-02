using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class RenderSearchTemplateResponse : ResponseBase
	{
		[DataMember(Name ="template_output")]
		public ILazyDocument TemplateOutput { get; set; }
	}
}
