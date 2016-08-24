using System;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Nest;
using FluentAssertions;
using Tests.Framework;
using Newtonsoft.Json;
using static Nest.Infer;

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
					include = new[] { "*" },
					exclude = new [] { "description" }
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Source(src => src
				.IncludeAll()
				.Exclude(e => e
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
					Include = "*",
					Exclude = Fields<Project>(p => p.Description)
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

	/**[float]
	 * == Disable Source Retrieval
	 * Source filtering can be disabled by setting disable to `true`.
	 *
	 */
	public class SourceFilteringDisabledUsageTests : SearchUsageTestBase
	{
		public SourceFilteringDisabledUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				_source = false
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Source(src => src.Disable());

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Source = new SourceFilter
				{
					Disable = true
				}
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
			[JsonProperty("_source")]
			public ISourceFilter SourceFilter { get; set; }
		}

		[U]
		public void CanDeserializeBoolean()
		{
			var o = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": false }");
			o.Should().NotBeNull();
			o.SourceFilter.Should().NotBeNull();
			o.SourceFilter.Exclude.Should().BeNull();
			o.SourceFilter.Include.Should().BeNull();
			o.SourceFilter.Disable.Should().BeTrue();
		}

		[U]
		public void CanDeserializeArray()
		{
			var o = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": [\"obj.*\"] }");
			o.Should().NotBeNull();
			o.SourceFilter.Should().NotBeNull();
			o.SourceFilter.Include.Should().Contain("obj.*");
		}

		[U]
		public void CanDeserializeString()
		{
			var o = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": \"obj.*\" }");
			o.Should().NotBeNull();
			o.SourceFilter.Should().NotBeNull();
			o.SourceFilter.Include.Should().Contain("obj.*");
		}

		[U]
		public void CanDeserializeObject()
		{
			var o = base.Deserialize<WithSourceFilterProperty>("{ \"_source\": { \"include\": [\"obj.*\"], \"exclude\": [\"foo.*\"] } }");
			o.Should().NotBeNull();
			o.SourceFilter.Should().NotBeNull();
			o.SourceFilter.Include.Should().Contain("obj.*");
			o.SourceFilter.Exclude.Should().Contain("foo.*");
		}
	}
}
