using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Repository
{
	[TestFixture]
	public class GetRepositoryTests : IntegrationTests
	{
		[Test]
		public void CreateARepositoryThenGetIt()
		{
			var repository = ElasticsearchConfiguration.NewUniqueIndexName();
			var createReposResult = this.Client.CreateRepository(repository, r => r
				.FileSystem(@"local\\path", o => o
					.Compress()
					.ConcurrentStreams(10)
				)
			);
			createReposResult.IsValid.Should().BeTrue();
			createReposResult.Acknowledged.Should().BeTrue();

			var t = this.Client.GetRepository(new GetRepositoryRequest(repository));
			t.IsValid.Should().BeTrue();
			t.Repositories.Should().NotBeEmpty();

			var repos = t.Repositories.First();
			repos.Key.Should().Be(repository);
			repos.Value.Should().NotBeNull();
			repos.Value.Type.Should().Be("fs");
			repos.Value.Settings.Should().NotBeEmpty()
				.And.HaveCount(3)
				.And.ContainKey("compress")
				.And.ContainKey("concurrent_streams")
				.And.ContainKey("location");

			var deleteReposResult = this.Client.DeleteRepository(repository);
			deleteReposResult.IsValid.Should().BeTrue();
			deleteReposResult.Acknowledged.Should().BeTrue();
		}

	}
}
