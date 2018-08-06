using System;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3084 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue3084(WritableCluster cluster) => _cluster = cluster;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		public class ObjectVersion1
		{
			public int Id { get; set; }
			public double Numeric { get; set; }
		}
		public class ObjectVersion2
		{
			public int Id { get; set; }
			public int Numeric { get; set; }
		}

		[I]
		public void DeserializeErrorIsTheSameForAsync()
		{
			var client = _cluster.Client;
			var index = $"gh3084-{RandomString()}";
			var document = new ObjectVersion1 {Id = 1, Numeric = 0};

			var indexResult = client.Index(document, i => i.Index(index).Type("doc"));
			indexResult.ShouldBeValid();

			Action getDoc = () =>client.Get<ObjectVersion2>(new GetRequest(index, "doc", 1));
			Func<Task<IGetResponse<ObjectVersion2>>> getDocAsync = async () => await client.GetAsync<ObjectVersion2>(new GetRequest(index, "doc", 1));

			getDoc.ShouldThrow<Exception>("synchonous code path should throw");
			getDocAsync.ShouldThrow<Exception>("async code path should throw");
		}
	}
}
