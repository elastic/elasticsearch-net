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
		public class ClientSideDefaultTypeTests : YamlTestsBase
		{
			[Test]
			public void ClientSideDefaultTypeTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do exists 
				this.Do(()=> this._client.ExistsHead("test_1", "_all", "1"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}
	}
}

