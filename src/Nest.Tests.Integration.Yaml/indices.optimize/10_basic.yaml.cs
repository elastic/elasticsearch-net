using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesOptimize
{
	public partial class IndicesOptimizeTests
	{	


		public class OptimizeIndexTestsTests : YamlTestsBase
		{
			[Test]
			public void OptimizeIndexTestsTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("testing", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.optimize 
				_status = this._client.IndicesOptimizeGet("testing", nv=>nv
					.Add("max_num_segments","1")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

