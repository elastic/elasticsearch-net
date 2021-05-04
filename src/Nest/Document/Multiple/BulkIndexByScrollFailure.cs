// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	[DataContract]
	public class BulkIndexByScrollFailure
	{
		[DataMember(Name = "cause")]
		public Error Cause { get; set; }

		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		[DataMember(Name = "index")]
		public string Index { get; set; }

		[DataMember(Name = "status")]
		public int Status { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; internal set; }
	}
}
