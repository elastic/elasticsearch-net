using System;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Nest;
using FluentAssertions;
using Tests.Framework;
using Newtonsoft.Json;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;
using Xunit;

namespace Tests.Search.Request
{
	/** Allows to control how the `_source` field is returned with every hit.
	 * By default operations return the contents of the `_source` field unless
	 * you have used the fields parameter or if the `_source` field is disabled.
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-request-source-filtering.html[Source Filtering] for more detail.
	 */
	public class SourceFilteringUsageTests : SearchUsageTestBase
	{
		public SourceFilteringUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				_source = new
				{
					includes = new[] { "*" },
					excludes = new[] { "description" }
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Source(src => src
				.IncludeAll()
				.Excludes(e => e
					.Fields(
						p => p.Description
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Source = new SourceFilter
				{
					Includes = "*",
					Excludes = Fields<Project>(p => p.Description)
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			foreach (var document in response.Documents)
			{
				document.Name.Should().NotBeNull();
				document.StartedOn.Should().NotBe(default(DateTime));
				document.Description.Should().BeNull();
			}
		}
	}

	public class SourceFilteringDisabledUsageTests : SearchUsageTestBase
	{
		public SourceFilteringDisabledUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				_source = false
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s.Source(false);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Source = false
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			foreach (var hit in response.Hits)
				hit.Source.Should().BeNull();
		}
	}

	//hide
	public class SourceFilteringSerializationTests : SerializationTestBase
	{
		internal class WithSourceFilterProperty
		{
			[PropertyName("_source"), JsonProperty("_source")]
			public Union<bool, ISourceFilter> SourceFilter { get; set; }
		}

		[U]
		public void CanDeserializeBoolean()
		{
			var falseCase = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": false }");
			falseCase.Should().NotBeNull();
			falseCase.SourceFilter.Should().NotBeNull();
			falseCase.SourceFilter.Match
				(b => b.Should().BeFalse(),
				f => Assert.True(false, "Expected bool but found ISourceFilter")
			);

			var trueCase = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": true }");
			trueCase.Should().NotBeNull();
			trueCase.SourceFilter.Should().NotBeNull();
			trueCase.SourceFilter.Match
				(b => b.Should().BeTrue(),
				f => Assert.True(false, "Expected bool but found ISourceFilter")
			);
		}

		[U]
		public void CanDeserializeArray()
		{
			var o = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": [\"obj.*\"] }");
			o.Should().NotBeNull();
			o.SourceFilter.Match(
				b => Assert.True(false, "Expected ISourceFilter but found bool"),
				f =>
				{
					f.Should().NotBeNull();
					f.Includes.Should().Contain("obj.*");
				}
			);

		}

		[U]
		public void CanDeserializeString()
		{
			var o = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": \"obj.*\" }");
			o.Should().NotBeNull();
			o.SourceFilter.Match(
				b => Assert.True(false, "Expected ISourceFilter but found bool"),
				f =>
				{
					f.Should().NotBeNull();
					f.Includes.Should().Contain("obj.*");
				}
			);
		}

		[U]
		public void CanDeserializeObject()
		{
			var o = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": { \"includes\": [\"obj.*\"], \"excludes\": [\"foo.*\"] } }");
			o.Should().NotBeNull();
			o.SourceFilter.Match(
				b => Assert.True(false, "Expected ISourceFilter but found bool"),
				f =>
				{
					f.Should().NotBeNull();
					f.Includes.Should().Contain("obj.*");
					f.Excludes.Should().Contain("foo.*");
				}
			);
;
		}
	}
}
