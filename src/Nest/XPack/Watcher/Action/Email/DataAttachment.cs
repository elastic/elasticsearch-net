using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{
	[DataContract]
	[ReadAs(typeof(DataAttachment))]
	public interface IDataAttachment : IEmailAttachment
	{
		[DataMember(Name ="format")]
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


	public enum DataAttachmentFormat
	{
		[EnumMember(Value = "json")]
		Json,

		[EnumMember(Value = "yaml")]
		Yaml
	}
}
