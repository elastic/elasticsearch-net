// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// input to submit a request to an HTTP endpoint and load the response
	/// into the watch execution context when a watch is triggered.
	/// </summary>
	[ReadAs(typeof(HttpInput))]
	public interface IHttpInput : IInput
	{
		/// <summary>
		/// A array of json keys to extract from the input response and use as payload.
		/// In cases when an input generates a large response this can be used to filter
		/// the relevant piece of the response to be used as payload.
		/// </summary>
		[DataMember(Name ="extract")]
		IEnumerable<string> Extract { get; set; }

		/// <summary>
		/// The HTTP input request details
		/// </summary>
		[DataMember(Name ="request")]
		IHttpInputRequest Request { get; set; }

		/// <summary>
		/// The expected content type the response body will contain.
		/// If the format is text, <see cref="HttpInput.Extract" /> cannot exist.
		/// Note that this overrides the header that is returned in the HTTP response.
		/// </summary>
		[DataMember(Name ="response_content_type")]
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
			Assign(httpRequestSelector(new HttpInputRequestDescriptor()), (a, v) => a.Request = v);

		/// <inheritdoc />
		public HttpInputDescriptor Extract(IEnumerable<string> extract) =>
			Assign(extract, (a, v) => a.Extract = v);

		/// <inheritdoc />
		public HttpInputDescriptor Extract(params string[] extract) =>
			Assign(extract, (a, v) => a.Extract = v);

		/// <inheritdoc />
		public HttpInputDescriptor ResponseContentType(ResponseContentType? responseContentType) =>
			Assign(responseContentType, (a, v) => a.ResponseContentType = v);
	}
}
