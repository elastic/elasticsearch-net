using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Bulk1
{
	public partial class Bulk1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ArrayOfObjects1Tests : YamlTestsBase
		{
			[Test]
			public void ArrayOfObjects1Test()
			{	

				//do bulk 
				_body = new dynamic[] {
					new {index=new {_index="test_index",_type="test_type",_id="test_id"}},
					new {f1="v1",f2="42"},
					new {index=new {_index="test_index",_type="test_type",_id="test_id2"}},
					new {f1="v2",f2="47"}
				};
				this.Do(()=> _client.Bulk(_body, nv=>nv
					.AddQueryString("refresh", @"true")
				));

				//do count 
				this.Do(()=> _client.CountGet("test_index"));

				//match _response.count: 
				this.IsMatch(_response.count, 2);

			}
		}
	}
}

