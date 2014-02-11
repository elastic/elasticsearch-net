using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get
{
	public partial class GetTests
	{	


		public class BasicTests : YamlTestsBase
		{
			[Test]
			public void BasicTest()
			{	

				//do index 
				_body = new {
					foo= "Hello= Ã¤Â¸Â­Ã¦â€“â€¡"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "Ã¤Â¸Â­Ã¦â€“â€¡", _body));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "Ã¤Â¸Â­Ã¦â€“â€¡"));

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
				this.Do(()=> this._client.Get("test_1", "_all", "Ã¤Â¸Â­Ã¦â€“â€¡"));

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

