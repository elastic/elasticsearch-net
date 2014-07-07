using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.ClusterReroute1
{
	public partial class ClusterReroute1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicSanityCheck1Tests : YamlTestsBase
		{
			[Test]
			public void BasicSanityCheck1Test()
			{	

				//do cluster.reroute 
				this.Do(()=> _client.ClusterReroute(null));

			}
		}
	}
}

