using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SlackMessage))]
	public interface ISlackMessage
	{
		[DataMember(Name ="attachments")]
		IEnumerable<ISlackAttachment> Attachments { get; set; }

		[DataMember(Name ="dynamic_attachments")]
		ISlackDynamicAttachment DynamicAttachments { get; set; }

		[DataMember(Name ="from")]
		string From { get; set; }

		[DataMember(Name ="icon")]
		string Icon { get; set; }

		[DataMember(Name ="text")]
		string Text { get; set; }

		[DataMember(Name ="to")]
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
