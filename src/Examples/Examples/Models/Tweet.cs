using System;
using Nest;

namespace Examples.Models
{
	public class Tweet
	{
		public int? Counter { get; set; }

		public int? Id { get; set; }

		public string Message { get; set; }

		[PropertyName("post_date")]
		public DateTime? PostDate { get; set; }

		public string[] Tags { get; set; }
		public string User { get; set; }
	}
}
