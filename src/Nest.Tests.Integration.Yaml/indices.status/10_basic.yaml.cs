using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesStatus
{
	public partial class IndicesStatusTests
	{	


		public class IndicesStatusTestTests : YamlTestsBase
		{
			[Test]
			public void IndicesStatusTestTest()
			{	

				//do indices.status 
				_status = this._client.IndicesStatusGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do indices.status 
				_status = this._client.IndicesStatusGet("not_here");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

