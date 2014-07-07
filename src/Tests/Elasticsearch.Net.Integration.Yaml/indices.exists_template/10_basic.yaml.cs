using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesExistsTemplate1
{
	public partial class IndicesExistsTemplate1YamlTests
	{	
	
		public class IndicesExistsTemplate110BasicYamlBase : YamlTestsBase
		{
			public IndicesExistsTemplate110BasicYamlBase() : base()
			{	

				//do indices.delete_template 
				this.Do(()=> _client.IndicesDeleteTemplateForAll("test", nv=>nv
					.AddQueryString("ignore", new [] {
						@"404"
					})
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExistsTemplate2Tests : IndicesExistsTemplate110BasicYamlBase
		{
			[Test]
			public void TestIndicesExistsTemplate2Test()
			{	

				//do indices.exists_template 
				this.Do(()=> _client.IndicesExistsTemplateForAll("test"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesPutTemplateForAll("test", _body));

				//do indices.exists_template 
				this.Do(()=> _client.IndicesExistsTemplateForAll("test"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExistsTemplateWithLocalFlag3Tests : IndicesExistsTemplate110BasicYamlBase
		{
			[Test]
			public void TestIndicesExistsTemplateWithLocalFlag3Test()
			{	

				//do indices.exists_template 
				this.Do(()=> _client.IndicesExistsTemplateForAll("test", nv=>nv
					.AddQueryString("local", @"true")
				));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

