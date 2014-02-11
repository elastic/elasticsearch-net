using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Exists
{
	public partial class ExistsTests
	{	


		public class ParentTests : YamlTestsBase
		{
			[Test]
			public void ParentTest()
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

				//do exists 
				this.Do(()=> this._client.ExistsHead("test_1", "test", "1"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

