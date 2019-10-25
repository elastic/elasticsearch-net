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

		/// <summary>
		/// Asserts that the client generated request matches the example from the docs
		/// </summary>
		public static void MatchesExample(this IResponse response, string content, Func<Example, Example> clientChanges = null)
		{
			var example = Example.Create(content);

			// a specific example might use notation that is not supported by the client, because it only
			// supports the long form. Allow a function to be passed to make modifications to suit.
			if (clientChanges != null)
				example = clientChanges(example);

			// apply global changes after local ones
			example = Example.ApplyGlobalChanges(example);

			response.ApiCall.HttpMethod.Should().Be(example.Method);

			// the client encodes characters such as commas, so decode to compare
			var decodedAbsolutePath = HttpUtility.UrlDecode(response.ApiCall.Uri.AbsolutePath);

			decodedAbsolutePath.Should().Be(example.Uri.AbsolutePath.Length > 1
				? example.Uri.AbsolutePath.TrimEnd('/')
				: example.Uri.AbsolutePath);

			// check expected query string params. Rather that _all_ keys match,
			// only check that the ones in reference doc example are present, because
			// the client may append more key/values such as "typed_keys"
			var expectedQueryParams = HttpUtility.ParseQueryString(example.Uri.Query);
			var actualQueryParams = HttpUtility.ParseQueryString(response.ApiCall.Uri.Query);
			if (expectedQueryParams.HasKeys())
			{
				foreach (var key in expectedQueryParams.AllKeys)
				{
					actualQueryParams.AllKeys.Should().Contain(key);
					var value = expectedQueryParams.Get(key);
					actualQueryParams.Get(key).Should().Be(value);
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
		private static List<JObject> ParseJObjects(string json)
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
