// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(EmailBody))]
	public interface IEmailBody
	{
		[DataMember(Name = "html")]
		string Html { get; set; }

		[DataMember(Name = "text")]
		string Text { get; set; }
	}

	public class EmailBody : IEmailBody
	{
		public string Html { get; set; }
		public string Text { get; set; }
	}

	public class EmailBodyDescriptor : DescriptorBase<EmailBodyDescriptor, IEmailBody>, IEmailBody
	{
		string IEmailBody.Html { get; set; }
		string IEmailBody.Text { get; set; }

		public EmailBodyDescriptor Text(string text) => Assign(text, (a, v) => a.Text = v);

		public EmailBodyDescriptor Html(string html) => Assign(html, (a, v) => a.Html = v);
	}
}
