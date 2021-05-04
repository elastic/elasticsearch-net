// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class EmailResult
	{
		[DataMember(Name = "bcc")]
		public IEnumerable<string> Bcc { get; set; }

		[DataMember(Name = "body")]
		public EmailBody Body { get; set; }

		[DataMember(Name = "cc")]
		public IEnumerable<string> Cc { get; set; }

		[DataMember(Name = "from")]
		public string From { get; set; }

		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "priority")]
		public EmailPriority? Priority { get; set; }

		[DataMember(Name = "reply_to")]
		public IEnumerable<string> ReplyTo { get; set; }

		[DataMember(Name = "sent_date")]
		public DateTime? SentDate { get; set; }

		[DataMember(Name = "subject")]
		public string Subject { get; set; }

		[DataMember(Name = "to")]
		public IEnumerable<string> To { get; set; }
	}
}
