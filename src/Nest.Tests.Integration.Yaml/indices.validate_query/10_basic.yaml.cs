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
				this.Do(()=> this._client.IndicesCreatePost("testing", null));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				));

				//do indices.validate_query 
				this.Do(()=> this._client.IndicesValidateQueryGet(nv=>nv
					.Add("q","query string")
				));

				//is_true _response.valid; 
				this.IsTrue(_response.valid);

				//do indices.validate_query 
				_body = new {
					query= new {
						invalid_query= new {}
					}
				};
				this.Do(()=> this._client.IndicesValidateQueryPost(_body));

				//is_false _response.valid; 
				this.IsFalse(_response.valid);

			}
		}
	}
}

