using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Create3
{
	public partial class Create3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class InternalVersion1Tests : YamlTestsBase
		{
			[Test]
			public void InternalVersion1Test()
			{	

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("op_type", @"create")
				));

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("op_type", @"create")
				), shouldCatch: @"conflict");

			}
		}
	}
}

