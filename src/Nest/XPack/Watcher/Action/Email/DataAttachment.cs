using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DataAttachment>))]
	public interface IDataAttachment : IEmailAttachment
	{
		[JsonProperty("format")]
		DataAttachmentFormat? Format { get; set; }
	}

	public class DataAttachment : IDataAttachment
	{
		public DataAttachmentFormat? Format { get; set; }
	}

	public class DataAttachmentDescriptor : DescriptorBase<DataAttachmentDescriptor, IDataAttachment>, IDataAttachment
	{
		DataAttachmentFormat? IDataAttachment.Format { get; set; }

		public DataAttachmentDescriptor Format(DataAttachmentFormat? format) => Assign(a => a.Format = format);
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum DataAttachmentFormat
	{
		[EnumMember(Value = "json")]
		Json,
		[EnumMember(Value = "yaml")]
		Yaml
	}
}
