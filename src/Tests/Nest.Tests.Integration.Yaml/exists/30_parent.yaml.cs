using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Exists2
{
	public partial class Exists2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
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
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Parent2Tests : YamlTestsBase
		{
			[Test]
			public void Parent2Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("parent", 5)
				));

				//do exists 
				this.Do(()=> this._client.ExistsHead("test_1", "test", "1", nv=>nv
					.Add("parent", 5)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ParentOmitted3Tests : YamlTestsBase
		{
			[Test]
			public void ParentOmitted3Test()
			{	

				//do exists 
				this.Do(()=> this._client.ExistsHead("test_1", "test", "1"), shouldCatch: @"request");

			}
		}
	}
}

