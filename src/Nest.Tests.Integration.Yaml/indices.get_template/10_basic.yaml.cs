using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetTemplate
{
	public partial class IndicesGetTemplateTests
	{	


		public class GetTemplateTests : YamlTestsBase
		{
			[Test]
			public void GetTemplateTest()
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

				//do indices.get_template 
				_status = this._client.IndicesGetTemplate("test");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class GetAllTemplatesTests : YamlTestsBase
		{
			[Test]
			public void GetAllTemplatesTest()
			{	

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				_status = this._client.IndicesPutTemplatePost("test", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.put_template 
				_body = new {
					template= "test2-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				_status = this._client.IndicesPutTemplatePost("test2", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_template 
				_status = this._client.IndicesGetTemplate();
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

