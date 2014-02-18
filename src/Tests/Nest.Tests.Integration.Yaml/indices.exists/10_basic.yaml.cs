using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesExists1
{
	public partial class IndicesExists1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExists1Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesExists1Test()
			{	

				//do indices.exists 
				this.Do(()=> this._client.IndicesExistsHead("test_index"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index", null));

				//do indices.exists 
				this.Do(()=> this._client.IndicesExistsHead("test_index"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExistsWithLocalFlag2Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesExistsWithLocalFlag2Test()
			{	

				//do indices.exists 
				this.Do(()=> this._client.IndicesExistsHead("test_index", nv=>nv
					.Add("local", @"true")
				));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

