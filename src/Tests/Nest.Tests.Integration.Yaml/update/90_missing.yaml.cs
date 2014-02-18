using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update16
{
	public partial class Update16YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentPartialDoc1Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentPartialDoc1Test()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body), shouldCatch: @"missing");

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("ignore", 404)
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentScript2Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentScript2Test()
			{	

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body), shouldCatch: @"missing");

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("ignore", 404)
				));

			}
		}
	}
}

