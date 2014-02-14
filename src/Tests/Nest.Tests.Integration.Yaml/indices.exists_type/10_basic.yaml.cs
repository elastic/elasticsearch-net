using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesExistsType1
{
	public partial class IndicesExistsType1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExistsType1Tests : YamlTestsBase
		{
			[Test]
			public void ExistsType1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {},
						type_2= new {}
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_2", "type_1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_1", "type_3"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_1", "type_1"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExistsTypeWithLocalFlag2Tests : YamlTestsBase
		{
			[Test]
			public void ExistsTypeWithLocalFlag2Test()
			{	

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_1", "type_1", nv=>nv
					.Add("local", @"true")
				));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

