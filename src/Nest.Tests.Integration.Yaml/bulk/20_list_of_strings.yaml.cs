using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Bulk
{
	public partial class BulkTests
	{	


		public class ListOfStringsTests : YamlTestsBase
		{
			[Test]
			public void ListOfStringsTest()
			{	

				//do bulk 
				_body = @"""{\""index\"": {\""_index\"": \""test_index\"", \""_type\"": \""test_type\"", \""_id\"": \""test_id\""}}""
""{\""f1\"": \""v1\"", \""f2\"": 42}""
""{\""index\"": {\""_index\"": \""test_index\"", \""_type\"": \""test_type\"", \""_id\"": \""test_id2\""}}""
""{\""f1\"": \""v2\"", \""f2\"": 47}""";				_status = this._client.BulkPost(_body, nv=>nv
					.Add("refresh","true")
				);
				_response = _status.Deserialize<dynamic>();

				//do count 
				_status = this._client.CountGet("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

