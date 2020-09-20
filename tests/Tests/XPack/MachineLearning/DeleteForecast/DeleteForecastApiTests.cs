// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.DeleteForecast
{
	public class DeleteForecastApiTests
		: MachineLearningIntegrationTestBase<DeleteForecastResponse, IDeleteForecastRequest, DeleteForecastDescriptor, DeleteForecastRequest>
	{
		public DeleteForecastApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private const int BucketSpanSeconds = 3600;

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteForecastDescriptor, IDeleteForecastRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteForecastRequest Initializer => new DeleteForecastRequest(CallIsolatedValue + "-job", CallIsolatedValue);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue + "-job"}/_forecast/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putJobResponse = client.MachineLearning.PutJob<object>(callUniqueValue.Value + "-job", f => f
					.Description("DeleteForecastApiTests")
					.AnalysisConfig(a => a
						.BucketSpan($"{BucketSpanSeconds}s")
						.Detectors(d => d
							.Mean(m => m
								.FieldName("value")
							)
						)
					)
					.DataDescription(d => d
						.TimeFormat("epoch")
					)
				);

				if (!putJobResponse.IsValid)
					throw new Exception($"Problem putting job {callUniqueValue.Value} for integration test: {putJobResponse.DebugInformation}");

				OpenJob(client, callUniqueValue.Value + "-job");
				IndexForecast(client, callUniqueValue.Value + "-job", callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				CloseJob(client, callUniqueValue.Value + "-job");
				DeleteJob(client, callUniqueValue.Value + "-job");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteForecast(CallIsolatedValue + "-job", CallIsolatedValue),
			(client, f) => client.MachineLearning.DeleteForecastAsync(CallIsolatedValue + "-job", CallIsolatedValue),
			(client, r) => client.MachineLearning.DeleteForecast(r),
			(client, r) => client.MachineLearning.DeleteForecastAsync(r)
		);

		protected override DeleteForecastDescriptor NewDescriptor() => new DeleteForecastDescriptor(CallIsolatedValue + "-job", CallIsolatedValue);

		protected override void ExpectResponse(DeleteForecastResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
