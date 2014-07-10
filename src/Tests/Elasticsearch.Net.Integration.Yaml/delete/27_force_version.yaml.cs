using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Delete5
{
	public partial class Delete5YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ForceVersion1Tests : YamlTestsBase
		{
			[Test]
			public void ForceVersion1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version_type", @"force")
					.AddQueryString("version", 5)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 5);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("version_type", @"force")
					.AddQueryString("version", 4)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 4);

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version_type", @"force")
					.AddQueryString("version", 6)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 6);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("version_type", @"force")
					.AddQueryString("version", 6)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 6);

			}
		}
	}
}

