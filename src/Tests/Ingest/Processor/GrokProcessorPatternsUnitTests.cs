using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Framework;

namespace Tests.Ingest.Processor
{
	public class GrokProcessorPatternsUnitTests
	{
		[U]
		public void ShouldDeserialize()
		{
			var fixedResponse = new
			{
				patterns = new Dictionary<string, object>
				{
					{ "BACULA_CAPACITY" ,"%{INT}{1,3}(,%{INT}{3})*"},
					{ "PATH", "(?:%{UNIXPATH}|%{WINPATH})"}
				}
			};

			var client = TestClient.GetFixedReturnClient(fixedResponse);

			//warmup
			var response = client.GrokProcessorPatterns();
			response.ShouldBeValid();

			response.Patterns.Should().NotBeNull();
			response.Patterns.Should().HaveCount(2);
			response.Patterns.Should().ContainKey("BACULA_CAPACITY");
			response.Patterns.Should().ContainKey("PATH");
		}
	}
}
