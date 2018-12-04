using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HttpAttachment))]
	public interface IHttpAttachment : IEmailAttachment
	{
		[DataMember(Name = "content_type")]
		string ContentType { get; set; }

		[DataMember(Name = "inline")]
		bool? Inline { get; set; }

		[DataMember(Name = "request")]
		IHttpInputRequest Request { get; set; }
	}

	public class HttpAttachment : IHttpAttachment
	{
		public string ContentType { get; set; }

		public bool? Inline { get; set; }

		public IHttpInputRequest Request { get; set; }
	}

	public class HttpAttachmentDescriptor : DescriptorBase<HttpAttachmentDescriptor, IHttpAttachment>, IHttpAttachment
	{
		string IHttpAttachment.ContentType { get; set; }
		bool? IHttpAttachment.Inline { get; set; }
		IHttpInputRequest IHttpAttachment.Request { get; set; }

		public HttpAttachmentDescriptor Request(Func<HttpInputRequestDescriptor, IHttpInputRequest> selector) =>
			Assign(a => a.Request = selector.InvokeOrDefault(new HttpInputRequestDescriptor()));

		public HttpAttachmentDescriptor Inline(bool? inline = true) => Assign(a => a.Inline = inline);

		public HttpAttachmentDescriptor ContentType(string contentType) => Assign(a => a.ContentType = contentType);
	}
}
