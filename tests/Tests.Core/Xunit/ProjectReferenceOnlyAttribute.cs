using System;
using Elastic.Xunit.XunitPlumbing;
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
