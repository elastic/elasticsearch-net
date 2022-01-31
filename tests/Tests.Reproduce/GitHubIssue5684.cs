// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;
using Nest;
using System.Runtime.Serialization;
using System.IO;
using FluentAssertions;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;

namespace Tests.Reproduce
{	
	public class GitHubIssue5684
	{
		private static readonly byte[] ResponseBytes = Encoding.UTF8.GetBytes(@"{
    ""script"": ""doc['sales_price'].value * 2""
}");

		[U]
		public void DeserialiseSimpleScript()
		{
			var client = new ElasticClient();
			var result = client.RequestResponseSerializer.Deserialize<Sample>(new MemoryStream(ResponseBytes));
			result.Should().NotBeNull();
			result.Script.Should().BeOfType<InlineScript>().Subject.Source.Should().Be("doc['sales_price'].value * 2");
		}

		private class Sample
		{
			[DataMember(Name = "script")]
			public IScript Script { get;set; }
		}
	}
}
