using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Delete2
{
	public partial class Delete2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class InternalVersion1Tests : YamlTestsBase
		{
			[Test]
			public void InternalVersion1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 2)
				), shouldCatch: @"conflict");

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("version", 1)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 2);

			}
		}
	}
}

