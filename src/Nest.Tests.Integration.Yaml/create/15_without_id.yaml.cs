using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Create
{
	public partial class CreateTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateWithoutIdTests : YamlTestsBase
		{
			[Test]
			public void CreateWithoutIdTest()
			{	

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", _body, nv=>nv
					.Add("op_type", @"create")
				));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

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
				this.Do(()=> this._client.Get("test_1", "test", (string)id));

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

