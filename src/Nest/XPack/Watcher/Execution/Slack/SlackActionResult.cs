// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class SlackActionResult
	{
		[DataMember(Name ="account")]
		public string Account { get; set; }

		[DataMember(Name ="sent_messages")]
		public IEnumerable<SlackActionMessageResult> SentMessages { get; set; }
	}
}
