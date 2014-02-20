using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get2
{
	public partial class Get2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DefaultValues1Tests : YamlTestsBase
		{
			[Test]
			public void DefaultValues1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "_all", "1"));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._source: 
				this.IsMatch(_response._source, new {
					foo= "bar"
				});

			}
		}
	}
}

