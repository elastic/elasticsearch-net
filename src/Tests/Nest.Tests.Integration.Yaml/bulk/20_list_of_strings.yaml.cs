using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Bulk2
{
	public partial class Bulk2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ListOfStrings1Tests : YamlTestsBase
		{
			[Test]
			public void ListOfStrings1Test()
			{	

				//do bulk 
				_body = new [] {
					@"{""index"": {""_index"": ""test_index"", ""_type"": ""test_type"", ""_id"": ""test_id""}}",
					@"{""f1"": ""v1"", ""f2"": 42}",
					@"{""index"": {""_index"": ""test_index"", ""_type"": ""test_type"", ""_id"": ""test_id2""}}",
					@"{""f1"": ""v2"", ""f2"": 47}"
				};
				this.Do(()=> _client.Bulk(_body, nv=>nv
					.Add("refresh", @"true")
				));

				//do count 
				this.Do(()=> _client.CountGet("test_index"));

				//match _response.count: 
				this.IsMatch(_response.count, 2);

			}
		}
	}
}

