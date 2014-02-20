using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get4
{
	public partial class Get4YamlTests
	{	
	
		public class Get430ParentYamlBase : YamlTestsBase
		{
			public Get430ParentYamlBase() : base()
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
					.Add("wait_for_status", @"yellow")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("parent", @"ä¸­æ–‡")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Parent2Tests : Get430ParentYamlBase
		{
			[Test]
			public void Parent2Test()
			{	

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("parent", @"ä¸­æ–‡")
					.Add("fields", new [] {
						@"_parent",
						@"_routing"
					})
				));

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response.fields._parent: 
				this.IsMatch(_response.fields._parent, @"ä¸­æ–‡");

				//match _response.fields._routing: 
				this.IsMatch(_response.fields._routing, @"ä¸­æ–‡");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ParentOmitted3Tests : Get430ParentYamlBase
		{
			[Test]
			public void ParentOmitted3Test()
			{	

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"), shouldCatch: @"request");

			}
		}
	}
}

