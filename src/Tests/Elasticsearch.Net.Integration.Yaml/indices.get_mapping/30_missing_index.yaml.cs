using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetMapping3
{
	public partial class IndicesGetMapping3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Raise404WhenIndexDoesntExist1Tests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenIndexDoesntExist1Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_index", "not_test_type"), shouldCatch: @"missing");

			}
		}
	}
}

