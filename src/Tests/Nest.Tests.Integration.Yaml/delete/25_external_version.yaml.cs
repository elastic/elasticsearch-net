using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Delete3
{
	public partial class Delete3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExternalVersion1Tests : YamlTestsBase
		{
			[Test]
			public void ExternalVersion1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type", @"external")
					.Add("version", 5)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 5);

				//do delete 
				this.Do(()=> this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version_type", @"external")
					.Add("version", 4)
				), shouldCatch: @"conflict");

				//do delete 
				this.Do(()=> this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version_type", @"external")
					.Add("version", 6)
				));

				//match _response._version: 
				this.IsMatch(_response._version, 6);

			}
		}
	}
}

