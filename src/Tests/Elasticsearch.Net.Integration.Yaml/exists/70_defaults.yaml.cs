using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Exists6
{
	public partial class Exists6YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ClientSideDefaultType1Tests : YamlTestsBase
		{
			[Test]
			public void ClientSideDefaultType1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do exists 
				this.Do(()=> _client.Exists("test_1", "_all", "1"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}
	}
}

