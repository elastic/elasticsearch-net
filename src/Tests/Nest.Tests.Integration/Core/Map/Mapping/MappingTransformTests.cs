
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Core.Map.Mapping
{
	[TestFixture]
	public class MappingTransformTests : BaseMappingTests
	{
		[Test]
		[SkipVersion("0 - 1.2.9", "Mapping transform not introduced until ES 1.3")]
		public void SingleTransformTest()
		{
			var putResult = this.Client.Map<ElasticsearchProject>(m => m
				.MapFromAttributes()
				.Index(ElasticsearchConfiguration.DefaultIndex)
				.Transform(t => t
					.Script("ctx._source['foo'] = ctx._source['bar'];")
					.Language("groovy")
					.Params(new Dictionary<string, string> { { "variable", "value" } })
				)
			);
			this.DefaultResponseAssertations(putResult);

			var getResult = this.Client.GetMapping<ElasticsearchProject>(g => g
				.Index(ElasticsearchConfiguration.DefaultIndex)
			);

			getResult.Mapping.Transform.Count.Should().BeGreaterOrEqualTo(1);
		}

		[Test]
		[SkipVersion("0 - 1.2.9", "Mapping transform not introduced until ES 1.3")]
		public void MultipleTransformTest()
		{
			var putResult = this.Client.Map<ElasticsearchProject>(m => m
				.MapFromAttributes()
				.Index(ElasticsearchConfiguration.DefaultIndex)
				.Transform(t => t
					.Script("ctx._source['foo'] = ctx._source['bar'];")
					.Language("groovy")
					.Params(new Dictionary<string, string> { { "variable", "value" } })
				)
				.Transform(t => t
					.Script("ctx._source['suggest'] = ctx._source['content']")
				)
			);
			this.DefaultResponseAssertations(putResult);

			var getResult = this.Client.GetMapping<ElasticsearchProject>(g => g
				.Index(ElasticsearchConfiguration.DefaultIndex)
			);

			getResult.IsValid.Should().BeTrue();
			getResult.Mapping.Transform.Count.Should().Be(2);
		}
	}
}
