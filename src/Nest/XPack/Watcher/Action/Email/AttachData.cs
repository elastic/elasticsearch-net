using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class AttachData
	{
		[JsonProperty("format")]
		public DataAttachmentFormat Format { get; set; }
	}
}
