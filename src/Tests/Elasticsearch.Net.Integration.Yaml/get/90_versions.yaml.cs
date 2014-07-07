using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Get10
{
	public partial class Get10YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Versions1Tests : YamlTestsBase
		{
			[Test]
			public void Versions1Test()
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

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 2)
				));

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 1)
				), shouldCatch: @"conflict");

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 2)
					.AddQueryString("version_type", @"external")
				));

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 10)
					.AddQueryString("version_type", @"external")
				), shouldCatch: @"conflict");

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 1)
					.AddQueryString("version_type", @"external")
				), shouldCatch: @"conflict");

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 2)
					.AddQueryString("version_type", @"external_gte")
				));

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 10)
					.AddQueryString("version_type", @"external_gte")
				), shouldCatch: @"conflict");

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 1)
					.AddQueryString("version_type", @"external_gte")
				), shouldCatch: @"conflict");

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 2)
					.AddQueryString("version_type", @"force")
				));

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 10)
					.AddQueryString("version_type", @"force")
				));

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 1)
					.AddQueryString("version_type", @"force")
				));

				//match _response._id: 
				this.IsMatch(_response._id, 1);

			}
		}
	}
}

