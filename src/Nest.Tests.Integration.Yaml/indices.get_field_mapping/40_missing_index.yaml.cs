using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMappingTests
	{	


		public class Raise404WhenIndexDoesntExistTests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenIndexDoesntExistTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("test_index", "type", "field");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

