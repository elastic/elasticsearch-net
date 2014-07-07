using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Get9
{
	public partial class Get9YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithCatch1Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithCatch1Test()
			{	

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithIgnore2Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithIgnore2Test()
			{	

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("ignore", 404)
				));

			}
		}
	}
}

