using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1304Tests : IntegrationTests
	{
		[Test]
		public void AndFilterSerializationTest()
		{
			var descriptor1 = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(5)
				.Query(q => q
					.Filtered(f => f
						.Query(qq => qq
							.QueryString(qs => qs
								.Query("*")
							)
						)
						.Filter(ff => ff
							.And(a => a
								.Range(r => r
									.OnField(p => p.StartedOn)
									.GreaterOrEquals("2000-10-10T00:00:00")
								.LowerOrEquals("2000-10-10T00:00:00")
								),
								a => a.Exists(p => p.Name),
								a => a.Exists(p => p.Country),
								a => a.Exists(p => p.Content)
							)
						)
					)
				);
			var json1Bytes = this.Client.Serializer.Serialize(descriptor1);
			var json1String = Encoding.UTF8.GetString(json1Bytes);

			var descriptor2 = this.Client.Serializer.Deserialize<SearchDescriptor<ElasticsearchProject>>(new MemoryStream(json1Bytes));
			var json2Bytes = this.Client.Serializer.Serialize(descriptor2);
			var json2String = Encoding.UTF8.GetString(json2Bytes);
			
			JToken.DeepEquals(JObject.Parse(json1String), JObject.Parse(json2String)).Should().BeTrue();
			
			var result = this.Client.Search<ElasticsearchProject>(descriptor1);
			result.IsValid.Should().BeTrue();
		}
	}
}
