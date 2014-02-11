using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutTemplate
{
	public partial class IndicesPutTemplateTests
	{	


		public class PutTemplateTests : YamlTestsBase
		{
			[Test]
			public void PutTemplateTest()
			{	

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				_status = this._client.IndicesPutTemplatePost("test", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do indices.get_template 
				_status = this._client.IndicesGetTemplate("test");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

