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


		public class RefreshTests : YamlTestsBase
		{
			[Test]
			public void RefreshTest()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						refresh_interval= "-1",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("refresh","1")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "2", _body, nv=>nv
					.Add("refresh","1")
				));

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				this.Do(()=> this._client.SearchPost("test_1", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 2);

				//do delete 
				this.Do(()=> this._client.Delete("test_1", "test", "1"));

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				this.Do(()=> this._client.SearchPost("test_1", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 2);

				//do delete 
				this.Do(()=> this._client.Delete("test_1", "test", "2", nv=>nv
					.Add("refresh","1")
				));

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				this.Do(()=> this._client.SearchPost("test_1", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 1);

			}
		}
	}
}

