using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesExists
{
	public partial class IndicesExistsTests
	{	


		public class TestIndicesExistsTests : YamlTestsBase
		{
			[Test]
			public void TestIndicesExistsTest()
			{	

				//do indices.exists 
				this.Do(()=> this._client.IndicesExistsHead("test_index"));

				//is_false this._status.Result; 
				this.IsFalse(this._status.Result);

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do indices.exists 
				this.Do(()=> this._client.IndicesExistsHead("test_index"));

				//is_true this._status.Result; 
				this.IsTrue(this._status.Result);

			}
		}
	}
}

