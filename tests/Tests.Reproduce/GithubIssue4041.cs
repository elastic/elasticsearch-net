using System;
using System.Collections.Generic;
using System.Text;
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
