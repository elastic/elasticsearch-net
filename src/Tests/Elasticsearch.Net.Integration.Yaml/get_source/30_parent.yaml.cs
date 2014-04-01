using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.GetSource3
{
	public partial class GetSource3YamlTests
	{	
	
		public class GetSource330ParentYamlBase : YamlTestsBase
		{
			public GetSource330ParentYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("parent", 5)
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Parent2Tests : GetSource330ParentYamlBase
		{
			[Test]
			public void Parent2Test()
			{	

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.AddQueryString("parent", 5)
				));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ParentOmitted3Tests : GetSource330ParentYamlBase
		{
			[Test]
			public void ParentOmitted3Test()
			{	

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1"), shouldCatch: @"request");

			}
		}
	}
}

