// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using System.Runtime.Serialization;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using Xunit;
using static Nest.Infer;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Search.Request
{
	/** Allows to control how the `_source` field is returned with every hit.
	 * By default operations return the contents of the `_source` field unless
	 * you have used the fields parameter or if the `_source` field is disabled.
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-request-body.html#request-body-search-source-filtering[Source Filtering] for more detail.
	 */
	public class SourceFilteringUsageTests : SearchUsageTestBase
	{
		public SourceFilteringUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			query = ProjectFilterExpectedJson,
			_source = new
			{
				includes = new[] { "*" },
				excludes = new[] { "description" }
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => ProjectFilter)
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
				Query = ProjectFilter,
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
	public class SourceFilteringSerializationTests
	{
		[U]
		public void CanDeserializeBoolean()
		{
			var falseCase = Expect("{ \"_source\": false }").DeserializesTo<WithSourceFilterProperty>();
			falseCase.Should().NotBeNull();
			falseCase.SourceFilter.Should().NotBeNull();
			falseCase.SourceFilter.Match
			(b => b.Should().BeFalse(),
				f => Assert.True(false, "Expected bool but found ISourceFilter")
			);

			var trueCase = Expect("{ \"_source\": true }").DeserializesTo<WithSourceFilterProperty>();
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
			var o = Expect("{ \"_source\": [\"obj.*\"] }").DeserializesTo<WithSourceFilterProperty>();
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
			var o = Expect("{ \"_source\": \"obj.*\" }").DeserializesTo<WithSourceFilterProperty>();
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
			var o = Expect("{ \"_source\": { \"includes\": [\"obj.*\"], \"excludes\": [\"foo.*\"] } }").DeserializesTo<WithSourceFilterProperty>();
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
		}

		internal class WithSourceFilterProperty
		{
			[PropertyName("_source")] [DataMember(Name ="_source")]
			public Union<bool, ISourceFilter> SourceFilter { get; set; }
		}
	}
}
