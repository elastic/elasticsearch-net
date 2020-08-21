// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class EmailActionResult
	{
		[DataMember(Name = "account")]
		public string Account { get; set; }

		[DataMember(Name = "message")]
		public EmailResult Message { get; set; }

		[DataMember(Name = "reason")]
		public string Reason { get; set; }
	}
}
