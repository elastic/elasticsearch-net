using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Index4
{
	public partial class Index4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class InternalVersion1Tests : YamlTestsBase
		{
			[Test]
			public void InternalVersion1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//match _response._version: 
				this.IsMatch(_response._version, 2);

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version", 1)
				), shouldCatch: @"conflict");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version", 2)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 3);

			}
		}
	}
}

