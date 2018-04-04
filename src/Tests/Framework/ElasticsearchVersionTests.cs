using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Framework.Versions;

namespace Tests.Framework
{
	public class MockElasticsearchVersionResolver : ElasticsearchVersionResolver
	{
		public MockElasticsearchVersionResolver() { }

		public override string LatestSnapshot => "7.3.1-SNAPSHOT";
		public override string LatestVersion => "7.3.1";
		public override string SnapshotZipFilename(string version)
		{
			return $"{version}.zip";
		}
	}

	public class ElasticsearchVersionTests
	{
		public ElasticsearchVersion Create(string version) => ElasticsearchVersion.Create(version, new MockElasticsearchVersionResolver());

		[U] public void ReleaseVersion()
		{
			var v = Create("7.6.0");
			v.State.Should().Be(ElasticsearchVersion.ReleaseState.Released);
		}
		[U] public void BuildCandidate()
		{
			var v = Create("0527a3c4:7.6.0");
			v.State.Should().Be(ElasticsearchVersion.ReleaseState.BuildCandidate);
			v.LocalFolderName.Should().Be("7.6.0-bc+0527a3c4");
		}
		[U] public void Snapshot()
		{
			var v = Create("7.6.0-SNAPSHOT");
			v.State.Should().Be(ElasticsearchVersion.ReleaseState.Snapshot);
			v.LocalFolderName.Should().Be("7.6.0-SNAPSHOT");
		}
		[U] public void Latest()
		{
			var v = Create("latest");
			v.State.Should().Be(ElasticsearchVersion.ReleaseState.Released);
			v.Version.Should().Be("7.3.1");
			v.LocalFolderName.Should().Be("7.3.1");
		}

	}
}
