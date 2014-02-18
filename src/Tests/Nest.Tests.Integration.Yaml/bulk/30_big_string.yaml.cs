using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Bulk3
{
	public partial class Bulk3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class OneBigString1Tests : YamlTestsBase
		{
			[Test]
			public void OneBigString1Test()
			{	

				//do bulk 
				_body = @"{""index"": {""_index"": ""test_index"", ""_type"": ""test_type"", ""_id"": ""test_id""}}
{""f1"": ""v1"", ""f2"": 42}
{""index"": {""_index"": ""test_index"", ""_type"": ""test_type"", ""_id"": ""test_id2""}}
{""f1"": ""v2"", ""f2"": 47}
";
				this.Do(()=> this._client.BulkPost(_body, nv=>nv
					.Add("refresh", @"true")
				));

				//do count 
				this.Do(()=> this._client.CountGet("test_index"));

				//match _response.count: 
				this.IsMatch(_response.count, 2);

			}
		}
	}
}

