// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new SlackAttachmentDescriptor())));
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

		public SlackAttachmentDescriptor Fallback(string fallback) => Assign(fallback, (a, v) => a.Fallback = v);

		public SlackAttachmentDescriptor Color(string color) => Assign(color, (a, v) => a.Color = v);

		public SlackAttachmentDescriptor Pretext(string pretext) => Assign(pretext, (a, v) => a.Pretext = v);

		public SlackAttachmentDescriptor AuthorName(string authorName) => Assign(authorName, (a, v) => a.AuthorName = v);

		public SlackAttachmentDescriptor AuthorIcon(string authorIcon) => Assign(authorIcon, (a, v) => a.AuthorIcon = v);

		public SlackAttachmentDescriptor Title(string title) => Assign(title, (a, v) => a.Title = v);

		public SlackAttachmentDescriptor TitleLink(string titleLink) => Assign(titleLink, (a, v) => a.TitleLink = v);

		public SlackAttachmentDescriptor Text(string text) => Assign(text, (a, v) => a.Text = v);

		public SlackAttachmentDescriptor Fields(Func<SlackAttachmentFieldsDescriptor, IPromise<IList<ISlackAttachmentField>>> selector) =>
			Assign(selector, (a, v) => a.Fields = v?.Invoke(new SlackAttachmentFieldsDescriptor())?.Value);

		public SlackAttachmentDescriptor ImageUrl(string url) => Assign(url, (a, v) => a.ImageUrl = v);

		public SlackAttachmentDescriptor ThumbUrl(string url) => Assign(url, (a, v) => a.ThumbUrl = v);

		public SlackAttachmentDescriptor Footer(string footer) => Assign(footer, (a, v) => a.Footer = v);

		public SlackAttachmentDescriptor FooterIcon(string footerIcon) => Assign(footerIcon, (a, v) => a.FooterIcon = v);

		public SlackAttachmentDescriptor Ts(DateTimeOffset? ts) => Assign(ts, (a, v) => a.Ts = v);
	}
}
