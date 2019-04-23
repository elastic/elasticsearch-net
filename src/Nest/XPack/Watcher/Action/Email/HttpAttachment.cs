using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<HttpAttachment>))]
	public interface IHttpAttachment : IEmailAttachment
	{
		[JsonProperty("content_type")]
		string ContentType { get; set; }

		[JsonProperty("inline")]
		bool? Inline { get; set; }

		[JsonProperty("request")]
		[JsonConverter(typeof(ReadAsTypeJsonConverter<HttpInputRequest>))]
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
			Assign(selector.InvokeOrDefault(new HttpInputRequestDescriptor()), (a, v) => a.Request = v);

		public HttpAttachmentDescriptor Inline(bool? inline = true) => Assign(inline, (a, v) => a.Inline = v);

		public HttpAttachmentDescriptor ContentType(string contentType) => Assign(contentType, (a, v) => a.ContentType = v);
	}
}
