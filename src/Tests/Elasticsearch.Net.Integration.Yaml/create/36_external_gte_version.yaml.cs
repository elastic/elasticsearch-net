using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Create5
{
	public partial class Create5YamlTests
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
					.AddQueryString("version_type", @"external_gte")
					.AddQueryString("version", 5)
					.AddQueryString("op_type", @"create")
				));

				//match _response._version: 
				this.IsMatch(_response._version, 5);

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version_type", @"external_gte")
					.AddQueryString("version", 5)
					.AddQueryString("op_type", @"create")
				), shouldCatch: @"conflict");

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version_type", @"external_gte")
					.AddQueryString("version", 6)
					.AddQueryString("op_type", @"create")
				), shouldCatch: @"conflict");

			}
		}
	}
}

