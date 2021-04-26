/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
			Assign(selector.InvokeOrDefault(new HttpInputRequestDescriptor()), (a, v) => a.Request = v);

		public HttpAttachmentDescriptor Inline(bool? inline = true) => Assign(inline, (a, v) => a.Inline = v);

		public HttpAttachmentDescriptor ContentType(string contentType) => Assign(contentType, (a, v) => a.ContentType = v);
	}
}
