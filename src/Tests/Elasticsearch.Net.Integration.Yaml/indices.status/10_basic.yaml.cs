using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStatus1
{
	public partial class IndicesStatus1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndicesStatusTest1Tests : YamlTestsBase
		{
			[Test]
			public void IndicesStatusTest1Test()
			{	

				//do indices.status 
				this.Do(()=> _client.IndicesStatusForAll());

				//do indices.status 
				this.Do(()=> _client.IndicesStatus("not_here"), shouldCatch: @"missing");

			}
		}
	}
}

