// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace Examples.Indices
{
	public class UpdateSettingsPage : ExampleBase
	{
		[U]
		[Description("indices/update-settings.asciidoc:10")]
		public void Line10()
		{
			// tag::8653e76676de5d327201b77512afa3a0[]
			var settingsResponse = client.Indices.UpdateSettings("twitter", u => u
				.IndexSettings(i => i
					.NumberOfReplicas(2)
				)
			);
			// end::8653e76676de5d327201b77512afa3a0[]

			settingsResponse.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""number_of_replicas"" : 2
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json.Remove("index");
				json["index.number_of_replicas"] = 2;
			}));
		}

		[U]
		[Description("indices/update-settings.asciidoc:73")]
		public void Line73()
		{
			// tag::42744a175125df5be0ef77413bf8f608[]
			var settingsResponse = client.Indices.UpdateSettings("twitter", u => u
				.IndexSettings(i => i
					.RefreshInterval(null)
				)
			);
			// end::42744a175125df5be0ef77413bf8f608[]

			settingsResponse.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""refresh_interval"" : null
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json.Remove("index");
				json["index.refresh_interval"] = null;
			}));
		}

		[U]
		[Description("indices/update-settings.asciidoc:97")]
		public void Line97()
		{
			// tag::dfac8d098b50aa0181161bcd17b38ef4[]
			var settingsResponse = client.Indices.UpdateSettings("twitter", u => u
				.IndexSettings(i => i
					.RefreshInterval(Time.MinusOne)
				)
			);
			// end::dfac8d098b50aa0181161bcd17b38ef4[]

			settingsResponse.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""refresh_interval"" : ""-1""
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json.Remove("index");
				json["index.refresh_interval"] = -1;
			}));
		}

		[U]
		[Description("indices/update-settings.asciidoc:114")]
		public void Line114()
		{
			// tag::0be2c28ee65384774b1e479b47dc3d92[]
			var settingsResponse = client.Indices.UpdateSettings("twitter", u => u
				.IndexSettings(i => i
					.RefreshInterval("1s")
				)
			);
			// end::0be2c28ee65384774b1e479b47dc3d92[]

			settingsResponse.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""refresh_interval"" : ""1s""
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json.Remove("index");
				json["index.refresh_interval"] = "1s";
			}));
		}

		[U]
		[Description("indices/update-settings.asciidoc:127")]
		public void Line127()
		{
			// tag::fe5763d32955e8b65eb3048e97b1580c[]
			var mergeResponse = client.Indices.ForceMerge("twitter", f => f
				.MaxNumSegments(5)
			);
			// end::fe5763d32955e8b65eb3048e97b1580c[]

			mergeResponse.MatchesExample(@"POST /twitter/_forcemerge?max_num_segments=5");
		}

		[U]
		[Description("indices/update-settings.asciidoc:145")]
		public void Line145()
		{
			// tag::ba0b4081c98f3387f76b77847c52ee9a[]
			var closeIndexResponse = client.Indices.Close("twitter");

			var settingsResponse = client.Indices.UpdateSettings("twitter", u => u
				.IndexSettings(i => i
					.Analysis(a => a
						.Analyzers(an => an
							.Custom("content", c => c
								.Tokenizer("whitespace")
							)
						)
					)
				)
			);

			var openIndexResponse = client.Indices.Open("twitter");
			// end::ba0b4081c98f3387f76b77847c52ee9a[]

			closeIndexResponse.MatchesExample(@"POST /twitter/_close");

			settingsResponse.MatchesExample(@"PUT /twitter/_settings
			{
			  ""analysis"" : {
			    ""analyzer"":{
			      ""content"":{
			        ""type"":""custom"",
			        ""tokenizer"":""whitespace""
			      }
			    }
			  }
			}");

			openIndexResponse.MatchesExample(@"POST /twitter/_open");
		}
	}
}
