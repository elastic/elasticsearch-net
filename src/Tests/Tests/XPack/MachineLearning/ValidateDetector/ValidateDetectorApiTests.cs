using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.XPack.MachineLearning.ValidateDetector
{
	public class ValidateDetectorApiTests
		: MachineLearningIntegrationTestBase<IValidateDetectorResponse, IValidateDetectorRequest, ValidateDetectorDescriptor<Project>,
			ValidateDetectorRequest>
	{
		public ValidateDetectorApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			detector_description = "detector description",
			function = "count",
			by_field_name = "numberOfCommits",
			over_field_name = "branches",
			partition_field_name = "leadDeveloper",
			exclude_frequent = "none",
			use_null = true,
			detector_index = 0
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ValidateDetectorDescriptor<Project>, IValidateDetectorRequest> Fluent => f => f
			.Count(c => c
				.DetectorDescription("detector description")
				.ByFieldName(p => p.NumberOfCommits)
				.OverFieldName(p => p.Branches)
				.PartitionFieldName(p => p.LeadDeveloper)
				.ExcludeFrequent(ExcludeFrequent.None)
				.UseNull()
				.DetectorIndex(0)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ValidateDetectorRequest Initializer =>
			new ValidateDetectorRequest
			{
				Detector = new CountDetector
				{
					DetectorDescription = "detector description",
					ByFieldName = Field<Project>(p => p.NumberOfCommits),
					OverFieldName = Field<Project>(p => p.Branches),
					PartitionFieldName = Field<Project>(p => p.LeadDeveloper),
					ExcludeFrequent = ExcludeFrequent.None,
					UseNull = true,
					DetectorIndex = 0
				}
			};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"_ml/anomaly_detectors/_validate/detector";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ValidateDetector(f),
			(client, f) => client.ValidateDetectorAsync(f),
			(client, r) => client.ValidateDetector(r),
			(client, r) => client.ValidateDetectorAsync(r)
		);

		protected override void ExpectResponse(IValidateDetectorResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
