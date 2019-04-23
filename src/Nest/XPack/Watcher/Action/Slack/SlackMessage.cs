using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SlackMessage>))]
	public interface ISlackMessage
	{
		[JsonProperty("attachments")]
		IEnumerable<ISlackAttachment> Attachments { get; set; }

		[JsonProperty("dynamic_attachments")]
		ISlackDynamicAttachment DynamicAttachments { get; set; }

		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("icon")]
		string Icon { get; set; }

		[JsonProperty("text")]
		string Text { get; set; }

		[JsonProperty("to")]
		IEnumerable<string> To { get; set; }
	}

	public class SlackMessage : ISlackMessage
	{
		public IEnumerable<ISlackAttachment> Attachments { get; set; }

		public ISlackDynamicAttachment DynamicAttachments { get; set; }
		public string From { get; set; }

		public string Icon { get; set; }

		public string Text { get; set; }

		public IEnumerable<string> To { get; set; }
	}

	public class SlackMessageDescriptor : DescriptorBase<SlackMessageDescriptor, ISlackMessage>, ISlackMessage
	{
		IEnumerable<ISlackAttachment> ISlackMessage.Attachments { get; set; }
		ISlackDynamicAttachment ISlackMessage.DynamicAttachments { get; set; }
		string ISlackMessage.From { get; set; }
		string ISlackMessage.Icon { get; set; }
		string ISlackMessage.Text { get; set; }
		IEnumerable<string> ISlackMessage.To { get; set; }

		public SlackMessageDescriptor From(string from) => Assign(from, (a, v) => a.From = v);

		public SlackMessageDescriptor To(IEnumerable<string> to) => Assign(to, (a, v) => a.To = v);

		public SlackMessageDescriptor To(params string[] to) => Assign(to, (a, v) => a.To = v);

		public SlackMessageDescriptor Icon(string icon) => Assign(icon, (a, v) => a.Icon = v);

		public SlackMessageDescriptor Text(string text) => Assign(text, (a, v) => a.Text = v);

		public SlackMessageDescriptor Attachments(Func<SlackAttachmentsDescriptor, IPromise<IList<ISlackAttachment>>> selector) =>
			Assign(selector, (a, v) => a.Attachments = v?.Invoke(new SlackAttachmentsDescriptor())?.Value);

		public SlackMessageDescriptor DynamicAttachments(Func<SlackDynamicAttachmentDescriptor, ISlackDynamicAttachment> selector) =>
			Assign(selector, (a, v) => a.DynamicAttachments = v?.Invoke(new SlackDynamicAttachmentDescriptor()));
	}
}
