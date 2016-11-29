using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SlackDynamicAttachment>))]
	public interface ISlackDynamicAttachment
	{
		[JsonProperty("list_path")]
		string ListPath { get; set; }

		[JsonProperty("attachment_template")]
		ISlackAttachment AttachmentTemplate { get; set; }
	}

	public class SlackDynamicAttachment : ISlackDynamicAttachment
	{
		public string ListPath { get; set; }

		public ISlackAttachment AttachmentTemplate { get; set; }
	}

	public class SlackDynamicAttachmentDescriptor : DescriptorBase<SlackDynamicAttachmentDescriptor, ISlackDynamicAttachment>, ISlackDynamicAttachment
	{
		string ISlackDynamicAttachment.ListPath { get; set; }
		ISlackAttachment ISlackDynamicAttachment.AttachmentTemplate { get; set; }

		public SlackDynamicAttachmentDescriptor ListPath(string listPath) => Assign(a => a.ListPath = listPath);

		public SlackDynamicAttachmentDescriptor AttachmentTemplate(Func<SlackAttachmentDescriptor, ISlackAttachment> selector) =>
			Assign(a => a.AttachmentTemplate = selector?.Invoke(new SlackAttachmentDescriptor()));
	}
}
