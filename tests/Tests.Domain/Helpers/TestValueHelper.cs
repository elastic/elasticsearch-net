// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Tests.Configuration;

namespace Tests.Domain.Helpers
{
	public static class TestValueHelper
	{
		public const string ProjectsIndex = "project";

		public static DateTime FixedDate { get; } = new DateTime(2015, 06, 06, 12, 01, 02, 123);

		public static string PercolatorType => InRange("<5.0.0-alpha1") ? ".percolator" : "query";

		private static bool InRange(string range) => TestConfiguration.Instance.ElasticsearchVersion.InRange(range);

		public static object Dependant(object builtin, object source) => TestConfiguration.Instance.Random.SourceSerializer ? source : builtin;
	}
}
