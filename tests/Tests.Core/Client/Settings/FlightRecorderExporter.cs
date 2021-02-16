// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Xunit;

namespace Tests.Core.Client.Settings
{
	public class FlightRecorderExporter
	{
		private static int _unique = 1;
		public static void OnBeforeReturn(IApiCallDetails response, RequestData requestData)
		{
			if (!response.ResponseMimeType.StartsWith("application/json")) return;

			var args = CreateArgsJson(requestData.Headers.Get("x-api-args")); //key=value;....
			var apiName = requestData.Headers.Get("x-api-name"); //count
			var fileName = requestData.Headers.Get("x-test-name"); //Search/Count/CountApiTests.cs-CountApiTests-<ClientUsage>b__15_2
			if (fileName == null) return;

			var tokens = fileName.Split('$');
			if (tokens.Length != 3) return;

			var unique = Interlocked.Increment(ref _unique);
			var path = tokens[0].Replace(".cs", "");
			var testDirectory = Path.Combine(XunitRunState.TemporaryDirectory.FullName, apiName, path);
			var @class = string.Join("_", tokens[1].Split(Path.GetInvalidFileNameChars()));
			var @cmethod = string.Join("_", tokens[2].Split(Path.GetInvalidFileNameChars().Concat(new[] { '<', '>' }).ToArray()));
			var fileNamePrefix = $"{@class}_{@cmethod}_{unique}_";

			Directory.CreateDirectory(testDirectory);

			var requestRecordingJson = CreateRequest(apiName, response.RequestBodyInBytes, args, response.HttpStatusCode.GetValueOrDefault());
			var requestFile = Path.Combine(testDirectory, fileNamePrefix + "_request.json");
			File.WriteAllText(requestFile, requestRecordingJson);

			var responseRecordingJson = CreateResponse(apiName, response.ResponseBodyInBytes, args, response.HttpStatusCode.GetValueOrDefault());
			var responseFile = Path.Combine(testDirectory, fileNamePrefix + "_response.json");
			File.WriteAllText(responseFile, responseRecordingJson);
		}

		public static string CreateArgsJson(string args)
		{
			if (args == null) return string.Empty;
			var argsJson = string.Join(",", args.Split(';')
				.Select(a => a.Split('='))
				.Where(a=>a.Length == 2)
				.Select(tokens => $"    \"{tokens[0]}\": \"{tokens[1]}\"")
				.ToArray());
			return argsJson;

		}
		public static string BytesToJson(byte[] bytes, bool isNdJsonApi = false)
		{
			if (bytes == null || bytes.Length <= 0) return string.Empty;

			var body = Encoding.UTF8.GetString(bytes);
			if (isNdJsonApi)
			{
				body = new StringBuilder()
					.Append("[")
					.Append(body.Replace("\n", "\n,").TrimEnd(','))
					.Append("]")
					.ToString();
			}

			var property = $"\"body\": {body}";
			return property;
		}

		public static string CreateResponse(string apiName, byte[] responseBytes, string args, int statusCode)
		{
			var error = (statusCode < 300 && statusCode >= 200) || statusCode == 404 ? "false" : "true";
			var body = BytesToJson(responseBytes);
			var contents = $@"{{
  ""api"": ""{apiName}"",
  ""origin"": [""nest-integration-tests""],
  ""payload"": {{
    {body}
  }},
  ""error"": {error},
  ""statusCode"": [{statusCode}]
}}
";
			return contents;
		}

		public static string CreateRequest(string apiName, byte[] requestBytes, string args, int statusCode)
		{
			var error = (statusCode < 300 && statusCode >= 200) || statusCode == 404 ? "false" : "true";
			var ndJsonApis = new[]
			{
				"bulk", "msearch", "msearch_template", "ml.find_file_structure", "monitoring.bulk", "xpack.ml.find_file_structure",
				"xpack.monitoring.bulk"
			};
			var isNdJsonApi = ndJsonApis.Contains(apiName);
			var body = BytesToJson(requestBytes, isNdJsonApi);
			if (!string.IsNullOrEmpty(args) && !string.IsNullOrEmpty(body)) args += ",";
			var contents = $@"{{
  ""api"": ""{apiName}"",
  ""origin"": [""nest-integration-tests""],
  ""args"": {{
{args}
    {body}
  }},
  ""error"": {error},
  ""statusCode"": [{statusCode}]
}}
";
			return contents;
		}
	}
}
