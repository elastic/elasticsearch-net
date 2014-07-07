using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update16
{
	public partial class Update16YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentPartialDoc1Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentPartialDoc1Test()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body), shouldCatch: @"missing");

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("ignore", 404)
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentScript2Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentScript2Test()
			{	

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body), shouldCatch: @"missing");

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("ignore", 404)
				));

			}
		}
	}
}

