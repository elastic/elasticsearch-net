using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.GetSource7
{
	public partial class GetSource7YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFiltering1Tests : YamlTestsBase
		{
			[Test]
			public void SourceFiltering1Test()
			{	

				//do index 
				_body = new {
					include= new {
						field1= "v1",
						field2= "v2"
					},
					count= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.AddQueryString("_source_include", @"include.field1")
				));

				//match _response.include.field1: 
				this.IsMatch(_response.include.field1, @"v1");

				//is_false _response.include.field2; 
				this.IsFalse(_response.include.field2);

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.AddQueryString("_source_include", @"include.field1,include.field2")
				));

				//match _response.include.field1: 
				this.IsMatch(_response.include.field1, @"v1");

				//match _response.include.field2: 
				this.IsMatch(_response.include.field2, @"v2");

				//is_false _response.count; 
				this.IsFalse(_response.count);

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.AddQueryString("_source_include", @"include")
					.AddQueryString("_source_exclude", @"*.field2")
				));

				//match _response.include.field1: 
				this.IsMatch(_response.include.field1, @"v1");

				//is_false _response.include.field2; 
				this.IsFalse(_response.include.field2);

				//is_false _response.count; 
				this.IsFalse(_response.count);

			}
		}
	}
}

