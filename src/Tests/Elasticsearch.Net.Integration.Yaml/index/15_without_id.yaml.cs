using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Index2
{
	public partial class Index2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexWithoutId1Tests : YamlTestsBase
		{
			[Test]
			public void IndexWithoutId1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", _body));

				//is_true _response._id; 
				this.IsTrue(_response._id);

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//set id = _response._id; 
				var id = _response._id;

				//do get 
				this.Do(()=> _client.Get("test_1", "test", (string)id));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, (string)id);

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//match _response._source: 
				this.IsMatch(_response._source, new {
					foo= "bar"
				});

			}
		}
	}
}

