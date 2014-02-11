using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesValidateQuery
{
	public partial class IndicesValidateQueryTests
	{	


		public class ValidateQueryApiTests : YamlTestsBase
		{
			[Test]
			public void ValidateQueryApiTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("testing", null);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.validate_query 
				_status = this._client.IndicesValidateQueryGet(nv=>nv
					.Add("q","query string")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true .valid; 
				this.IsTrue(_response.valid);

				//do indices.validate_query 
				_body = new {
					query= new {
						invalid_query= new {}
					}
				};
				_status = this._client.IndicesValidateQueryPost(_body);
				_response = _status.Deserialize<dynamic>();

				//is_false .valid; 
				this.IsFalse(_response.valid);

			}
		}
	}
}

