// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
