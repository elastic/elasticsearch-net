// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Elastic.Transport;
using Elasticsearch.Net;
using Newtonsoft.Json.Linq;

namespace Examples
{
	public class Example
	{
		private static readonly Uri BaseUri = new Uri("http://localhost:9200");

		private static readonly Regex Callout = new Regex(@"(?:\\)?<\d+>\s*$", RegexOptions.Multiline);

		private Example(HttpMethod method, Uri uri, string body)
		{
			Method = method;
			Uri = new UriBuilder(uri);
			Body = body;
		}

		public string Body { get; private set; }

		public HttpMethod Method { get; set; }

		public UriBuilder Uri { get; set; }

		public void ApplyBulkBodyChanges(Action<List<JObject>> action)
		{
			var body = Body == null
				? new List<JObject>()
				: ResponseExtensions.ParseJObjects(Body);
			action(body);
			Body = string.Join(Environment.NewLine, body.Select(b => b.ToString()).ToArray());
		}

		public void ApplyBodyChanges(Action<JObject> action)
		{
			var body = Body == null
				? new JObject()
				: JObject.Parse(Body);
			action(body);
			Body = body.ToString();
		}

		public Example MoveQueryStringToBody(string key, object value)
		{
			Uri.Query = Uri.Query.Replace($"{key}={value}", string.Empty);
			ApplyBodyChanges(body =>
			{
				body[key] = new JValue(value);
			});
			return this;
		}

		public static Example Create(string example)
		{
			var exampleParts = example.Split(new[] { "\r\n", "\r", "\n" }, 2, StringSplitOptions.None);
			var urlParts = exampleParts[0].Split(new[] { " " }, 2, StringSplitOptions.None);
			var method = (HttpMethod)Enum.Parse(typeof(HttpMethod), urlParts[0], true);
			var path = Callout.Replace(urlParts[1], string.Empty);
			var body = exampleParts.Length > 1 ? exampleParts[1] : null;

			if (!System.Uri.TryCreate(BaseUri, path, out var uri))
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

			// Searches are always POST
			if (example.Uri.Path.Contains("_search") && example.Method == HttpMethod.GET)
				example.Method = HttpMethod.POST;

			return example;
		}
	}
}
