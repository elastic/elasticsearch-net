using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update1
{
	public partial class Update1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PartialDocument1Tests : YamlTestsBase
		{
			[Test]
			public void PartialDocument1Test()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1",
					nested= new {
						one= "1",
						two= "2"
					}
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do update 
				_body = new {
					doc= new {
						foo= "baz",
						nested= new {
							one= "3"
						}
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._version: 
				this.IsMatch(_response._version, 2);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"baz");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 1);

				//match _response._source.nested.one: 
				this.IsMatch(_response._source.nested.one, 3);

				//match _response._source.nested.two: 
				this.IsMatch(_response._source.nested.two, 2);

			}
		}
	}
}

