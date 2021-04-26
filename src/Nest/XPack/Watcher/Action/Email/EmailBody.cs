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

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
