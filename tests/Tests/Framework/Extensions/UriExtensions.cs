// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Tests.Core.Extensions;

namespace Tests.Framework.Extensions
{
	public static class UriExtensions
	{
		public static void PathEquals(this Uri actualUri, string pathAndQueryString, string origin)
		{
			var expectedUri = CreateExpectedUri(actualUri, pathAndQueryString);

			var actualParameters = ExplodeQueryString(actualUri);
			var expectedParameters = ExplodeQueryString(expectedUri);

			AssertSpecialQueryStringValues(expectedUri, expectedParameters, actualUri, actualParameters, origin);

			actualUri = CreateUri(actualUri, actualParameters);
			expectedUri = CreateUri(expectedUri, expectedParameters);

			AssertQueryString(actualUri, expectedUri, origin);

			ComparePaths(actualUri, expectedUri, origin);
		}

		private static Uri CreateUri(Uri baseUri, Dictionary<string, string> newQueryString)
		{
			var query = FlattenQueryString(newQueryString);
			var uriBuilder = new UriBuilder(baseUri) { Query = query };
			return uriBuilder.Uri;
		}

		private static void AssertQueryString(Uri actualUri, Uri expectedUri, string origin)
		{
			var eol = Environment.NewLine;
			var because = $"{eol} query string from {origin}: {expectedUri.Query} on {expectedUri.PathAndQuery}";
			because += $"{eol}Actual querystring from {origin}  : {actualUri.Query} on {actualUri.PathAndQuery}{eol}";

			var actualParameters = ExplodeQueryString(actualUri);
			var expectedParameters = ExplodeQueryString(expectedUri);

			actualParameters.Keys.Should()
				.BeEquivalentTo(expectedParameters.Keys, $"All query string parameters need to be asserted.{eol}{{0}}", because);

			if (actualParameters.Count == 0) return;

			actualParameters.Should().ContainKeys(expectedParameters.Keys.ToArray(), because);
			actualParameters.Should().Equal(expectedParameters, because);
		}

		private static void AssertSpecialQueryStringValues(
			Uri expectedUri,
			Dictionary<string, string> expectedParameters,
			Uri actualUri,
			Dictionary<string, string> actualParameters,
			string origin
		)
		{
			var eol = Environment.NewLine;
			var because = $"{eol}Expected query string from {origin}: {expectedUri.Query} on {expectedUri.PathAndQuery}";
			because += $"{eol}Actual query string from {origin}: {actualUri.Query} on {actualUri.PathAndQuery}{eol}";

			//only assert these if they appear in expectedUri
			var specialQueryStringParameters = new[] { "pretty", "typed_keys", "error_trace" };
			foreach (var key in specialQueryStringParameters)
			{
				if (!expectedParameters.ContainsKey(key)) continue;

				var expected = expectedParameters[key];
				actualParameters.Should().ContainKey(key, $"query value for '{{0}}' expected to exist{eol}{{1}}", key, because);
				var actual = actualParameters[key];
				new[] { key, actual }.Should().BeEquivalentTo(new[] { key, expected }, $"query value for '{{0}}' should be equal{eol}{{1}}", key, because);
			}

			foreach (var key in specialQueryStringParameters)
			{
				if (actualParameters.ContainsKey(key)) actualParameters.Remove(key);
				if (expectedParameters.ContainsKey(key)) expectedParameters.Remove(key);
			}
		}

		private static void ComparePaths(Uri actualUri, Uri expectedUri, string origin)
		{
			var eol = Environment.NewLine;
			var because = $"{eol}Expected from {origin}: {expectedUri.PathAndQuery}";
			because += $"{eol}Actual from {origin}: {actualUri.PathAndQuery}{eol}";
			actualUri.AbsolutePath.Should().Be(expectedUri.AbsolutePath, because);
		}

		private static Uri CreateExpectedUri(Uri u, string pathAndQueryString)
		{
			var paths = (pathAndQueryString ?? "").Split(new[] { '?' }, 2);

			string path = paths.First(), query = string.Empty;
			if (paths.Length > 1) query = paths.Last();

			var expectedUri = new UriBuilder("http", "localhost", u.Port, path, "?" + query).Uri;
			return expectedUri;
		}

		private static string FlattenQueryString(Dictionary<string, string> queryString)
		{
			if (queryString == null || queryString.Count == 0) return string.Empty;

			return string.Join("&", queryString.Select(kv => $"{kv.Key}={kv.Value}"));
		}

		private static Dictionary<string, string> ExplodeQueryString(Uri u)
		{
			var query = u.Query;
			if (string.IsNullOrEmpty(query) || query.Length <= 1) return new Dictionary<string, string>();

			return query.Substring(1)
				.Split('&')
				.Select(v => v.Split('='))
				.Where(k => !string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());
		}
	}
}
