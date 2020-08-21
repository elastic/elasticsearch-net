// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Reproduce {
	public class Utf8JsonTest
	{
		private readonly string _json = @"{
  ""took"" : 6,
  ""timed_out"" : false,
  ""_shards"" : {
    ""total"" : 2,
    ""successful"" : 2,
    ""skipped"" : 0,
    ""failed"" : 0
  },
  ""hits"" : {
    ""total"" : 29,
    ""max_score"" : 1.4046435,
    ""hits"" : [
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""Koch, Harvey and Kuhn2246"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""Koch, Harvey and Kuhn2246"",
        ""_source"" : {
          ""branches"" : [
            ""dev"",
            ""master""
          ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-11T14:14:21.2563863+10:00"",
              ""name"" : ""illum""
            },
            {
              ""added"" : ""2018-11-12T11:25:18.0072809+10:00"",
              ""name"" : ""alias""
            }
          ],
          ""dateString"" : ""2018-04-13T14:09:08.2762851+10:00"",
          ""description"" : ""Est consequatur et qui tenetur nobis libero doloremque natus. Nulla illum inventore sapiente veniam similique. Magni sint fugit maxime possimus.\n\nAut minus ea consequuntur aspernatur. Laboriosam accusamus qui expedita exercitationem repudiandae. Rerum neque et voluptatibus commodi nisi non laborum occaecati.\n\nQuam rerum eum voluptatibus ut asperiores sed perferendis necessitatibus. Porro eius quia perspiciatis recusandae fugit. Reiciendis incidunt in. Consequatur quibusdam totam ut illo dolores eum velit."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-12T06:21:14.0264672+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""Female"",
            ""ipAddress"" : ""11.135.101.160"",
            ""nickname"" : ""Allene_Romaguera72"",
            ""firstName"" : ""Hailee"",
            ""id"" : 1288,
            ""jobTitle"" : ""Lead Solutions Engineer"",
            ""lastName"" : ""Borer"",
            ""location"" : {
              ""lat"" : -50.0,
              ""lon"" : 94.0
            }
          },
          ""location"" : {
            ""lat"" : 72.7967,
            ""lon"" : 37.0724
          },
          ""name"" : ""Koch, Harvey and Kuhn2246"",
          ""numberOfCommits"" : 28,
          ""numberOfContributors"" : 138,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2014-10-11T00:14:04.1369266+10:00"",
              ""lte"" : ""2017-12-16T06:07:35.2406753+10:00""
            },
            ""doubles"" : {
              ""gte"" : 8343.520119277537,
              ""lt"" : 38912.85903416598
            },
            ""floats"" : {
              ""gte"" : 1937.0,
              ""lt"" : 5290.0
            },
            ""ips"" : {
              ""gte"" : ""25.0.204.194"",
              ""lte"" : ""159.85.190.199""
            },
            ""longs"" : {
              ""gte"" : 8256,
              ""lt"" : 34771
            }
          },
          ""requiredBranches"" : 2,
          ""startedOn"" : ""2018-04-13T14:09:08.2762851+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red"",
                ""blue"",
                ""green"",
                ""violet""
              ]
            },
            ""input"" : [
              ""Koch, Harvey and Kuhn""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-11T16:07:59.7402653+10:00"",
              ""name"" : ""aut""
            },
            {
              ""added"" : ""2018-11-12T03:26:22.4273419+10:00"",
              ""name"" : ""dolorum""
            },
            {
              ""added"" : ""2018-11-11T18:57:40.8933864+10:00"",
              ""name"" : ""eos""
            },
            {
              ""added"" : ""2018-11-11T19:55:51.4707587+10:00"",
              ""name"" : ""amet""
            },
            {
              ""added"" : ""2018-11-11T14:56:48.2038887+10:00"",
              ""name"" : ""cupiditate""
            },
            {
              ""added"" : ""2018-11-11T21:03:47.0767479+10:00"",
              ""name"" : ""voluptas""
            },
            {
              ""added"" : ""2018-11-11T15:08:31.1141901+10:00"",
              ""name"" : ""optio""
            },
            {
              ""added"" : ""2018-11-11T14:13:24.1222341+10:00"",
              ""name"" : ""exercitationem""
            },
            {
              ""added"" : ""2018-11-11T22:38:21.2123966+10:00"",
              ""name"" : ""animi""
            },
            {
              ""added"" : ""2018-11-12T07:21:23.7948407+10:00"",
              ""name"" : ""veritatis""
            },
            {
              ""added"" : ""2018-11-12T08:41:16.2111393+10:00"",
              ""name"" : ""neque""
            },
            {
              ""added"" : ""2018-11-12T10:40:36.8565716+10:00"",
              ""name"" : ""voluptatem""
            },
            {
              ""added"" : ""2018-11-12T12:40:24.7243772+10:00"",
              ""name"" : ""placeat""
            },
            {
              ""added"" : ""2018-11-11T14:05:52.9588507+10:00"",
              ""name"" : ""nostrum""
            },
            {
              ""added"" : ""2018-11-12T09:29:48.4295446+10:00"",
              ""name"" : ""doloremque""
            },
            {
              ""added"" : ""2018-11-12T03:01:25.3106336+10:00"",
              ""name"" : ""optio""
            },
            {
              ""added"" : ""2018-11-11T17:46:52.9489099+10:00"",
              ""name"" : ""at""
            },
            {
              ""added"" : ""2018-11-11T22:17:41.3552589+10:00"",
              ""name"" : ""nemo""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Private""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""O'Kon - Haley2295"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""O'Kon - Haley2295"",
        ""_source"" : {
          ""branches"" : [
            ""test"",
            ""master"",
            ""qa"",
            ""release""
          ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-11T18:51:15.2557159+10:00"",
              ""name"" : ""ducimus""
            },
            {
              ""added"" : ""2018-11-12T12:26:23.3648693+10:00"",
              ""name"" : ""vel""
            }
          ],
          ""dateString"" : ""2018-05-28T14:43:52.8144353+10:00"",
          ""description"" : ""Quo et omnis qui dolor. Fuga sunt et minus consequatur officia tempora porro. Reprehenderit voluptas dolore ullam culpa non. Modi aliquam dolor pariatur alias dignissimos ut rerum sit. Ut beatae exercitationem debitis dignissimos qui ratione.\n\nCum qui quia ut et sunt illum. Similique inventore consequuntur. Molestiae reprehenderit veritatis sit molestias unde recusandae minus dolor laboriosam.\n\nVoluptas ab ratione nihil error hic aliquam aut. A magnam harum quae maiores. Omnis amet reprehenderit. Dolorem necessitatibus facilis et est incidunt vero itaque. Numquam minima ducimus."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-12T01:30:47.461266+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""NoneOfYourBeeswax"",
            ""ipAddress"" : ""11.251.125.195"",
            ""nickname"" : ""Nyasia_Ebert0"",
            ""firstName"" : ""Dennis"",
            ""id"" : 1811,
            ""jobTitle"" : ""District Group Executive"",
            ""lastName"" : ""Jerde"",
            ""location"" : {
              ""lat"" : -53.0,
              ""lon"" : -93.0
            }
          },
          ""location"" : {
            ""lat"" : -24.5708,
            ""lon"" : -21.6589
          },
          ""name"" : ""O'Kon - Haley2295"",
          ""numberOfCommits"" : 322,
          ""numberOfContributors"" : 189,
          ""ranges"" : {
            ""dates"" : {
              ""gt"" : ""2010-08-04T21:04:19.014526+10:00"",
              ""lt"" : ""2012-05-11T19:18:48.4451589+10:00""
            },
            ""doubles"" : {
              ""gte"" : 2755.209873562311,
              ""lt"" : 15717.053786883045
            },
            ""floats"" : {
              ""gte"" : 814.0,
              ""lte"" : 7859.0
            },
            ""ips"" : {
              ""gte"" : ""75.221.7.126"",
              ""lte"" : ""96.15.75.19""
            },
            ""longs"" : {
              ""gte"" : 5096,
              ""lt"" : 42137
            }
          },
          ""requiredBranches"" : 4,
          ""startedOn"" : ""2018-05-28T14:43:52.8144353+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red"",
                ""blue"",
                ""green""
              ]
            },
            ""input"" : [
              ""O'Kon - Haley""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-11T22:41:01.4888037+10:00"",
              ""name"" : ""quo""
            },
            {
              ""added"" : ""2018-11-12T00:41:15.4967978+10:00"",
              ""name"" : ""magni""
            },
            {
              ""added"" : ""2018-11-12T05:57:46.6304962+10:00"",
              ""name"" : ""voluptas""
            },
            {
              ""added"" : ""2018-11-12T09:18:37.3627302+10:00"",
              ""name"" : ""ullam""
            },
            {
              ""added"" : ""2018-11-11T14:54:08.5015112+10:00"",
              ""name"" : ""id""
            },
            {
              ""added"" : ""2018-11-12T08:13:54.6529209+10:00"",
              ""name"" : ""accusantium""
            },
            {
              ""added"" : ""2018-11-12T03:50:07.7752169+10:00"",
              ""name"" : ""animi""
            },
            {
              ""added"" : ""2018-11-11T18:45:58.8209754+10:00"",
              ""name"" : ""sed""
            },
            {
              ""added"" : ""2018-11-11T15:25:51.7440789+10:00"",
              ""name"" : ""delectus""
            },
            {
              ""added"" : ""2018-11-11T20:05:00.7288032+10:00"",
              ""name"" : ""eius""
            },
            {
              ""added"" : ""2018-11-11T19:33:10.1224749+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-12T07:16:07.2992423+10:00"",
              ""name"" : ""magni""
            },
            {
              ""added"" : ""2018-11-11T20:55:23.53689+10:00"",
              ""name"" : ""ratione""
            },
            {
              ""added"" : ""2018-11-11T14:37:46.6762326+10:00"",
              ""name"" : ""tempore""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Public""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""Sawayn - Kemmer2448"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""Sawayn - Kemmer2448"",
        ""_source"" : {
          ""branches"" : [
            ""test""
          ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-12T04:21:16.5837721+10:00"",
              ""name"" : ""a""
            }
          ],
          ""dateString"" : ""2018-01-04T13:50:47.6219760+10:00"",
          ""description"" : ""Atque laboriosam enim suscipit beatae. Voluptatem distinctio cum numquam rem ex fugit voluptas quibusdam. Fugit id atque voluptatum reiciendis expedita maiores. Facilis occaecati ullam quo in et velit porro.\n\nQui occaecati dolorem sunt veniam. Voluptatem libero maiores voluptas accusamus molestiae ipsam aut quo vel. Cumque ipsum hic voluptatem dolorem ex deserunt eligendi iste rem. Voluptatem possimus sit possimus aut vero quod. Omnis accusantium quia provident non.\n\nExpedita pariatur numquam sed nostrum alias sit ipsa. Unde id itaque possimus dolorem ut. Est voluptatum distinctio voluptate incidunt nemo. Dolorum praesentium enim."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-12T04:25:59.6875764+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""Female"",
            ""ipAddress"" : ""73.78.179.213"",
            ""nickname"" : ""Rafael.Klocko"",
            ""firstName"" : ""Jan"",
            ""id"" : 1626,
            ""jobTitle"" : ""Dynamic Brand Agent"",
            ""lastName"" : ""Rohan"",
            ""location"" : {
              ""lat"" : 86.0,
              ""lon"" : -18.0
            }
          },
          ""location"" : {
            ""lat"" : -30.2178,
            ""lon"" : -166.0103
          },
          ""name"" : ""Sawayn - Kemmer2448"",
          ""numberOfCommits"" : 857,
          ""numberOfContributors"" : 63,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2010-04-29T22:04:26.3230706+10:00"",
              ""lt"" : ""2019-03-30T12:35:42.268958+10:00""
            },
            ""doubles"" : {
              ""gt"" : 8601.888052712142,
              ""lte"" : 68497.57605864298
            },
            ""floats"" : {
              ""gt"" : 6951.0,
              ""lte"" : 65308.0
            },
            ""ips"" : {
              ""gt"" : ""41.51.5.30"",
              ""lt"" : ""191.9.74.249""
            },
            ""longs"" : {
              ""gt"" : 5966,
              ""lt"" : 39280
            }
          },
          ""requiredBranches"" : 1,
          ""startedOn"" : ""2018-01-04T13:50:47.621976+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red""
              ]
            },
            ""input"" : [
              ""Sawayn - Kemmer""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-12T12:04:05.768732+10:00"",
              ""name"" : ""unde""
            },
            {
              ""added"" : ""2018-11-12T08:05:03.7928929+10:00"",
              ""name"" : ""cumque""
            },
            {
              ""added"" : ""2018-11-11T16:37:13.2734292+10:00"",
              ""name"" : ""commodi""
            },
            {
              ""added"" : ""2018-11-12T08:05:41.9270783+10:00"",
              ""name"" : ""repudiandae""
            },
            {
              ""added"" : ""2018-11-12T13:00:45.1169276+10:00"",
              ""name"" : ""quas""
            },
            {
              ""added"" : ""2018-11-11T20:51:34.4917031+10:00"",
              ""name"" : ""qui""
            },
            {
              ""added"" : ""2018-11-11T20:31:06.2570109+10:00"",
              ""name"" : ""quis""
            },
            {
              ""added"" : ""2018-11-12T03:23:12.0741516+10:00"",
              ""name"" : ""delectus""
            },
            {
              ""added"" : ""2018-11-11T16:47:23.1161052+10:00"",
              ""name"" : ""ipsa""
            },
            {
              ""added"" : ""2018-11-11T17:32:47.1270482+10:00"",
              ""name"" : ""atque""
            },
            {
              ""added"" : ""2018-11-12T07:30:12.8914768+10:00"",
              ""name"" : ""hic""
            },
            {
              ""added"" : ""2018-11-12T05:53:16.084849+10:00"",
              ""name"" : ""aut""
            },
            {
              ""added"" : ""2018-11-11T18:09:51.3982057+10:00"",
              ""name"" : ""accusamus""
            },
            {
              ""added"" : ""2018-11-11T20:36:22.9946659+10:00"",
              ""name"" : ""optio""
            },
            {
              ""added"" : ""2018-11-11T15:00:21.5902392+10:00"",
              ""name"" : ""eaque""
            },
            {
              ""added"" : ""2018-11-11T17:04:44.3819168+10:00"",
              ""name"" : ""non""
            },
            {
              ""added"" : ""2018-11-12T01:16:57.6883729+10:00"",
              ""name"" : ""corrupti""
            },
            {
              ""added"" : ""2018-11-11T13:33:21.2961802+10:00"",
              ""name"" : ""dolor""
            },
            {
              ""added"" : ""2018-11-11T17:48:34.2598864+10:00"",
              ""name"" : ""culpa""
            },
            {
              ""added"" : ""2018-11-11T16:12:00.9197179+10:00"",
              ""name"" : ""nam""
            },
            {
              ""added"" : ""2018-11-11T18:56:48.9587525+10:00"",
              ""name"" : ""quidem""
            },
            {
              ""added"" : ""2018-11-12T12:48:47.1948758+10:00"",
              ""name"" : ""deserunt""
            },
            {
              ""added"" : ""2018-11-12T03:40:54.9173237+10:00"",
              ""name"" : ""quisquam""
            },
            {
              ""added"" : ""2018-11-12T12:15:38.9304349+10:00"",
              ""name"" : ""asperiores""
            },
            {
              ""added"" : ""2018-11-12T02:49:10.4850479+10:00"",
              ""name"" : ""fugit""
            },
            {
              ""added"" : ""2018-11-11T19:20:22.9578731+10:00"",
              ""name"" : ""voluptas""
            },
            {
              ""added"" : ""2018-11-12T05:56:41.8558218+10:00"",
              ""name"" : ""eius""
            },
            {
              ""added"" : ""2018-11-12T06:24:17.6513148+10:00"",
              ""name"" : ""omnis""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Private""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""Becker - Moen2923"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""Becker - Moen2923"",
        ""_source"" : {
          ""branches"" : [
            ""test"",
            ""master"",
            ""release""
          ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-12T04:08:46.3365207+10:00"",
              ""name"" : ""earum""
            }
          ],
          ""dateString"" : ""2018-02-28T03:58:36.7647889+10:00"",
          ""description"" : ""Minus a dolor nostrum quia alias omnis magnam est sunt. In eius vitae quidem dignissimos at ut dolor nesciunt. Quidem quas et unde veritatis. Aut est maiores beatae culpa et. Suscipit fuga et ipsa.\n\nAliquid nam reiciendis recusandae quidem. Reprehenderit labore reprehenderit libero sunt assumenda. Ea qui aspernatur blanditiis minima quod quaerat. Nam cupiditate fuga perferendis ut qui accusantium. Perferendis earum vel impedit nulla consequatur quidem expedita.\n\nQuasi tempore quaerat porro inventore saepe nemo voluptatibus quasi. Labore ipsum modi ullam vel dolorem nobis. Ex et in suscipit. Quos vel est ad veritatis. Omnis rerum odio rerum velit tempore qui magnam rerum."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-12T07:26:53.4022357+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""NoneOfYourBeeswax"",
            ""ipAddress"" : ""156.123.203.79"",
            ""nickname"" : ""Shemar45"",
            ""firstName"" : ""Salma"",
            ""id"" : 1191,
            ""jobTitle"" : ""Global Communications Liaison"",
            ""lastName"" : ""Effertz"",
            ""location"" : {
              ""lat"" : 8.0,
              ""lon"" : -58.0
            }
          },
          ""location"" : {
            ""lat"" : -73.7817,
            ""lon"" : -102.0544
          },
          ""name"" : ""Becker - Moen2923"",
          ""numberOfCommits"" : 125,
          ""numberOfContributors"" : 12,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2011-10-01T23:59:15.178062+10:00"",
              ""lt"" : ""2014-08-28T13:10:33.4721989+10:00""
            },
            ""doubles"" : {
              ""gte"" : 152.65352160514027,
              ""lte"" : 1039.7246576761197
            },
            ""floats"" : {
              ""gte"" : 5834.0,
              ""lt"" : 39200.0
            },
            ""ips"" : {
              ""gt"" : ""12.41.63.39"",
              ""lte"" : ""253.161.53.2""
            },
            ""longs"" : {
              ""gt"" : 2490,
              ""lt"" : 15750
            }
          },
          ""requiredBranches"" : 3,
          ""startedOn"" : ""2018-02-28T03:58:36.7647889+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red""
              ]
            },
            ""input"" : [
              ""Becker - Moen""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-12T03:20:40.5129092+10:00"",
              ""name"" : ""ratione""
            },
            {
              ""added"" : ""2018-11-11T14:46:46.5804149+10:00"",
              ""name"" : ""error""
            },
            {
              ""added"" : ""2018-11-12T11:05:51.8369405+10:00"",
              ""name"" : ""aliquam""
            },
            {
              ""added"" : ""2018-11-11T14:32:33.7544354+10:00"",
              ""name"" : ""nostrum""
            },
            {
              ""added"" : ""2018-11-11T22:20:41.8484124+10:00"",
              ""name"" : ""magnam""
            },
            {
              ""added"" : ""2018-11-11T13:51:10.3700896+10:00"",
              ""name"" : ""quae""
            },
            {
              ""added"" : ""2018-11-11T19:18:27.9688824+10:00"",
              ""name"" : ""fugit""
            },
            {
              ""added"" : ""2018-11-12T04:08:47.8117708+10:00"",
              ""name"" : ""amet""
            },
            {
              ""added"" : ""2018-11-11T17:42:47.4725412+10:00"",
              ""name"" : ""libero""
            },
            {
              ""added"" : ""2018-11-11T22:03:09.363842+10:00"",
              ""name"" : ""necessitatibus""
            },
            {
              ""added"" : ""2018-11-11T20:58:16.1951865+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-11T16:56:58.9598582+10:00"",
              ""name"" : ""incidunt""
            },
            {
              ""added"" : ""2018-11-12T12:39:19.8635991+10:00"",
              ""name"" : ""itaque""
            },
            {
              ""added"" : ""2018-11-12T06:50:19.5310325+10:00"",
              ""name"" : ""numquam""
            },
            {
              ""added"" : ""2018-11-12T05:24:41.7826016+10:00"",
              ""name"" : ""ducimus""
            },
            {
              ""added"" : ""2018-11-12T02:29:08.4924416+10:00"",
              ""name"" : ""velit""
            },
            {
              ""added"" : ""2018-11-12T13:05:01.2673378+10:00"",
              ""name"" : ""et""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Private""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""D'Amore, Renner and Wyman3001"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""D'Amore, Renner and Wyman3001"",
        ""_source"" : {
          ""branches"" : [
            ""master""
          ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-12T12:54:03.2423371+10:00"",
              ""name"" : ""eum""
            }
          ],
          ""dateString"" : ""2018-01-01T21:25:21.6957701+10:00"",
          ""description"" : ""Impedit voluptatem nobis molestiae tenetur sequi hic fuga quia. Vel praesentium ducimus vero maiores. Repellendus laboriosam qui placeat et. Aliquid et sint consequatur ab dolore ut ipsam.\n\nConsequatur iste unde similique molestias. Sint totam dignissimos voluptatem. Id autem enim sed.\n\nEos optio esse. Nam aut et repudiandae aperiam aut officia aut cumque. Consequatur quia dicta explicabo necessitatibus nemo. Vel culpa quam voluptatem corporis placeat."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-11T22:03:28.6103298+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""NoneOfYourBeeswax"",
            ""ipAddress"" : ""10.169.125.57"",
            ""nickname"" : ""Julius.Wiza"",
            ""firstName"" : ""Olen"",
            ""id"" : 1276,
            ""jobTitle"" : ""Global Configuration Coordinator"",
            ""lastName"" : ""Mosciski"",
            ""location"" : {
              ""lat"" : -32.0,
              ""lon"" : -159.0
            }
          },
          ""location"" : {
            ""lat"" : 74.3092,
            ""lon"" : 40.4058
          },
          ""name"" : ""D'Amore, Renner and Wyman3001"",
          ""numberOfCommits"" : 564,
          ""numberOfContributors"" : 144,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2017-12-02T00:53:26.6691733+10:00"",
              ""lte"" : ""2023-06-15T01:01:30.6651401+10:00""
            },
            ""doubles"" : {
              ""gt"" : -98.7060502142208,
              ""lt"" : 694.8333896470301
            },
            ""floats"" : {
              ""gt"" : 8433.0,
              ""lte"" : 65026.0
            },
            ""ips"" : {
              ""gt"" : ""100.223.74.93"",
              ""lt"" : ""237.53.199.161""
            },
            ""longs"" : {
              ""gte"" : 1572,
              ""lt"" : 5064
            }
          },
          ""requiredBranches"" : 1,
          ""startedOn"" : ""2018-01-01T21:25:21.6957701+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red"",
                ""blue"",
                ""green"",
                ""violet""
              ]
            },
            ""input"" : [
              ""D'Amore, Renner and Wyman""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-11T18:12:23.5582129+10:00"",
              ""name"" : ""culpa""
            },
            {
              ""added"" : ""2018-11-12T13:12:42.3201571+10:00"",
              ""name"" : ""sapiente""
            },
            {
              ""added"" : ""2018-11-12T06:38:06.8425749+10:00"",
              ""name"" : ""delectus""
            },
            {
              ""added"" : ""2018-11-11T22:59:03.0940389+10:00"",
              ""name"" : ""vel""
            },
            {
              ""added"" : ""2018-11-12T01:39:40.7793732+10:00"",
              ""name"" : ""reiciendis""
            },
            {
              ""added"" : ""2018-11-11T15:15:24.6576822+10:00"",
              ""name"" : ""iusto""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Private""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""O'Connell, Barrows and Bins3090"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""O'Connell, Barrows and Bins3090"",
        ""_source"" : {
          ""branches"" : [
            ""qa"",
            ""release""
          ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-12T07:48:42.0046754+10:00"",
              ""name"" : ""eius""
            },
            {
              ""added"" : ""2018-11-11T20:33:35.3690922+10:00"",
              ""name"" : ""labore""
            }
          ],
          ""dateString"" : ""2018-10-17T22:41:30.4214010+10:00"",
          ""description"" : ""Sit ad natus laborum in. Rerum qui sed ut a veniam voluptatem ut. Ipsum quis hic laborum vel est eos vel velit fugit. Aliquid non assumenda consequuntur consequatur et non. Et reiciendis vero dolore.\n\nAut consequatur adipisci quibusdam eligendi maxime possimus doloribus. Necessitatibus molestiae tempora. Nulla omnis quia praesentium voluptatem dolores vero. Sit autem voluptate animi ab. Dolor nisi non. Eaque dolorem et aut delectus.\n\nPorro quis temporibus. Repudiandae debitis cumque eos sit. Qui corporis sint nihil placeat. Ea repellendus et repellendus velit rerum tempore in facilis dicta. Iure veritatis libero quo officia expedita dignissimos."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-11T13:51:58.5262202+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""Male"",
            ""ipAddress"" : ""50.218.167.66"",
            ""nickname"" : ""Watson_Bailey"",
            ""firstName"" : ""Dorothy"",
            ""id"" : 1470,
            ""jobTitle"" : ""Product Tactics Orchestrator"",
            ""lastName"" : ""Botsford"",
            ""location"" : {
              ""lat"" : -44.0,
              ""lon"" : -87.0
            }
          },
          ""location"" : {
            ""lat"" : 78.0375,
            ""lon"" : 50.5036
          },
          ""name"" : ""O'Connell, Barrows and Bins3090"",
          ""numberOfCommits"" : 486,
          ""numberOfContributors"" : 95,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2012-08-16T19:23:30.0595454+10:00"",
              ""lte"" : ""2014-09-18T23:17:34.56547+10:00""
            },
            ""doubles"" : {
              ""gt"" : 1484.6995315187144,
              ""lt"" : 5386.907880066663
            },
            ""floats"" : {
              ""gte"" : 1074.0,
              ""lte"" : 8281.0
            },
            ""ips"" : {
              ""gte"" : ""18.23.134.52"",
              ""lt"" : ""205.187.175.185""
            },
            ""longs"" : {
              ""gte"" : 5438,
              ""lt"" : 28225
            }
          },
          ""requiredBranches"" : 2,
          ""startedOn"" : ""2018-10-17T22:41:30.421401+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red"",
                ""blue""
              ]
            },
            ""input"" : [
              ""O'Connell, Barrows and Bins""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-11T20:19:25.0997738+10:00"",
              ""name"" : ""consectetur""
            },
            {
              ""added"" : ""2018-11-11T23:09:52.8278141+10:00"",
              ""name"" : ""numquam""
            },
            {
              ""added"" : ""2018-11-12T08:47:45.7886319+10:00"",
              ""name"" : ""consequuntur""
            },
            {
              ""added"" : ""2018-11-11T18:17:35.3985412+10:00"",
              ""name"" : ""error""
            },
            {
              ""added"" : ""2018-11-11T17:06:31.4617858+10:00"",
              ""name"" : ""eligendi""
            },
            {
              ""added"" : ""2018-11-12T06:42:36.1337857+10:00"",
              ""name"" : ""repudiandae""
            },
            {
              ""added"" : ""2018-11-12T10:45:59.1308339+10:00"",
              ""name"" : ""maiores""
            },
            {
              ""added"" : ""2018-11-12T01:29:23.8320986+10:00"",
              ""name"" : ""velit""
            },
            {
              ""added"" : ""2018-11-12T10:39:09.0162328+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-12T12:35:56.0236593+10:00"",
              ""name"" : ""nostrum""
            },
            {
              ""added"" : ""2018-11-12T12:02:01.7596632+10:00"",
              ""name"" : ""cum""
            },
            {
              ""added"" : ""2018-11-11T21:32:05.6550307+10:00"",
              ""name"" : ""rem""
            },
            {
              ""added"" : ""2018-11-12T08:10:28.6804091+10:00"",
              ""name"" : ""est""
            },
            {
              ""added"" : ""2018-11-12T09:13:33.1062792+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-12T06:08:19.0296092+10:00"",
              ""name"" : ""nam""
            },
            {
              ""added"" : ""2018-11-12T11:36:45.6429118+10:00"",
              ""name"" : ""iste""
            },
            {
              ""added"" : ""2018-11-12T00:36:08.9657566+10:00"",
              ""name"" : ""voluptatibus""
            },
            {
              ""added"" : ""2018-11-11T19:35:04.5870685+10:00"",
              ""name"" : ""voluptates""
            },
            {
              ""added"" : ""2018-11-12T04:59:28.9767347+10:00"",
              ""name"" : ""vel""
            },
            {
              ""added"" : ""2018-11-11T18:01:55.1510185+10:00"",
              ""name"" : ""non""
            },
            {
              ""added"" : ""2018-11-12T00:04:16.1196985+10:00"",
              ""name"" : ""repellendus""
            },
            {
              ""added"" : ""2018-11-12T06:44:32.8337112+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-12T03:56:37.1224025+10:00"",
              ""name"" : ""illo""
            },
            {
              ""added"" : ""2018-11-11T23:37:04.7200434+10:00"",
              ""name"" : ""ut""
            },
            {
              ""added"" : ""2018-11-12T03:15:21.178209+10:00"",
              ""name"" : ""sapiente""
            },
            {
              ""added"" : ""2018-11-11T17:06:27.5150431+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-12T11:30:38.0170496+10:00"",
              ""name"" : ""adipisci""
            },
            {
              ""added"" : ""2018-11-12T10:28:29.2887774+10:00"",
              ""name"" : ""voluptates""
            },
            {
              ""added"" : ""2018-11-12T00:20:56.970976+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-11T15:50:58.8585993+10:00"",
              ""name"" : ""dolorem""
            },
            {
              ""added"" : ""2018-11-12T04:16:12.5592869+10:00"",
              ""name"" : ""qui""
            },
            {
              ""added"" : ""2018-11-12T04:16:39.3979325+10:00"",
              ""name"" : ""dolores""
            },
            {
              ""added"" : ""2018-11-11T17:28:33.958988+10:00"",
              ""name"" : ""nostrum""
            },
            {
              ""added"" : ""2018-11-12T02:46:36.3377698+10:00"",
              ""name"" : ""corporis""
            },
            {
              ""added"" : ""2018-11-12T02:43:34.0408363+10:00"",
              ""name"" : ""quia""
            },
            {
              ""added"" : ""2018-11-11T16:16:25.3543193+10:00"",
              ""name"" : ""est""
            },
            {
              ""added"" : ""2018-11-12T02:00:24.0093936+10:00"",
              ""name"" : ""officia""
            },
            {
              ""added"" : ""2018-11-12T10:42:41.6125219+10:00"",
              ""name"" : ""aliquam""
            },
            {
              ""added"" : ""2018-11-12T08:58:56.6618997+10:00"",
              ""name"" : ""eos""
            },
            {
              ""added"" : ""2018-11-12T04:32:59.2251774+10:00"",
              ""name"" : ""laborum""
            },
            {
              ""added"" : ""2018-11-12T08:12:40.2071253+10:00"",
              ""name"" : ""doloribus""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Public""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""Roob Inc3192"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""Roob Inc3192"",
        ""_source"" : {
          ""branches"" : [
            ""dev"",
            ""test"",
            ""qa""
          ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-11T13:48:51.1496214+10:00"",
              ""name"" : ""ex""
            },
            {
              ""added"" : ""2018-11-12T11:57:03.9652039+10:00"",
              ""name"" : ""voluptatem""
            }
          ],
          ""dateString"" : ""2018-10-25T01:25:14.5471056+10:00"",
          ""description"" : ""Libero similique quas quia excepturi minima unde aut aut labore. Blanditiis necessitatibus ut molestias perspiciatis dolores. Culpa aspernatur non delectus voluptas.\n\nNulla veniam quo omnis quo qui ut officia veritatis ab. Odio incidunt deserunt ad ut sunt rerum. Laborum ullam quia nisi porro omnis deleniti doloremque. Voluptatibus voluptatibus nihil. Quod magnam libero asperiores quo odit ipsa nulla. Accusamus eum numquam delectus odit voluptatem.\n\nNatus ea quia explicabo voluptatem. Libero earum sed alias ea commodi repudiandae est. Nisi illum cumque. Et minima dolorem laudantium animi eveniet dolorem et nisi consequuntur. Libero numquam enim maxime nihil fuga eum harum officia."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-11T23:18:40.1978099+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""NoneOfYourBeeswax"",
            ""ipAddress"" : ""121.194.45.123"",
            ""nickname"" : ""Enid.Casper"",
            ""firstName"" : ""Rick"",
            ""id"" : 1622,
            ""jobTitle"" : ""Forward Integration Representative"",
            ""lastName"" : ""Friesen"",
            ""location"" : {
              ""lat"" : 34.0,
              ""lon"" : 0.0
            }
          },
          ""location"" : {
            ""lat"" : -8.8421,
            ""lon"" : 164.5819
          },
          ""name"" : ""Roob Inc3192"",
          ""numberOfCommits"" : 667,
          ""numberOfContributors"" : 33,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2008-06-03T08:20:03.7001737+10:00"",
              ""lt"" : ""2009-11-07T22:47:50.5643144+10:00""
            },
            ""doubles"" : {
              ""gte"" : 4272.492184788218,
              ""lt"" : 7356.135424819387
            },
            ""floats"" : {
              ""gte"" : 7422.0,
              ""lte"" : 66212.0
            },
            ""ips"" : {
              ""gt"" : ""16.62.119.195"",
              ""lt"" : ""35.169.18.212""
            },
            ""longs"" : {
              ""gt"" : 5705,
              ""lt"" : 7104
            }
          },
          ""requiredBranches"" : 3,
          ""startedOn"" : ""2018-10-25T01:25:14.5471056+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red"",
                ""blue"",
                ""green"",
                ""violet""
              ]
            },
            ""input"" : [
              ""Roob Inc""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-12T09:08:06.7468742+10:00"",
              ""name"" : ""eos""
            },
            {
              ""added"" : ""2018-11-12T09:58:05.9545938+10:00"",
              ""name"" : ""incidunt""
            },
            {
              ""added"" : ""2018-11-12T11:17:14.2874473+10:00"",
              ""name"" : ""occaecati""
            },
            {
              ""added"" : ""2018-11-12T03:48:21.6868376+10:00"",
              ""name"" : ""id""
            },
            {
              ""added"" : ""2018-11-11T18:48:01.4973656+10:00"",
              ""name"" : ""deserunt""
            },
            {
              ""added"" : ""2018-11-11T13:35:20.8074557+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-11T19:08:51.7631037+10:00"",
              ""name"" : ""repudiandae""
            },
            {
              ""added"" : ""2018-11-12T06:08:41.4911797+10:00"",
              ""name"" : ""magni""
            },
            {
              ""added"" : ""2018-11-12T01:45:06.9592949+10:00"",
              ""name"" : ""voluptatem""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Private""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""Bernhard Group3497"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""Bernhard Group3497"",
        ""_source"" : {
          ""branches"" : [ ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-12T09:30:27.1000265+10:00"",
              ""name"" : ""quos""
            },
            {
              ""added"" : ""2018-11-12T08:25:10.9006726+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-11T22:45:48.3765682+10:00"",
              ""name"" : ""sed""
            }
          ],
          ""dateString"" : ""2018-11-11T11:42:40.4228670+10:00"",
          ""description"" : ""Sed non quos possimus minima ab tenetur non veniam officia. Eum minus rem pariatur voluptatem neque. Et perferendis consectetur asperiores et.\n\nVoluptatem iusto optio voluptas repellat debitis quae. Quibusdam velit numquam sed inventore voluptas delectus dignissimos. Asperiores ratione animi nobis.\n\nEnim et facilis dignissimos blanditiis illo facere non ratione. Dolores magni et quaerat id veniam. Quis facere recusandae enim harum repellendus odit dolores."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-12T13:27:13.9306795+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""Male"",
            ""ipAddress"" : ""9.249.196.46"",
            ""nickname"" : ""Isabel_Shields"",
            ""firstName"" : ""Angeline"",
            ""id"" : 1498,
            ""jobTitle"" : ""Global Assurance Supervisor"",
            ""lastName"" : ""Bechtelar"",
            ""location"" : {
              ""lat"" : -78.0,
              ""lon"" : -176.0
            }
          },
          ""location"" : {
            ""lat"" : -5.4697,
            ""lon"" : 37.0261
          },
          ""name"" : ""Bernhard Group3497"",
          ""numberOfCommits"" : 67,
          ""numberOfContributors"" : 92,
          ""ranges"" : {
            ""dates"" : {
              ""gt"" : ""2012-02-17T12:53:28.5299077+10:00"",
              ""lte"" : ""2013-08-27T00:56:37.9174341+10:00""
            },
            ""doubles"" : {
              ""gte"" : 2324.5425904023195,
              ""lte"" : 18240.98922924576
            },
            ""floats"" : {
              ""gte"" : 7292.0,
              ""lt"" : 31505.0
            },
            ""ips"" : {
              ""gte"" : ""63.76.149.237"",
              ""lt"" : ""198.56.64.46""
            },
            ""longs"" : {
              ""gt"" : 7151,
              ""lte"" : 35525
            }
          },
          ""requiredBranches"" : 0,
          ""startedOn"" : ""2018-11-11T11:42:40.422867+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red""
              ]
            },
            ""input"" : [
              ""Bernhard Group""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-11T15:51:09.6916628+10:00"",
              ""name"" : ""eum""
            },
            {
              ""added"" : ""2018-11-11T21:16:43.0940832+10:00"",
              ""name"" : ""deserunt""
            },
            {
              ""added"" : ""2018-11-11T18:41:05.1070768+10:00"",
              ""name"" : ""ipsam""
            },
            {
              ""added"" : ""2018-11-12T07:32:08.6876711+10:00"",
              ""name"" : ""deleniti""
            },
            {
              ""added"" : ""2018-11-12T04:19:39.0993816+10:00"",
              ""name"" : ""qui""
            },
            {
              ""added"" : ""2018-11-11T22:50:44.2686624+10:00"",
              ""name"" : ""est""
            },
            {
              ""added"" : ""2018-11-11T14:26:05.8619341+10:00"",
              ""name"" : ""quia""
            },
            {
              ""added"" : ""2018-11-12T06:20:11.8746557+10:00"",
              ""name"" : ""omnis""
            },
            {
              ""added"" : ""2018-11-12T09:29:29.5993829+10:00"",
              ""name"" : ""voluptatem""
            },
            {
              ""added"" : ""2018-11-11T21:07:35.8076608+10:00"",
              ""name"" : ""maxime""
            },
            {
              ""added"" : ""2018-11-11T13:53:45.6284251+10:00"",
              ""name"" : ""voluptatem""
            },
            {
              ""added"" : ""2018-11-12T01:16:51.4414254+10:00"",
              ""name"" : ""quia""
            },
            {
              ""added"" : ""2018-11-12T06:49:55.7761746+10:00"",
              ""name"" : ""consequatur""
            },
            {
              ""added"" : ""2018-11-12T02:38:39.9623508+10:00"",
              ""name"" : ""non""
            },
            {
              ""added"" : ""2018-11-11T19:46:22.5631891+10:00"",
              ""name"" : ""ut""
            },
            {
              ""added"" : ""2018-11-11T15:21:36.3537606+10:00"",
              ""name"" : ""laboriosam""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Private""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""Fisher, Bradtke and Funk3545"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""Fisher, Bradtke and Funk3545"",
        ""_source"" : {
          ""branches"" : [ ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-12T11:16:30.3284711+10:00"",
              ""name"" : ""vel""
            },
            {
              ""added"" : ""2018-11-11T15:44:47.5016838+10:00"",
              ""name"" : ""asperiores""
            },
            {
              ""added"" : ""2018-11-11T16:58:48.8117857+10:00"",
              ""name"" : ""eum""
            }
          ],
          ""dateString"" : ""2018-05-27T18:46:05.8913886+10:00"",
          ""description"" : ""Accusamus hic fugiat quidem cum voluptas. Optio vero facere nisi magni nisi. Nobis delectus nulla excepturi assumenda ipsum amet.\n\nOptio vel illo ut. Non consequatur et ea id occaecati est fugit. Quis saepe aperiam minima numquam aut nesciunt. Ut voluptates excepturi amet quia aut et ut fugiat exercitationem.\n\nEos dolorem dolorum repellendus nobis atque pariatur rerum. Cupiditate dolorem aut similique autem iste et minus saepe aut. Dicta quibusdam nulla ratione accusantium. Consequatur velit et reprehenderit assumenda modi incidunt."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-12T06:59:39.0580829+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""Male"",
            ""ipAddress"" : ""47.144.126.139"",
            ""nickname"" : ""Georgianna.Hermiston"",
            ""firstName"" : ""Dahlia"",
            ""id"" : 1061,
            ""jobTitle"" : ""Legacy Response Representative"",
            ""lastName"" : ""Haley"",
            ""location"" : {
              ""lat"" : 85.0,
              ""lon"" : 162.0
            }
          },
          ""location"" : {
            ""lat"" : 21.2854,
            ""lon"" : -172.009
          },
          ""name"" : ""Fisher, Bradtke and Funk3545"",
          ""numberOfCommits"" : 287,
          ""numberOfContributors"" : 167,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2005-09-13T11:45:08.7694332+10:00"",
              ""lte"" : ""2006-05-18T02:51:15.1194328+10:00""
            },
            ""doubles"" : {
              ""gt"" : 7635.938583901123,
              ""lt"" : 10967.909055559006
            },
            ""floats"" : {
              ""gt"" : 2830.0,
              ""lte"" : 27227.0
            },
            ""ips"" : {
              ""gt"" : ""47.100.170.211"",
              ""lte"" : ""228.165.157.176""
            },
            ""longs"" : {
              ""gt"" : 379,
              ""lte"" : 3746
            }
          },
          ""requiredBranches"" : 0,
          ""startedOn"" : ""2018-05-27T18:46:05.8913886+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red""
              ]
            },
            ""input"" : [
              ""Fisher, Bradtke and Funk""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-11T15:44:15.7165497+10:00"",
              ""name"" : ""saepe""
            },
            {
              ""added"" : ""2018-11-11T15:35:31.7636949+10:00"",
              ""name"" : ""doloribus""
            },
            {
              ""added"" : ""2018-11-12T02:25:03.3824695+10:00"",
              ""name"" : ""assumenda""
            },
            {
              ""added"" : ""2018-11-12T02:35:55.2631083+10:00"",
              ""name"" : ""sint""
            },
            {
              ""added"" : ""2018-11-12T10:45:53.2083997+10:00"",
              ""name"" : ""earum""
            },
            {
              ""added"" : ""2018-11-12T08:46:58.8331785+10:00"",
              ""name"" : ""doloremque""
            },
            {
              ""added"" : ""2018-11-12T02:23:07.5189496+10:00"",
              ""name"" : ""ut""
            },
            {
              ""added"" : ""2018-11-11T18:05:54.5205483+10:00"",
              ""name"" : ""vel""
            },
            {
              ""added"" : ""2018-11-11T17:19:45.0096584+10:00"",
              ""name"" : ""commodi""
            },
            {
              ""added"" : ""2018-11-12T08:23:37.2679355+10:00"",
              ""name"" : ""non""
            },
            {
              ""added"" : ""2018-11-12T02:57:49.6926883+10:00"",
              ""name"" : ""dignissimos""
            },
            {
              ""added"" : ""2018-11-12T11:43:01.1969043+10:00"",
              ""name"" : ""velit""
            },
            {
              ""added"" : ""2018-11-12T01:12:25.9840885+10:00"",
              ""name"" : ""sed""
            },
            {
              ""added"" : ""2018-11-12T05:07:54.5258536+10:00"",
              ""name"" : ""accusamus""
            },
            {
              ""added"" : ""2018-11-12T09:52:08.9813819+10:00"",
              ""name"" : ""sit""
            },
            {
              ""added"" : ""2018-11-12T07:52:15.6827632+10:00"",
              ""name"" : ""omnis""
            },
            {
              ""added"" : ""2018-11-11T19:09:15.6361907+10:00"",
              ""name"" : ""suscipit""
            },
            {
              ""added"" : ""2018-11-12T10:14:39.3784208+10:00"",
              ""name"" : ""explicabo""
            },
            {
              ""added"" : ""2018-11-12T08:33:49.5870181+10:00"",
              ""name"" : ""vel""
            },
            {
              ""added"" : ""2018-11-11T22:16:22.3882747+10:00"",
              ""name"" : ""sit""
            },
            {
              ""added"" : ""2018-11-12T06:07:14.7378879+10:00"",
              ""name"" : ""doloremque""
            },
            {
              ""added"" : ""2018-11-11T15:51:18.7568014+10:00"",
              ""name"" : ""laudantium""
            },
            {
              ""added"" : ""2018-11-12T07:17:36.2226284+10:00"",
              ""name"" : ""eum""
            },
            {
              ""added"" : ""2018-11-12T11:55:37.0718544+10:00"",
              ""name"" : ""et""
            },
            {
              ""added"" : ""2018-11-12T11:24:33.855854+10:00"",
              ""name"" : ""sed""
            },
            {
              ""added"" : ""2018-11-11T22:06:08.9323329+10:00"",
              ""name"" : ""nesciunt""
            },
            {
              ""added"" : ""2018-11-12T02:02:00.8946164+10:00"",
              ""name"" : ""voluptate""
            },
            {
              ""added"" : ""2018-11-12T02:38:12.5818588+10:00"",
              ""name"" : ""eligendi""
            },
            {
              ""added"" : ""2018-11-11T15:37:19.4602544+10:00"",
              ""name"" : ""dolores""
            },
            {
              ""added"" : ""2018-11-12T07:24:26.6064488+10:00"",
              ""name"" : ""autem""
            },
            {
              ""added"" : ""2018-11-12T09:33:39.5067918+10:00"",
              ""name"" : ""deserunt""
            },
            {
              ""added"" : ""2018-11-11T18:34:22.1715586+10:00"",
              ""name"" : ""ad""
            },
            {
              ""added"" : ""2018-11-11T18:25:45.3122874+10:00"",
              ""name"" : ""error""
            },
            {
              ""added"" : ""2018-11-12T02:16:50.3836582+10:00"",
              ""name"" : ""eos""
            },
            {
              ""added"" : ""2018-11-11T21:18:16.0069158+10:00"",
              ""name"" : ""consequatur""
            },
            {
              ""added"" : ""2018-11-12T09:47:06.6893275+10:00"",
              ""name"" : ""asperiores""
            },
            {
              ""added"" : ""2018-11-11T23:40:46.3418856+10:00"",
              ""name"" : ""expedita""
            },
            {
              ""added"" : ""2018-11-12T10:16:58.7736448+10:00"",
              ""name"" : ""sit""
            },
            {
              ""added"" : ""2018-11-12T01:18:23.5159239+10:00"",
              ""name"" : ""reiciendis""
            },
            {
              ""added"" : ""2018-11-11T16:54:05.8259209+10:00"",
              ""name"" : ""dolore""
            },
            {
              ""added"" : ""2018-11-11T18:15:40.9961989+10:00"",
              ""name"" : ""eos""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Public""
        }
      },
      {
        ""_index"" : ""project"",
        ""_type"" : ""doc"",
        ""_id"" : ""Cormier - Collins3602"",
        ""_score"" : 1.4046435,
        ""_routing"" : ""Cormier - Collins3602"",
        ""_source"" : {
          ""branches"" : [ ],
          ""curatedTags"" : [
            {
              ""added"" : ""2018-11-12T05:55:56.8515053+10:00"",
              ""name"" : ""sed""
            }
          ],
          ""dateString"" : ""2017-12-10T23:22:46.5738192+10:00"",
          ""description"" : ""Aspernatur voluptas rerum. Aut id repudiandae velit. Pariatur eum in explicabo sint impedit consequuntur placeat. Consequatur incidunt sequi ipsum adipisci cum ad nisi mollitia. Eos aliquid exercitationem consectetur odit voluptate sunt molestiae iure sit. Et repellendus nisi perferendis laborum quisquam sunt ipsa distinctio.\n\nSimilique sed alias assumenda sapiente molestiae est sapiente. Et aut doloremque omnis aliquid iste quidem rem nobis molestiae. Id minus alias quas dignissimos consequatur nesciunt culpa a. Corporis recusandae veritatis quia dolor sunt consectetur suscipit. Neque minima molestiae veritatis dolore at. Autem distinctio inventore vitae aut ea incidunt aperiam alias.\n\nEt quia sequi assumenda. Consequatur magnam quia. Ut et sunt. Ducimus dolores labore ut quas enim. Facere est eligendi sit dolores occaecati officiis animi. Nihil accusantium quia earum rerum est aut nostrum reprehenderit non."",
          ""join"" : ""project"",
          ""lastActivity"" : ""2018-11-11T18:54:59.7661002+10:00"",
          ""leadDeveloper"" : {
            ""gender"" : ""Female"",
            ""ipAddress"" : ""185.214.128.154"",
            ""nickname"" : ""Kamron.Hayes3"",
            ""firstName"" : ""Eleanora"",
            ""id"" : 1973,
            ""jobTitle"" : ""Forward Marketing Coordinator"",
            ""lastName"" : ""Cronin"",
            ""location"" : {
              ""lat"" : -28.0,
              ""lon"" : -74.0
            }
          },
          ""location"" : {
            ""lat"" : -71.3855,
            ""lon"" : -52.9057
          },
          ""name"" : ""Cormier - Collins3602"",
          ""numberOfCommits"" : 472,
          ""numberOfContributors"" : 140,
          ""ranges"" : {
            ""dates"" : {
              ""gte"" : ""2016-01-15T14:35:40.9757694+10:00"",
              ""lt"" : ""2016-04-05T04:16:54.4443583+10:00""
            },
            ""doubles"" : {
              ""gt"" : 5049.810759532644,
              ""lte"" : 46588.1589033916
            },
            ""floats"" : {
              ""gt"" : 8997.0,
              ""lte"" : 12941.0
            },
            ""ips"" : {
              ""gte"" : ""188.168.171.129"",
              ""lt"" : ""226.106.153.9""
            },
            ""longs"" : {
              ""gte"" : 9771,
              ""lte"" : 66727
            }
          },
          ""requiredBranches"" : 0,
          ""startedOn"" : ""2017-12-10T23:22:46.5738192+10:00"",
          ""state"" : ""Stable"",
          ""suggest"" : {
            ""contexts"" : {
              ""color"" : [
                ""red"",
                ""blue"",
                ""green""
              ]
            },
            ""input"" : [
              ""Cormier - Collins""
            ]
          },
          ""tags"" : [
            {
              ""added"" : ""2018-11-11T16:54:56.9013365+10:00"",
              ""name"" : ""optio""
            },
            {
              ""added"" : ""2018-11-12T13:29:31.228019+10:00"",
              ""name"" : ""explicabo""
            },
            {
              ""added"" : ""2018-11-11T17:37:38.3405773+10:00"",
              ""name"" : ""maiores""
            },
            {
              ""added"" : ""2018-11-12T00:06:58.4352524+10:00"",
              ""name"" : ""exercitationem""
            },
            {
              ""added"" : ""2018-11-11T13:51:22.9038576+10:00"",
              ""name"" : ""voluptatum""
            },
            {
              ""added"" : ""2018-11-11T15:04:56.8651984+10:00"",
              ""name"" : ""nam""
            },
            {
              ""added"" : ""2018-11-11T18:33:17.2942085+10:00"",
              ""name"" : ""reprehenderit""
            },
            {
              ""added"" : ""2018-11-12T09:13:33.7263274+10:00"",
              ""name"" : ""aliquid""
            },
            {
              ""added"" : ""2018-11-11T21:45:22.2487325+10:00"",
              ""name"" : ""autem""
            },
            {
              ""added"" : ""2018-11-12T13:22:33.2910587+10:00"",
              ""name"" : ""delectus""
            },
            {
              ""added"" : ""2018-11-11T20:03:58.6880151+10:00"",
              ""name"" : ""qui""
            },
            {
              ""added"" : ""2018-11-12T11:17:19.2815716+10:00"",
              ""name"" : ""aperiam""
            },
            {
              ""added"" : ""2018-11-11T20:03:44.2234098+10:00"",
              ""name"" : ""maxime""
            }
          ],
          ""type"" : ""project"",
          ""visibility"" : ""Private""
        }
      }
    ]
  }
}
";

		[U]
		public void Deserialize()
		{
			var client = FixedResponseClient.Create(_json);

			var searchResponse = client.Search<Project>();

			searchResponse.Should().NotBeNull();
			searchResponse.Documents.Should().NotBeNull();
			searchResponse.Documents.Count.Should().Be(10);
		}

		[U]
		public void SerializeMatchQuery()
		{
			var client = FixedResponseClient.Create(new {}, modifySettings: c => c.DisableDirectStreaming());

			var searchRequest = new SearchRequest<Project>
			{
				Query = new MatchQuery
				{
					Field = Infer.Field<Project>(p => p.Name),
					Query = "Bar"
				}
			};

			var searchResponse = client.Search<Project>(searchRequest);

			searchResponse.ApiCall.RequestBodyInBytes.Should().NotBeNull();
			var json = Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes);

			var queryJson = @"{""query"":{""match"":{""name"":{""query"":""Bar""}}}}";
			json.Should().NotBeNullOrEmpty().And.Be(queryJson);

			searchResponse = client.Search<Project>(s => s
				.Query(q => q
					.Match(m => m
						.Field(f => f.Name)
						.Query("Bar")
					)
				)
			);

			searchResponse.ApiCall.RequestBodyInBytes.Should().NotBeNull();
			json = Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes);

			json.Should().NotBeNullOrEmpty().And.Be(queryJson);
		}

		[U]
		public void SerializeBoolQuery()
		{
			var client = FixedResponseClient.Create(new {}, modifySettings: c => c.DisableDirectStreaming());

			var searchRequest = new SearchRequest<Project>
			{
				Query = new BoolQuery
				{
					Must = new QueryContainer[]
					{
						new MatchQuery
						{
							Field = Infer.Field<Project>(p => p.Name),
							Query = "Bar"
						},
						new MatchQuery
						{
							Field = Infer.Field<Project>(p => p.Description),
							Query = "Baz"
						}
					}
				}
			};

			var searchResponse = client.Search<Project>(searchRequest);

			searchResponse.ApiCall.RequestBodyInBytes.Should().NotBeNull();
			var json = Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes);

			var queryJson = @"{""query"":{""bool"":{""must"":[{""match"":{""name"":{""query"":""Bar""}}},{""match"":{""description"":{""query"":""Baz""}}}]}}}";

			json.Should().NotBeNullOrEmpty().And.Be(queryJson);

			searchResponse = client.Search<Project>(s => s
				.Query(q => q
					.Bool(b => b
						.Must(qq => qq
							.Match(m => m
								.Field(f => f.Name)
								.Query("Bar")
							), qq => qq
							.Match(m => m
								.Field(f => f.Description)
								.Query("Baz")
							)
						)
					)
				)
			);

			searchResponse.ApiCall.RequestBodyInBytes.Should().NotBeNull();
			json = Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes);

			json.Should().NotBeNullOrEmpty().And.Be(queryJson);

			searchResponse = client.Search<Project>(s => s
				.Query(q => q
					.Match(m => m
						.Field(f => f.Name)
						.Query("Bar")
					) && q
					.Match(m => m
						.Field(f => f.Description)
						.Query("Baz")
					)
				)
			);

			searchResponse.ApiCall.RequestBodyInBytes.Should().NotBeNull();
			json = Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes);

			json.Should().NotBeNullOrEmpty().And.Be(queryJson);
		}
	}
}
