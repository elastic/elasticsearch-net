using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce629Tests : BaseJsonTests
	{
		public class UriSerializationTest
		{
			public Uri MyUri { get; set; }
		}

		[Test]
		public void DeserializingRelativeUriThrowsException()
		{
			var json = @"{""MyUri"":""/relative""}";
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
			var uriTest = _client.Serializer.Deserialize<UriSerializationTest>(stream);
			uriTest.Should().NotBeNull();
			uriTest.MyUri.Should().NotBeNull();
			uriTest.MyUri.IsAbsoluteUri.Should().BeFalse();
			uriTest.MyUri.OriginalString.Should().Be("/relative");
		}

		[Test]
		public void NestSerializesUriAsString()
		{
			var uriTest = new UriSerializationTest { MyUri = new Uri("http://localhost:9200") };

			var serialized = Encoding.UTF8.GetString(_client.Serializer.Serialize(uriTest));
			serialized.JsonEquals(@"{ myUri: ""http://localhost:9200""}");

			var deserialized = _client.Serializer.Deserialize<UriSerializationTest>(new MemoryStream(Encoding.UTF8.GetBytes(serialized)));
			deserialized.MyUri.AbsoluteUri.Should().Be("http://localhost:9200/");
		}

		[Test]
		public void JsonNetSerializesUriAsString()
		{
			var uriTest = new UriSerializationTest { MyUri = new Uri("http://localhost:9200") };

			var serialized = JsonConvert.SerializeObject(uriTest);
			serialized.JsonEquals(@"{ MyUri: ""http://localhost:9200""}");

			var deserialized = JsonConvert.DeserializeObject<UriSerializationTest>(serialized);
			deserialized.MyUri.AbsoluteUri.Should().Be("http://localhost:9200/");
		}
	}
}

