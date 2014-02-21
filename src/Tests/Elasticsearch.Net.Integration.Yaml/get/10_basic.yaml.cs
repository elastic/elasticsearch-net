using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Get1
{
	public partial class Get1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Basic1Tests : YamlTestsBase
		{
			[Test]
			public void Basic1Test()
			{	

				//do index 
				_body = new {
					foo= "Hello= Ã¤Â¸Â­Ã¦â€“â€¡"
				};
				this.Do(()=> _client.Index("test_1", "test", "ä¸­æ–‡", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "ä¸­æ–‡"));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, @"ä¸­æ–‡");

				//match _response._source: 
				this.IsMatch(_response._source, new {
					foo= "Hello= Ã¤Â¸Â­Ã¦â€“â€¡"
				});

				//do get 
				this.Do(()=> _client.Get("test_1", "_all", "ä¸­æ–‡"));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, @"ä¸­æ–‡");

				//match _response._source: 
				this.IsMatch(_response._source, new {
					foo= "Hello= Ã¤Â¸Â­Ã¦â€“â€¡"
				});

			}
		}
	}
}

