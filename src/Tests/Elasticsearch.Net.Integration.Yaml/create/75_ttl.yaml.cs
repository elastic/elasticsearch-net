using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Create10
{
	public partial class Create10YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Ttl1Tests : YamlTestsBase
		{
			[Test]
			public void Ttl1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_ttl= new {
								enabled= "1",
								store= "yes",
								@default= "10s"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("op_type", @"create")
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("fields", @"_ttl")
				));

				//lt _response.fields._ttl: 10000; 
				this.IsLowerThan(_response.fields._ttl, 10000);

				//gt _response.fields._ttl: 0; 
				this.IsGreaterThan(_response.fields._ttl, 0);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1"));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("ttl", 100000)
					.AddQueryString("op_type", @"create")
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("fields", @"_ttl")
				));

				//lt _response.fields._ttl: 100000; 
				this.IsLowerThan(_response.fields._ttl, 100000);

				//gt _response.fields._ttl: 0; 
				this.IsGreaterThan(_response.fields._ttl, 0);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1"));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("ttl", @"20s")
					.AddQueryString("op_type", @"create")
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("fields", @"_ttl")
				));

				//lt _response.fields._ttl: 20000; 
				this.IsLowerThan(_response.fields._ttl, 20000);

				//gt _response.fields._ttl: 0; 
				this.IsGreaterThan(_response.fields._ttl, 0);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1"));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("ttl", @"20s")
					.AddQueryString("timestamp", @"2013-06-23T18:14:40")
					.AddQueryString("op_type", @"create")
				), shouldCatch: @"/AlreadyExpiredException/");

			}
		}
	}
}

