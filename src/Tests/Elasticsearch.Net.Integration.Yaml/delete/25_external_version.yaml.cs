using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Delete3
{
	public partial class Delete3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExternalVersion1Tests : YamlTestsBase
		{
			[Test]
			public void ExternalVersion1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version_type", @"external")
					.AddQueryString("version", 5)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 5);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("version_type", @"external")
					.AddQueryString("version", 4)
				), shouldCatch: @"conflict");

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("version_type", @"external")
					.AddQueryString("version", 6)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 6);

			}
		}
	}
}

