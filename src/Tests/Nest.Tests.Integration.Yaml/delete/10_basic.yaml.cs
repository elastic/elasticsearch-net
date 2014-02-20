using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Delete1
{
	public partial class Delete1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Basic1Tests : YamlTestsBase
		{
			[Test]
			public void Basic1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//match _response._version: 
				this.IsMatch(_response._version, 1);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1"));

				//match _response._version: 
				this.IsMatch(_response._version, 2);

			}
		}
	}
}

