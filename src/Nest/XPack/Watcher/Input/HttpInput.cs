using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// input to submit a request to an HTTP endpoint and load the response
	/// into the watch execution context when a watch is triggered.
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HttpInput>))]
	public interface IHttpInput : IInput
	{
		/// <summary>
		/// A array of json keys to extract from the input response and use as payload.
		/// In cases when an input generates a large response this can be used to filter
		/// the relevant piece of the response to be used as payload.
		/// </summary>
		[JsonProperty("extract")]
		IEnumerable<string> Extract { get; set; }

		/// <summary>
		/// The HTTP input request details
		/// </summary>
		[JsonProperty("request")]
		IHttpInputRequest Request { get; set; }

		/// <summary>
		/// The expected content type the response body will contain.
		/// If the format is text, <see cref="HttpInput.Extract"/> cannot exist.
		/// Note that this overrides the header that is returned in the HTTP response.
		/// </summary>
		[JsonProperty("response_content_type")]
		ResponseContentType? ResponseContentType { get; set; }
	}

	/// <inheritdoc />
	public class HttpInput : InputBase, IHttpInput
	{
		/// <inheritdoc />
		public IEnumerable<string> Extract { get; set; }

		/// <inheritdoc />
		public IHttpInputRequest Request { get; set; }

		/// <inheritdoc />
		public ResponseContentType? ResponseContentType { get; set; }

		internal override void WrapInContainer(IInputContainer container) => container.Http = this;
	}

	/// <inheritdoc />
	public class HttpInputDescriptor : DescriptorBase<HttpInputDescriptor, IHttpInput>, IHttpInput
	{
		IEnumerable<string> IHttpInput.Extract { get; set; }
		IHttpInputRequest IHttpInput.Request { get; set; }
		ResponseContentType? IHttpInput.ResponseContentType { get; set; }

		/// <inheritdoc />
		public HttpInputDescriptor Request(Func<HttpInputRequestDescriptor, IHttpInputRequest> httpRequestSelector) =>
			Assign(a => a.Request = httpRequestSelector(new HttpInputRequestDescriptor()));

		/// <inheritdoc />
		public HttpInputDescriptor Extract(IEnumerable<string> extract) =>
			Assign(a => a.Extract = extract);

		/// <inheritdoc />
		public HttpInputDescriptor Extract(params string[] extract) =>
			Assign(a => a.Extract = extract);

		/// <inheritdoc />
		public HttpInputDescriptor ResponseContentType(ResponseContentType? responseContentType) =>
			Assign(a => a.ResponseContentType = responseContentType);
	}
}
