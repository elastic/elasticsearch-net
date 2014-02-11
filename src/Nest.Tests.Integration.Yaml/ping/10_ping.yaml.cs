using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Ping
{
	public partial class PingTests
	{	


		public class PingReturnsTrueTests : YamlTestsBase
		{
			[Test]
			public void PingReturnsTrueTest()
			{	

				//do ping 
				_status = this._client.PingHead();
				_response = _status.Deserialize<dynamic>();

				//is_true ; 
				this.IsTrue(_response);

			}
		}
	}
}

