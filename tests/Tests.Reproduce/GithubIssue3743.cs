using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Tests.Core.Serialization;

namespace Tests.Reproduce
{
	public class GithubIssue3743
	{
		[U]
		public void SerializesUnicodeEscapeSequences()
		{
			var doc = new { value = new string(Enumerable.Range(0, 9727).Select(i => (char)i).ToArray()) };

			var internalJson = SerializationTester.Default.Client.SourceSerializer.SerializeToString(doc, formatting: SerializationFormatting.None);
			var jsonNet = JsonConvert.SerializeObject(doc, Formatting.None);

			// json.net lowercases unicode escape sequences, utf8json uppercases them (faster op). Both are valid and accepted
			internalJson.Should().BeEquivalentTo(jsonNet);
		}
	}
}
