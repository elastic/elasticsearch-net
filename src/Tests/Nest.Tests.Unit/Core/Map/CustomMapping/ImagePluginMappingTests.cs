using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Map.CustomMapping
{
	[TestFixture]
	public class ImagePluginMappingTests : BaseJsonTests
	{

		public class ImagePluginType : IElasticType
		{
			public PropertyNameMarker Name { get; set; }
			public TypeNameMarker Type { get { return "image";  } }


			public IDictionary<string, object> Feature { get; set; }
			public IDictionary<string, object> MetaData { get; set; }
		}

		[Test]
		public void RepresentUnknowImageMappingPlugin()
		{
			var imagePluginMapping = new ImagePluginType()
			{
				Name = "my_img",
				Feature = new Dictionary<string, object>
				{
					{ "CEDD", new { hash = "BIT_SAMPLING"} },
					{ "JCD", new { hash = new [] {"BIT_SAMPLING", "LSH"}} }
				},
				MetaData = new Dictionary<string, object>
				{
					
					{	"jpeg.image_width", new {
							type = "string",
							store = "yes"
						}
					},
					{	"jpeg.image_height", new {
							type= "string",
							store= "yes"
						}
					}
				}
			};

			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(p=>p
					.Custom(imagePluginMapping)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
