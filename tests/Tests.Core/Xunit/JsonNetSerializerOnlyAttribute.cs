// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Core.Client;

namespace Tests.Core.Xunit
{
	public class JsonNetSerializerOnlyAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skipping this test because we are not running with JsonNetSerializer";
		public override bool Skip => !TestClient.Configuration.Random.SourceSerializer;
	}
}
