using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.SignificantTerms
{
	public class SignificantTermsAggregationUsageTests : AggregationUsageTestBase
	{
		public SignificantTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				significant_names = new
				{
					significant_terms = new
					{
						field = "name",
						min_doc_count = 10,
						mutual_information = new
						{
							background_is_superset = true,
							include_negatives = true
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.SignificantTerms("significant_names", st => st
					.Field(p => p.Name)
					.MinimumDocumentCount(10)
					.MutualInformation(mi => mi
						.BackgroundIsSuperSet()
						.IncludeNegatives()
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new SignificantTermsAggregation("significant_names")
				{
					Field = Field<Project>(p => p.Name),
					MinimumDocumentCount = 10,
					MutualInformation = new MutualInformationHeuristic
					{
						BackgroundIsSuperSet = true,
						IncludeNegatives = true
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var sigNames = response.Aggs.SignificantTerms("significant_names");
			sigNames.Should().NotBeNull();
			sigNames.DocCount.Should().BeGreaterThan(0);
		}
	}
}