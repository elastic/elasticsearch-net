// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.XPack.MachineLearning.ValidateDetector
{
	public class ValidateDetectorApiTests
		: MachineLearningIntegrationTestBase<ValidateDetectorResponse, IValidateDetectorRequest, ValidateDetectorDescriptor<Project>,
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
			(client, f) => client.MachineLearning.ValidateDetector(f),
			(client, f) => client.MachineLearning.ValidateDetectorAsync(f),
			(client, r) => client.MachineLearning.ValidateDetector(r),
			(client, r) => client.MachineLearning.ValidateDetectorAsync(r)
		);

		protected override void ExpectResponse(ValidateDetectorResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
