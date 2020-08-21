// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class SlackActionMessageResult
	{
		[DataMember(Name ="message")]
		public ISlackMessage Message { get; set; }

		[DataMember(Name ="reason")]
		public string Reason { get; set; }

		[DataMember(Name ="request")]
		public HttpInputRequestResult Request { get; set; }

		[DataMember(Name ="response")]
		public HttpInputResponseResult Response { get; set; }

		[DataMember(Name ="status")]
		public Status Status { get; set; }

		[DataMember(Name ="to")]
		public string To { get; set; }
	}
}
