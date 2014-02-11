using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Create
{
	public partial class CreateTests
	{	


		public class ExternalVersionTests : YamlTestsBase
		{
			[Test]
			public void ExternalVersionTest()
			{	

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","5")
					.Add("op_type","create")
				));

				//match _response._version: 
				this.IsMatch(_response._version, 5);

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","5")
					.Add("op_type","create")
				));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","6")
					.Add("op_type","create")
				));

			}
		}
	}
}

