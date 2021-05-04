// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Tests.Benchmarking.Framework
{
	public class BenchmarkConfigAttribute : Attribute
	{
		public int RunCount { get; }

		public BenchmarkConfigAttribute(int runCount = 1) => RunCount = runCount;
	}
}
