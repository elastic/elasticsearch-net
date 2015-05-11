using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class ReproduceRangeFormatTests : IntegrationTests
	{
		[Test]
		public void RangesSerializedAsStringsCausesElasticsearchException()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Range(r => r
						.OnField(p => p.LongValue)
						.LowerOrEquals(10d)
						.GreaterOrEquals(50d)
					)
				)
			);

			result.IsValid.Should().BeTrue();
		}
	}
}
