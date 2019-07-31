using System;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.Extensions;
using Xunit;

namespace Examples
{
	public static class ResponseExtensions
	{
		public static void MatchesExample(this IResponse response, string example)
		{
			var exampleParts = example.Split(new[] { "\r\n", "\r", "\n" }, 2, StringSplitOptions.None);
			var urlParts = exampleParts[0].Split(new[] { " " }, 2, StringSplitOptions.None);
			var method = Enum.Parse<HttpMethod>(urlParts[0], true);
			var path = urlParts[1];
			var body = exampleParts.Length > 1 ? exampleParts[1] : null;

			response.ApiCall.HttpMethod.Should().Be(method);
			response.ApiCall.Uri.AbsolutePath.Should().Be(path);

			if (body != null)
			{
				var expected = JToken.Parse(body);
				var actual = JToken.Parse(Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes));
				var matches = JToken.DeepEquals(expected, actual);

				if (!matches)
				{
					(actual as JObject)?.DeepSort();
					(expected as JObject)?.DeepSort();

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
