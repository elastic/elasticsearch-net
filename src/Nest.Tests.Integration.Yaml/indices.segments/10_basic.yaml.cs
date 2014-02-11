using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesSegments
{
	public partial class IndicesSegmentsTests
	{	


		public class SegmentsTestTests : YamlTestsBase
		{
			[Test]
			public void SegmentsTestTest()
			{	

				//do indices.segments 
				_status = this._client.IndicesSegmentsGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

