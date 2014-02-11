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


		public class LuceneVersionTests : YamlTestsBase
		{
			[Test]
			public void LuceneVersionTest()
			{	

				//do info 
				_status = this._client.InfoGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .version.lucene_version; 
				this.IsTrue(_response.version.lucene_version);

			}
		}
	}
}

