using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesExistsTemplate1
{
	public partial class IndicesExistsTemplate1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.delete_template 
				this.Do(()=> this._client.IndicesDeleteTemplateForAll("test", nv=>nv
					.Add("ignore", new [] {
						@"404"
					})
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExistsTemplate2Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesExistsTemplate2Test()
			{	

				//do indices.exists_template 
				this.Do(()=> this._client.IndicesExistsTemplateHeadForAll("test"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> this._client.IndicesPutTemplateForAll("test", _body));

				//do indices.exists_template 
				this.Do(()=> this._client.IndicesExistsTemplateHeadForAll("test"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExistsTemplateWithLocalFlag3Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesExistsTemplateWithLocalFlag3Test()
			{	

				//do indices.exists_template 
				this.Do(()=> this._client.IndicesExistsTemplateHeadForAll("test", nv=>nv
					.Add("local", @"true")
				));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

