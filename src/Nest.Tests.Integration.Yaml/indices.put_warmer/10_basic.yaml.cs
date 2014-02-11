using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutWarmer
{
	public partial class IndicesPutWarmerTests
	{	


		public class BasicTestForWarmersTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForWarmersTest()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_index", "test_warmer"));

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmer("test_index", "test_warmer", _body));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_index", "test_warmer"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//do indices.delete_warmer 
				this.Do(()=> this._client.IndicesDeleteWarmer("test_index"));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_index", "test_warmer"));

			}
		}
	}
}

