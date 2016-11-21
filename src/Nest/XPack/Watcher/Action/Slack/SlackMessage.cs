using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SlackMessage>))]
	public interface ISlackMessage
	{
		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("to")]
		IEnumerable<string> To { get; set; }

		[JsonProperty("icon")]
		string Icon { get; set; }

		[JsonProperty("text")]
		string Text { get; set; }

		[JsonProperty("attachments")]
		IEnumerable<ISlackAttachment> Attachments { get; set; }

		[JsonProperty("dynamic_attachments")]
		ISlackDynamicAttachment DynamicAttachments { get; set; }
	}

	public class SlackMessage : ISlackMessage
	{
		public string From { get; set; }

		public IEnumerable<string> To { get; set; }

		public string Icon { get; set; }

		public string Text { get; set; }

		public IEnumerable<ISlackAttachment> Attachments { get; set; }

		public ISlackDynamicAttachment DynamicAttachments { get; set; }
	}

	public class SlackMessageDescriptor : DescriptorBase<SlackMessageDescriptor, ISlackMessage>, ISlackMessage
	{
		string ISlackMessage.From { get; set; }
		IEnumerable<string> ISlackMessage.To { get; set; }
		string ISlackMessage.Icon { get; set; }
		string ISlackMessage.Text { get; set; }
		IEnumerable<ISlackAttachment> ISlackMessage.Attachments { get; set; }
		ISlackDynamicAttachment ISlackMessage.DynamicAttachments { get; set; }

		public SlackMessageDescriptor From(string from) => Assign(a => a.From = from);

		public SlackMessageDescriptor To(IEnumerable<string> to) => Assign(a => a.To = to);

		public SlackMessageDescriptor To(params string[] to) => Assign(a => a.To = to);

		public SlackMessageDescriptor Icon(string icon) => Assign(a => a.Icon = icon);

		public SlackMessageDescriptor Text(string text) => Assign(a => a.Text = text);

		public SlackMessageDescriptor Attachments(Func<SlackAttachmentsDescriptor, IPromise<IList<ISlackAttachment>>> selector) =>
			Assign(a => a.Attachments = selector?.Invoke(new SlackAttachmentsDescriptor())?.Value);

		public SlackMessageDescriptor DynamicAttachments(Func<SlackDynamicAttachmentDescriptor, ISlackDynamicAttachment> selector) =>
			Assign(a => a.DynamicAttachments = selector?.Invoke(new SlackDynamicAttachmentDescriptor()));
	}
}
