using System;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.Serialization;

namespace Tests.Reproduce
{
	public class GithubIssue3356
	{
		[U]
		public void JoinFieldDeserializedCorrectly()
		{
			var doc = new MyDocument
			{
				Join = JoinField.Root("parent")
			};

			var tester = SerializationTester.DefaultWithJsonNetSerializer;
			var response = tester.Client.IndexDocument(doc);

			tester.AssertSerialize(response.ApiCall.RequestBodyInBytes, new { join = "parent" });
			doc = tester.AssertDeserialize<MyDocument>(response.ApiCall.RequestBodyInBytes);

			doc.Join.Match(
				p => { p.Name.Should().Be("parent"); },
				c => throw new InvalidOperationException("should not be called"));
		}

		private class MyDocument
		{
			public JoinField Join { get; set; }
		}
	}
}
