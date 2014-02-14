using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Index1
{
	public partial class Index1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexWithId1Tests : YamlTestsBase
		{
			[Test]
			public void IndexWithId1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test-weird-index-ä¸­æ–‡", "weird.type", "1", _body));

				//match _response._index: 
				this.IsMatch(_response._index, @"test-weird-index-ä¸­æ–‡");

				//match _response._type: 
				this.IsMatch(_response._type, @"weird.type");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//do get 
				this.Do(()=> this._client.Get("test-weird-index-ä¸­æ–‡", "weird.type", "1"));

				//match _response._index: 
				this.IsMatch(_response._index, @"test-weird-index-ä¸­æ–‡");

				//match _response._type: 
				this.IsMatch(_response._type, @"weird.type");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

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

