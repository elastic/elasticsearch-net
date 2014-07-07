using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Create1
{
	public partial class Create1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateWithId1Tests : YamlTestsBase
		{
			[Test]
			public void CreateWithId1Test()
			{	

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("op_type", @"create")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//match _response._source: 
				this.IsMatch(_response._source, new {
					foo= "bar"
				});

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("op_type", @"create")
				), shouldCatch: @"conflict");

			}
		}
	}
}

