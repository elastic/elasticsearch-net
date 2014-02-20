using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.GetSource1
{
	public partial class GetSource1YamlTests
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

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1"));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "_all", "1"));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

			}
		}
	}
}

