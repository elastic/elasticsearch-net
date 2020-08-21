// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Reproduce
{
	public class GithubIssue4041
	{
		[U]
		[UseCulture("sv-SE")]
		public void DistanceSerializesWithInvariantCulture()
		{
			const string distanceString = "2.5m";
			Distance distance = distanceString;

			Expect(distanceString)
				.WhenSerializing(distance);
		}
	}
}
