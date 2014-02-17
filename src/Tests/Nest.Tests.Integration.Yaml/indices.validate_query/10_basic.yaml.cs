using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesValidateQuery1
{
	public partial class IndicesValidateQuery1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ValidateQueryApi1Tests : YamlTestsBase
		{
			[Test]
			public void ValidateQueryApi1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("testing", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do indices.validate_query 
				this.Do(()=> this._client.IndicesValidateQueryGetForAll(nv=>nv
					.Add("q", @"query string")
				));

				//is_true _response.valid; 
				this.IsTrue(_response.valid);

				//do indices.validate_query 
				_body = new {
					query= new {
						invalid_query= new {}
					}
				};
				this.Do(()=> this._client.IndicesValidateQueryPostForAll(_body));

				//is_false _response.valid; 
				this.IsFalse(_response.valid);

			}
		}
	}
}

