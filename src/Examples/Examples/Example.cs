using System;
using System.Text.RegularExpressions;
using Elasticsearch.Net;

namespace Examples
{
	public class Example
	{
		private static readonly Uri BaseUri = new Uri("http://localhost:9200");

		private Example(HttpMethod method, Uri uri, string body)
		{
			Method = method;
			Uri = uri;
			Body = body;
		}

		public string Body { get; set; }

		public HttpMethod Method { get; set; }

		public Uri Uri { get; set; }

		public static Example Create(string example)
		{
			var exampleParts = example.Split(new[] { "\r\n", "\r", "\n" }, 2, StringSplitOptions.None);
			var urlParts = exampleParts[0].Split(new[] { " " }, 2, StringSplitOptions.None);
			var method = (HttpMethod)Enum.Parse(typeof(HttpMethod), urlParts[0], true);
			var path = urlParts[1];
			var body = exampleParts.Length > 1 ? exampleParts[1] : null;

			if (!Uri.TryCreate(BaseUri, path, out var uri))
				throw new Exception($"Cannot parse Uri from {path}");

			if (body != null)
				// remove callouts, like \<1>, from the reference body
				body = Regex.Replace(body, @"\\<\d+>\s*$", string.Empty, RegexOptions.Multiline);

			return new Example(method, uri, body);
		}

		public static Example ApplyGlobalChanges(Example example)
		{
			// docs use GET with body for lots of examples, which the client does not support.
			// Rather than changing the example for all cases, special case GET with body
			if (example.Body != null && example.Method == HttpMethod.GET)
				example.Method = HttpMethod.POST;

			return example;
		}
	}
}
