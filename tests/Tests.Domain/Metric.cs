using System;
using Nest;

namespace Tests.Domain
{
	public class Metric
	{
		public long Accept { get; set; }

		public long Deny { get; set; }

		public string Host { get; set; }

		public float Response { get; set; }

		public string Service { get; set; }

		[Date(Name = "@timestamp")]
		[MachineLearningDateTime]
		public DateTime Timestamp { get; set; }

		public long Total { get; set; }
	}
}
