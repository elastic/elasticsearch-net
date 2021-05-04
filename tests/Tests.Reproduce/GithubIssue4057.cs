// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Serialization;

namespace Tests.Reproduce
{
	public class GithubIssue4057
	{
		[U]
		[UseCulture("sv-SE")]
		public void DoubleAffectedByPrecisionProblemDeserializesCorrectlyIndependentOfCurrentCulture()
		{
			var expected = 16.27749494276941D;

			var tester = SerializationTester.Default;
			var actual = tester.Deserializes<double>("16.27749494276941");

			Math.Round(actual.Result, 13).Should().Be(Math.Round(expected, 13));
		}
	}
}
