using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update2
{
	public partial class Update2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Script1Tests : YamlTestsBase
		{
			[Test]
			public void Script1Test()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do update 
				_body = new {
					lang= "mvel",
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("script", 1)
				));

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
				this.IsMatch(_response._source.foo, @"xxx");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 1);

				//do update 
				this.Do(()=> _client.Update("test_1", "test", "1", null, nv=>nv
					.AddQueryString("lang", @"mvel")
					.AddQueryString("script", @"ctx._source.foo = 'yyy'")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._version: 
				this.IsMatch(_response._version, 3);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"yyy");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 1);

				//do update 
				_body = new {
					script= "1",
					lang= "doesnotexist",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body), shouldCatch: @"/script_lang not supported \[doesnotexist\]/");

				//do update 
				this.Do(()=> _client.Update("test_1", "test", "1", null, nv=>nv
					.AddQueryString("lang", @"doesnotexist")
					.AddQueryString("script", 1)
				), shouldCatch: @"/script_lang not supported \[doesnotexist\]/");

			}
		}
	}
}

