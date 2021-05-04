// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SlackDynamicAttachment))]
	public interface ISlackDynamicAttachment
	{
		[DataMember(Name = "attachment_template")]
		ISlackAttachment AttachmentTemplate { get; set; }

		[DataMember(Name = "list_path")]
		string ListPath { get; set; }
	}

	public class SlackDynamicAttachment : ISlackDynamicAttachment
	{
		public ISlackAttachment AttachmentTemplate { get; set; }
		public string ListPath { get; set; }
	}

	public class SlackDynamicAttachmentDescriptor : DescriptorBase<SlackDynamicAttachmentDescriptor, ISlackDynamicAttachment>, ISlackDynamicAttachment
	{
		ISlackAttachment ISlackDynamicAttachment.AttachmentTemplate { get; set; }
		string ISlackDynamicAttachment.ListPath { get; set; }

		public SlackDynamicAttachmentDescriptor ListPath(string listPath) => Assign(listPath, (a, v) => a.ListPath = v);

		public SlackDynamicAttachmentDescriptor AttachmentTemplate(Func<SlackAttachmentDescriptor, ISlackAttachment> selector) =>
			Assign(selector, (a, v) => a.AttachmentTemplate = v?.Invoke(new SlackAttachmentDescriptor()));
	}
}
