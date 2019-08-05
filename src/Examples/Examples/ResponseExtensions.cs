using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.Extensions;
using Xunit;

namespace Examples
{
	public static class ResponseExtensions
	{
		/// <summary>
		/// Asserts that the client generated request matches the example from the docs
		/// </summary>
		public static void MatchesExample(this IResponse response, string content, Func<Example, Example> clientChanges = null)
		{
			var example = Example.CreateWithGlobalClientChanges(content);

			// a specific example might use notation that is not supported by the client, because it only
			// supports the long form. Allow a function to be passed to make modifications to suit.
			if (clientChanges != null)
				example = clientChanges(example);

			response.ApiCall.HttpMethod.Should().Be(example.Method);
			response.ApiCall.Uri.AbsolutePath.Should().Be(example.Uri.AbsolutePath.TrimEnd('/'));

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
				var expected = JObject.Parse(example.Body);
				var actual = JObject.Parse(Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes));
				var matches = JToken.DeepEquals(expected, actual);

				if (!matches)
				{
					actual.DeepSort();
					expected.DeepSort();

					var sortedExpected = expected.ToString();
					var sortedActual = actual.ToString();
					var diff = sortedExpected.Diff(sortedActual);

					if (!string.IsNullOrWhiteSpace(diff))
						Assert.True(false, $"body diff: {diff}");
				}
			}
		}
	}
}
