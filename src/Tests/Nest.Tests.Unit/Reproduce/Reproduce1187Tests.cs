using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Text;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce1187Tests : BaseJsonTests
	{
		[Test]
		public void IsClientSideSearchQueryDeserialisable()
		{
			var newDescriptor = Deserialize<SearchDescriptor<dynamic>>(Original);
			var actual = Serialize(newDescriptor);//Should is empty

			var descriptorJobject = JObject.Parse(actual);
		    var jsonPathList = new[]
		                       {
		                           "size", "from", "query.filtered.query.query_string.fields",
		                           "query.filtered.filter.bool.must", //Works
		                           "query.filtered.filter.bool.should[0].indices.index",//Should find contents of "should"
		                           "query.filtered.filter.bool.should[0].indices.indices",
		                           "query.filtered.filter.bool.should[0].indices.filter",
		                           "query.filtered.filter.bool.should[0].indices.no_match_filter",
		                           "aggs.Databases.terms.field", //Works
		                           "aggs.Year.terms.field" //Works
		                       };
		    foreach (var jsonPath in jsonPathList)
                VerifyJsonPath(descriptorJobject, jsonPath);

			//If we do a search without the below line
			//it seems to contect /_all/object/_search instead of /_search
			newDescriptor.AllTypes().AllIndices();
		}

        private static void VerifyJsonPath(JToken json, string path)
        {
            Console.WriteLine(path);
            Assert.IsNotNull(json.SelectToken(path));
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

		const string Original =
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
                        ""indices"":[""index1""],
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
                        ""indices"":[""index2""],
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
