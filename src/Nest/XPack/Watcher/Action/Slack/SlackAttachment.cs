using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SlackAttachment))]
	public interface ISlackAttachment
	{
		[DataMember(Name = "author_icon")]
		string AuthorIcon { get; set; }

		[DataMember(Name = "author_link")]
		string AuthorLink { get; set; }

		[DataMember(Name = "author_name")]
		string AuthorName { get; set; }

		[DataMember(Name = "color")]
		string Color { get; set; }

		[DataMember(Name = "fallback")]
		string Fallback { get; set; }

		[DataMember(Name = "fields")]
		IEnumerable<ISlackAttachmentField> Fields { get; set; }

		[DataMember(Name = "footer")]
		string Footer { get; set; }

		[DataMember(Name = "footer_icon")]
		string FooterIcon { get; set; }

		[DataMember(Name = "image_url")]
		string ImageUrl { get; set; }

		[DataMember(Name = "pretext")]
		string Pretext { get; set; }

		[DataMember(Name = "text")]
		string Text { get; set; }

		[DataMember(Name = "thumb_url")]
		string ThumbUrl { get; set; }

		[DataMember(Name = "title")]
		string Title { get; set; }

		[DataMember(Name = "title_link")]
		string TitleLink { get; set; }

		[DataMember(Name = "ts")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochSecondsFormatter))]
		DateTimeOffset? Ts { get; set; }
	}

	public class SlackAttachment : ISlackAttachment
	{
		public string AuthorIcon { get; set; }

		public string AuthorLink { get; set; }

		public string AuthorName { get; set; }

		public string Color { get; set; }
		public string Fallback { get; set; }

		public IEnumerable<ISlackAttachmentField> Fields { get; set; }

		public string Footer { get; set; }

		public string FooterIcon { get; set; }

		public string ImageUrl { get; set; }

		public string Pretext { get; set; }

		public string Text { get; set; }

		public string ThumbUrl { get; set; }

		public string Title { get; set; }

		public string TitleLink { get; set; }

		public DateTimeOffset? Ts { get; set; }
	}

	public class SlackAttachmentsDescriptor : DescriptorPromiseBase<SlackAttachmentsDescriptor, IList<ISlackAttachment>>
	{
		public SlackAttachmentsDescriptor() : base(new List<ISlackAttachment>()) { }

		public SlackAttachmentsDescriptor Attachment(Func<SlackAttachmentDescriptor, ISlackAttachment> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new SlackAttachmentDescriptor())));
	}

	public class SlackAttachmentDescriptor : DescriptorBase<SlackAttachmentDescriptor, ISlackAttachment>, ISlackAttachment
	{
		string ISlackAttachment.AuthorIcon { get; set; }
		string ISlackAttachment.AuthorLink { get; set; }
		string ISlackAttachment.AuthorName { get; set; }
		string ISlackAttachment.Color { get; set; }
		string ISlackAttachment.Fallback { get; set; }
		IEnumerable<ISlackAttachmentField> ISlackAttachment.Fields { get; set; }
		string ISlackAttachment.Footer { get; set; }
		string ISlackAttachment.FooterIcon { get; set; }
		string ISlackAttachment.ImageUrl { get; set; }
		string ISlackAttachment.Pretext { get; set; }
		string ISlackAttachment.Text { get; set; }
		string ISlackAttachment.ThumbUrl { get; set; }
		string ISlackAttachment.Title { get; set; }
		string ISlackAttachment.TitleLink { get; set; }
		DateTimeOffset? ISlackAttachment.Ts { get; set; }

		public SlackAttachmentDescriptor Fallback(string fallback) => Assign(a => a.Fallback = fallback);

		public SlackAttachmentDescriptor Color(string color) => Assign(a => a.Color = color);

		public SlackAttachmentDescriptor Pretext(string pretext) => Assign(a => a.Pretext = pretext);

		public SlackAttachmentDescriptor AuthorName(string authorName) => Assign(a => a.AuthorName = authorName);

		public SlackAttachmentDescriptor AuthorIcon(string authorIcon) => Assign(a => a.AuthorIcon = authorIcon);

		public SlackAttachmentDescriptor Title(string title) => Assign(a => a.Title = title);

		public SlackAttachmentDescriptor TitleLink(string titleLink) => Assign(a => a.TitleLink = titleLink);

		public SlackAttachmentDescriptor Text(string text) => Assign(a => a.Text = text);

		public SlackAttachmentDescriptor Fields(Func<SlackAttachmentFieldsDescriptor, IPromise<IList<ISlackAttachmentField>>> selector) =>
			Assign(a => a.Fields = selector?.Invoke(new SlackAttachmentFieldsDescriptor())?.Value);

		public SlackAttachmentDescriptor ImageUrl(string url) => Assign(a => a.ImageUrl = url);

		public SlackAttachmentDescriptor ThumbUrl(string url) => Assign(a => a.ThumbUrl = url);

		public SlackAttachmentDescriptor Footer(string footer) => Assign(a => a.Footer = footer);

		public SlackAttachmentDescriptor FooterIcon(string footerIcon) => Assign(a => a.FooterIcon = footerIcon);

		public SlackAttachmentDescriptor Ts(DateTimeOffset? ts) => Assign(a => a.Ts = ts);
	}
}
