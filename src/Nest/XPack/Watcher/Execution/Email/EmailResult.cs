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
