// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Xpack;
using VerifyXunit;

namespace Tests.Serialization.Xpack;

[UsesVerify]
public class MachineLearningJobsSerializationTests : SerializerTestBase
{
	[U]
	public void Deserializes_Xpack_Usage_MachineLearning_Jobs()
	{
		const string json = @"{""_all"":{""count"":1,""created_by"":{},""detectors"":{""avg"":10,""max"":20,""min"":1,""total"":100},""forecasts"":
{""forecasted_jobs"":30,""total"":300},""model_size"":{""avg"":200,""max"":400,""min"":100,""total"":50}}}";

		var jobs = DeserializeJsonString<Jobs>(json);

		jobs.All.Should().NotBeNull();
		jobs.All.Count.Should().Be(1);
		jobs.All.Detectors["avg"].Should().Be(10);
		jobs.All.Detectors["max"].Should().Be(20);
		jobs.All.Detectors["min"].Should().Be(1);
		jobs.All.Detectors["total"].Should().Be(100);
		jobs.All.Forecasts["forecasted_jobs"].Should().Be(30);
		jobs.All.ModelSize["avg"].Should().Be(200);
		jobs.All.ModelSize["max"].Should().Be(400);
		jobs.All.ModelSize["min"].Should().Be(100);
		jobs.All.ModelSize["total"].Should().Be(50);
	}
}
