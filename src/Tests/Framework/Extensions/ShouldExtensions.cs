using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public static class ShouldExtensions
	{
		public static void ShouldHaveExpectedIsValid(this IResponse response, bool expectedIsValid) =>
			response.IsValid.Should().Be(expectedIsValid, "{0}", response.DebugInformation);

		public static void ShouldBeValid(this IResponse response) =>
			response.IsValid.Should().BeTrue("{0}", response.DebugInformation);

		public static void ShouldNotBeValid(this IResponse response) =>
			response.IsValid.Should().BeFalse("{0}", response.DebugInformation);


		public static void ShouldAdhereToSourceSerializerWhenSet(this Project project)
		{
			var usingSourceSerializer = TestClient.Configuration.Random.SourceSerializer;
			project.Should().NotBeNull();
			if (!usingSourceSerializer)
			{
				project.SourceOnly.Should().BeNull();
				return;
			}
			project.SourceOnly.Should().NotBeNull();
			project.SourceOnly.NotWrittenByDefaultSerializer.Should().Be("written");
			project.SourceOnly.NotReadByDefaultSerializer.Should().Be("read");
		}

		public static void ShouldBeTrue(this bool b, IUrlParameter p) =>
			b.Should().BeTrue(p?.GetString(TestClient.GlobalDefaultSettings) ?? "NULL");
	}
}
