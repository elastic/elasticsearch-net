// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elastic.Transport.Serialization;
using Elasticsearch.Net;

namespace Examples.Models
{
	public class Blog
	{
		public string Content { get; set; }
		public string Title { get; set; }
		public PublishStatus Status { get; set; }

		[DataMember(Name = "tag")]
		public string[] Tags { get; set; }

		[DataMember(Name = "publish_date")]
		public DateTimeOffset PublishDate { get; set; }
	}

	[StringEnum]
	public enum PublishStatus
	{
		[DataMember(Name = "published")]
		Published,
		[DataMember(Name = "active")]
		Active,
	}
}
