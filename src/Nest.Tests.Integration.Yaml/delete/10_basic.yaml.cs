using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Delete
{
	public partial class DeleteTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTests : YamlTestsBase
		{
			[Test]
			public void BasicTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//do delete 
				this.Do(()=> this._client.Delete("test_1", "test", "1"));

				//match _response._version: 
				this.IsMatch(_response._version, 2);

			}
		}
	}
}

