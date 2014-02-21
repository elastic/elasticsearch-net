using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Create4
{
	public partial class Create4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExternalVersion1Tests : YamlTestsBase
		{
			[Test]
			public void ExternalVersion1Test()
			{	

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("version_type", @"external")
					.Add("version", 5)
					.Add("op_type", @"create")
				));

				//match _response._version: 
				this.IsMatch(_response._version, 5);

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("version_type", @"external")
					.Add("version", 5)
					.Add("op_type", @"create")
				), shouldCatch: @"conflict");

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("version_type", @"external")
					.Add("version", 6)
					.Add("op_type", @"create")
				), shouldCatch: @"conflict");

			}
		}
	}
}

