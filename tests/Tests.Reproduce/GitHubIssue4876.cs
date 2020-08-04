// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue4876
	{
		private readonly ElasticClient _client;

		public GitHubIssue4876() => _client = new ElasticClient();

		[U]
		public void CanDeserializeExtendedFormatOffsetIso8601DateTime() =>
			AssertDateTime("{\"timestamp\":\"2020-07-31T12:29:29.4425068+10:00\"}");

		[U]
		public void CanDeserializeBasicFormatOffsetWithMinutesIso8601DateTime() =>
			AssertDateTime("{\"timestamp\":\"2020-07-31T12:29:29.4425068+1000\"}");

		[U]
		public void CanDeserializeBasicFormatOffsetIso8601DateTime() =>
			AssertDateTime("{\"timestamp\":\"2020-07-31T12:29:29.4425068+10\"}");

		[U]
		public void ThrowExceptionWhenInvalidBasicFormatOffset()
		{
			Action action = () => AssertDateTime("{\"timestamp\":\"2020-07-31T12:29:29.4425068+100\"}");
			action.Should().Throw<InvalidOperationException>();
		}

		[U]
		public void ThrowExceptionWhenInvalidBasicFormatOffset2()
		{
			Action action = () => AssertDateTime("{\"timestamp\":\"2020-07-31T12:29:29.4425068-10000\"}");
			action.Should().Throw<InvalidOperationException>();
		}

		[U]
		public void CanDeserializeExtendedFormatOffsetIso8601DateTimeOffset() =>
			AssertDateTimeOffset("{\"timestamp\":\"2020-07-31T12:29:29.4425068+10:00\"}");

		[U]
		public void CanDeserializeBasicFormatOffsetWithMinutesIso8601DateTimeOffset() =>
			AssertDateTimeOffset("{\"timestamp\":\"2020-07-31T12:29:29.4425068+1000\"}");

		[U]
		public void CanDeserializeBasicFormatOffsetIso8601DateTimeOffset() =>
			AssertDateTimeOffset("{\"timestamp\":\"2020-07-31T12:29:29.4425068+10\"}");

		private void AssertDateTime(string json)
		{
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
			var document = _client.SourceSerializer.Deserialize<Document>(stream);

			document.Timestamp.Should().Be(
				new DateTime(637317593694425068, DateTimeKind.Utc).ToLocalTime());
		}

		private void AssertDateTimeOffset(string json)
		{
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
			var document = _client.SourceSerializer.Deserialize<Document2>(stream);

			document.Timestamp.Should().Be(
				new DateTimeOffset(637317593694425068, TimeSpan.Zero));
		}

		public class Document
		{
			public DateTime Timestamp { get; set; }
		}

		public class Document2
		{
			public DateTimeOffset Timestamp { get; set; }
		}
	}
}
