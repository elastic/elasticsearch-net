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


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTests : YamlTestsBase
		{
			[Test]
			public void BasicTest()
			{	

				//do exists 
				this.Do(()=> this._client.ExistsHead("test_1", "test", "1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do exists 
				this.Do(()=> this._client.ExistsHead("test_1", "test", "1"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}
	}
}

