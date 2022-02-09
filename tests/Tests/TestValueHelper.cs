// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Configuration;

namespace Tests
{
	public static class TestValueHelper
	{
		public const string ProjectsIndex = "project";

		public static DateTime FixedDate { get; } = new DateTime(2015, 06, 06, 12, 01, 02, 123);

		private static bool InRange(string range) => TestConfiguration.Instance.ElasticsearchVersion.InRange(range);

		public static object Dependant(object builtin, object source) => TestConfiguration.Instance.Random.SourceSerializer ? source : builtin;
	}
}


