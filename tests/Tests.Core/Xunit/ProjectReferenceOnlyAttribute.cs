// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Core.Client;

namespace Tests.Core.Xunit
{
	public class ProjectReferenceOnlyAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "This test can only be run if client dependencies are project references";
		public override bool Skip => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TestPackageVersion"));
	}

	public class NotWhenUsingSourceSerializerAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skipping this test because we are randomly running with a custom source serializer";
		public override bool Skip => TestClient.Configuration.Random.SourceSerializer;
	}
}
