// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Examples.Models
{
	public class Tweet
	{
		public int? Counter { get; set; }

		public int? Id { get; set; }

		public string Name { get; set; }

		public string Message { get; set; }

		[DataMember(Name = "post_date")]
		public DateTime? PostDate { get; set; }

		public string[] Tags { get; set; }

		public string User { get; set; }

		[DataMember(Name = "user_name")]
		public string UserName { get; set; }
		public long? Likes { get; set; }
		public int? Age { get; set; }
		public string Email { get; set; }

		public string Title { get; set; }

		public DateTime? Date { get; set; }
	}
}
