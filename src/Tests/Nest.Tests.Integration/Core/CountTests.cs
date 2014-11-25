using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class CountTests : IntegrationTests
	{
		[Test]
		public void CountTest()
		{
			var response = this.Client.Count<ElasticsearchProject>();

			response.IsValid.Should().BeTrue();
			response.Count.Should().BeGreaterThan(0);
		}

		[Test]
		public void CountWithQueryTest()
		{
			var lookFor = NestTestData.Data.Select(p => p.Country).First();
			var response = this.Client.Count<ElasticsearchProject>(c => c
				.Query(q => q
					.Match(m => m
						.OnField(p => p.Country)
						.Query(lookFor)
					)
				)
			);

			response.IsValid.Should().BeTrue();
			response.Count.Should().BeGreaterThan(0);
		}
	}
}
