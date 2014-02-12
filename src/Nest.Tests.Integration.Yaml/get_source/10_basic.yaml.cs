using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.GetSource
{
	public partial class GetSourceTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTests : YamlTestsBase
		{
			[Test]
			public void BasicTest()
			{	

				//skip 0 - 0.90.0; 
				this.Skip("0 - 0.90.0", "Get source not supported in pre 0.90.1 versions.");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do get_source 
				this.Do(()=> this._client.GetSource("test_1", "test", "1"));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

				//do get_source 
				this.Do(()=> this._client.GetSource("test_1", "_all", "1"));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

			}
		}
	}
}

