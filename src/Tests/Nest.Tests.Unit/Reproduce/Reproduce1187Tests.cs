using System.IO;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce1187Tests : BaseJsonTests
	{
		[Test]
		public void IsClientSideSearchQueryDeserialisable()
		{
			var newDescriptor = Deserialize<SearchDescriptor<dynamic>>(original);
			var actual = Serialize(newDescriptor);//Should is empty

			var descriptorJobject = JObject.Parse(actual);
			var expressionList = new object[][]
	                             {
	                                 new[] {"size"},//Works
	                                 new[] {"from"},//Works
	                                 new[] {"query", "filtered", "query", "query_string", "fields"},//Works
	                                 new[] {"query", "filtered", "filter", "bool", "must"},//Works
//Can't find contents of should
                                     new object[] {"query", "filtered", "filter", "bool", "should", 0, "indices", "filter"},
                                     new object[] {"query", "filtered", "filter", "bool", "should", 0, "indices", "no_match_filter"},
	                                 new[] {"aggs", "Databases", "terms", "field"},//Works
	                                 new[] {"aggs", "Year", "terms", "field"}//Works
	                             };
			foreach (var e in expressionList)
				VerifyJsonPath(descriptorJobject, e);

			//If we do a search without the below line
			//it seems to contect /_all/object/_search instead of /_search
			newDescriptor.AllTypes().AllIndices();
		}

		private static void VerifyJsonPath(JToken descriptorJobject, IEnumerable<object> strings)
		{
			foreach (var item in strings)
			{
				Assert.IsNotNull(descriptorJobject[item], item.ToString());
				descriptorJobject = descriptorJobject[item];
				if (item.ToString() == "no_match_filter")
					descriptorJobject.ToString().Should().Be("none");
			}
		}

		public string Serialize<T>(T obj) where T : class
		{
			var json = Encoding.UTF8.GetString(_client.Serializer.Serialize(obj));
			return json;
		}

		public T Deserialize<T>(string json) where T : class
		{
			return _client.Serializer.Deserialize<T>(new MemoryStream(Encoding.UTF8.GetBytes(json)));
		}

		const string original =
			@"{
   ""size"":10,
   ""from"":0,
   ""query"":{
      ""filtered"":{
         ""query"":{
            ""query_string"":{
               ""query"":""*"",
               ""default_operator"":""AND"",
               ""fields"":[
                  ""type1._all"",
                  ""type2.title"",
                  ""type3._all"",
                  ""type4._all""
               ]
            }
         },
         ""filter"":{
            ""bool"":{
               ""must"":[

               ],
               ""should"":[
                  {
                     ""indices"":{
                        ""index"":""index1"",
                        ""filter"":{
                           ""terms"":{
                              ""_type"":[
                                 ""type1"",
                                 ""type2"",
                                 ""type3""
                              ]
                           }
                        },
                        ""no_match_filter"":""none""
                     }
                  },
                  {
                     ""indices"":{
                        ""index"":""index2"",
                        ""filter"":{
                           ""terms"":{
                              ""_type"":[
                                 ""type2"",
                                 ""type4""
                              ]
                           }
                        },
                        ""no_match_filter"":""none""
                     }
                  }
               ]
            }
         }
      }
   },
   ""aggs"":{
      ""Databases"":{
         ""terms"":{
            ""field"":""_type"",
            ""size"":5
         }
      },
      ""Index"":{
         ""terms"":{
            ""field"":""_index"",
            ""size"":5
         }
      },
      ""Year"":{
         ""terms"":{
            ""field"":""publicationYear"",
            ""size"":5
         }
      }
   }
}";

	}
}
