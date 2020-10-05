// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Core.Extensions
{
	public static class ShouldExtensions
	{
		public static void ShouldHaveExpectedIsValid(this IResponse response, bool expectedIsValid) =>
			response.IsValid.Should().Be(expectedIsValid, "{0}", response.DebugInformation);

		public static void ShouldBeValid(this IResponse response) =>
			response.IsValid.Should().BeTrue("{0}", response.DebugInformation);

		public static void ShouldBeValid(this IResponse response, string message) =>
			response.IsValid.Should().BeTrue("{1} {0}", response.DebugInformation, message);

		public static void ShouldNotBeValid(this IResponse response) =>
			response.IsValid.Should().BeFalse("{0}", response.DebugInformation);

		public static void ShouldBeSuccess(this IResponse response) =>
			response.ApiCall.Success.Should().BeTrue("{0}", response.DebugInformation);


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
			b.Should().BeTrue(p?.GetString(TestClient.DefaultInMemoryClient.ConnectionSettings) ?? "NULL");
	}
}
