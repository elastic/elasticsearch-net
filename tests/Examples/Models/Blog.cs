using System;
using System.Runtime.Serialization;
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
