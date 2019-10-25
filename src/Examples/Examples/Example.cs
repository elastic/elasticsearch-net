using System;
using System.Text.RegularExpressions;
using Elasticsearch.Net;
using Newtonsoft.Json.Linq;

namespace Examples
{
	public class Example
	{
		private static readonly Uri BaseUri = new Uri("http://localhost:9200");

		private static readonly Regex Callout = new Regex(@"\\<\d+>\s*$", RegexOptions.Multiline);

		private Example(HttpMethod method, Uri uri, string body)
		{
			Method = method;
			Uri = uri;
			Body = body;
		}

		public string Body { get; private set; }

		public HttpMethod Method { get; set; }

		public Uri Uri { get; set; }

		public void ApplyBodyChanges(Action<JObject> action)
		{
			var body = Body == null
				? new JObject()
				: JObject.Parse(Body);
			action(body);
			Body = body.ToString();
		}

		public static Example Create(string example)
		{
			var exampleParts = example.Split(new[] { "\r\n", "\r", "\n" }, 2, StringSplitOptions.None);
			var urlParts = exampleParts[0].Split(new[] { " " }, 2, StringSplitOptions.None);
			var method = (HttpMethod)Enum.Parse(typeof(HttpMethod), urlParts[0], true);
			var path = Callout.Replace(urlParts[1], string.Empty);
			var body = exampleParts.Length > 1 ? exampleParts[1] : null;

			if (!Uri.TryCreate(BaseUri, path, out var uri))
				throw new Exception($"Cannot parse Uri from {path}");

			if (body != null)
				// remove callouts, like \<1>, from the reference body
				body = Callout.Replace(body, string.Empty);

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
