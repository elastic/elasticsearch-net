using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Tests
{
	public static class UriExtensions
	{
		public static void PathEquals(this Uri u, string pathAndQueryString, string because)
		{
			var paths = (pathAndQueryString ?? "").Split(new[] { '?' }, 2);

			string path = paths.First(), query = string.Empty;
			if (paths.Length > 1)
				query = paths.Last();

			var expectedUri = new UriBuilder("http", "localhost", u.Port, path, "?" + query).Uri;

			u.AbsolutePath.Should().Be(expectedUri.AbsolutePath, because);
			var sanitizedQuery = u.Query
				.Replace("pretty=true&", "")
				.Replace("pretty=true", "")
				.Replace("error_trace=true&", "")
				.Replace("error_trace=true", "");
			u = new UriBuilder(u.Scheme, u.Host, u.Port, u.AbsolutePath, sanitizedQuery).Uri;

			var queries = new[] { u.Query, expectedUri.Query };
			if (queries.All(string.IsNullOrWhiteSpace)) return;
			if (queries.Any(string.IsNullOrWhiteSpace))
			{
				queries.Last().Should().Be(queries.First(), because);
				return;
			}

			var clientKeyValues = u.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k => !string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());
			var expectedKeyValues = expectedUri.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k => !string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());

			clientKeyValues.Count.Should().Be(expectedKeyValues.Count, because);
			clientKeyValues.Should().ContainKeys(expectedKeyValues.Keys.ToArray(), because);
			clientKeyValues.Should().Equal(expectedKeyValues, because);
		}
	}
}
