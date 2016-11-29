using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class EmailResult
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("sent_date")]
		public DateTime? SentDate { get; set; }

		[JsonProperty("from")]
		public string From { get; set; }

		[JsonProperty("to")]
		public IEnumerable<string> To { get; set; }

		[JsonProperty("cc")]
		public IEnumerable<string> Cc { get; set; }

		[JsonProperty("bcc")]
		public IEnumerable<string> Bcc { get; set; }

		[JsonProperty("reply_to")]
		public IEnumerable<string> ReplyTo { get; set; }

		[JsonProperty("subject")]
		public string Subject { get; set; }

		[JsonProperty("body")]
		public EmailBody Body { get; set; }

		[JsonProperty("priority")]
		public EmailPriority? Priority { get; set; }
	}
}
