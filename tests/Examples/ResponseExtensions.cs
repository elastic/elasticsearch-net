// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Core.Extensions;
using Xunit;

namespace Examples
{
	public static class ResponseExtensions
	{
		private static readonly JsonSerializer Serializer = new JsonSerializer();

		public static void MatchesExample(this IResponse response, string content, Action<Example, JObject> clientChanges) =>
			response.MatchesExample(content, e =>
			{
				e.ApplyBodyChanges(b=> clientChanges(e, b));
			});

		/// <summary>
		/// Asserts that the client generated request matches the example from the docs
		/// </summary>
		public static void MatchesExample(this IResponse response, string content, Action<Example> clientChanges = null)
		{
			var example = Example.Create(content);

			// a specific example might use notation that is not supported by the client, because it only
			// supports the long form. Allow a function to be passed to make modifications to suit.
			clientChanges?.Invoke(example);

			// apply global changes after local ones
			example = Example.ApplyGlobalChanges(example);

			response.ApiCall.HttpMethod.Should().Be(example.Method);

			// the client encodes characters such as commas, so decode to compare
			var decodedAbsolutePath = HttpUtility.UrlDecode(response.ApiCall.Uri.AbsolutePath);

			var expectedUri = example.Uri.Uri;
			var expectedPath = HttpUtility.UrlDecode(expectedUri.AbsolutePath.Length > 1
				? expectedUri.AbsolutePath.TrimEnd('/')
				: expectedUri.AbsolutePath);

			// A lot of the examples are not constrained to an index which is not necessarily what users typically do.
			// in NEST you have to be explicit and call AllIndices() which explicitly puts _all on the path
			// So if we expect `/_search` but `/_all/_search` is passed we feel they are equivalent
			if (expectedPath != "/_search" || decodedAbsolutePath != "/_all/_search")
				decodedAbsolutePath.Should().Be(expectedPath);

			// check expected query string params. Rather that _all_ keys match,
			// only check that the ones in reference doc example are present, because
			// the client may append more key/values such as "typed_keys"
			// Because the tests remove keys with string replace the following bad query string can appear
			example.Uri.Query = example.Uri.Query.Replace("?&", "?").Replace("&&", "&");
			var expectedQueryParams = HttpUtility.ParseQueryString(example.Uri.Query);
			var actualQueryParams = HttpUtility.ParseQueryString(response.ApiCall.Uri.Query);
			if (expectedQueryParams.HasKeys())
			{
				foreach (var key in expectedQueryParams.AllKeys)
				{
					if (string.IsNullOrWhiteSpace(key)) continue;

					actualQueryParams.AllKeys.Should().Contain(key);
					var expectedValue = expectedQueryParams.Get(key);
					var actualValue = actualQueryParams.Get(key);
					// The client always sends x=true while the docs document just sending ?x
					if (string.IsNullOrEmpty(expectedValue) && actualValue == "true")
						continue;
					actualQueryParams.Get(key).Should().Be(expectedValue);
				}
			}

			if (example.Body != null)
			{
				var expected = ParseJObjects(example.Body);
				var actual = ParseJObjects(Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes));

				expected.Count.Should().Be(actual.Count);

				foreach (var (e, a) in expected.Zip(actual))
				{
					var matches = JToken.DeepEquals(e, a);
					if (!matches)
					{
						e.DeepSort();
						a.DeepSort();

						var sortedExpected = string.Join(Environment.NewLine,expected.Select(x => x.ToString()));
						var sortedActual = string.Join(Environment.NewLine,actual.Select(x => x.ToString()));
						var diff = sortedExpected.Diff(sortedActual);

						if (!string.IsNullOrWhiteSpace(diff))
							Assert.True(false, $"body diff: {diff}");
					}
				}
			}
		}

		/// <summary>
		/// Parses a collection of JObjects from the JSON input. Provides support
		/// for both regular JSON and newline delimited JSON
		/// </summary>
		public static List<JObject> ParseJObjects(string json)
		{
			var jObjects = new List<JObject>();
			using (var stringReader = new StringReader(json))
			using (var jsonReader = new JsonTextReader(stringReader) { SupportMultipleContent = true })
				while (jsonReader.Read())
					jObjects.Add(Serializer.Deserialize<JObject>(jsonReader));
			return jObjects;
		}
	}
}
