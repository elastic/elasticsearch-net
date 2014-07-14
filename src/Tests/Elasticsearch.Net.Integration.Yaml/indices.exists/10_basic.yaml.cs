using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesExists1
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
				this.Do(()=> _client.IndicesExists("test_index"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do indices.exists 
				this.Do(()=> _client.IndicesExists("test_index"));

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
				this.Do(()=> _client.IndicesExists("test_index", nv=>nv
					.AddQueryString("local", @"true")
				));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

