using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Info
{
	public partial class InfoTests
	{	


		public class InfoReturnsBodyTests : YamlTestsBase
		{
			[Test]
			public void InfoReturnsBodyTest()
			{	

				//do info 
				_status = this._client.InfoGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//is_true .name; 
				this.IsTrue(_response.name);

				//is_true .tagline; 
				this.IsTrue(_response.tagline);

				//is_true .version; 
				this.IsTrue(_response.version);

				//is_true .version.number; 
				this.IsTrue(_response.version.number);

			}
		}
	}
}

