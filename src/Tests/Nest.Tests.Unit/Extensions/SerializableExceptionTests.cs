using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Extensions
{
	[TestFixture]
	public class SerializableExceptionTests 
	{
		private readonly ElasticsearchResponse<Stream> TestResponse =
			ElasticsearchResponse<Stream>.Create(new ConnectionSettings(), 200, "GET", "/", null, null);

		private readonly ElasticsearchServerError ServerError = new ElasticsearchServerError
		{
			Status = 400, Error = "some error", ExceptionType = "some type"
		};

		[Test]
		public void ConnectionException() => 
			SerializedException(new ConnectionException(400, "response"), e =>
		{
			e.HttpStatusCode.Should().Be(400);
		});

		[Test]
		public void ElasticsearchAuthenticationException() => 
			SerializedException(new ElasticsearchAuthenticationException(TestResponse));

		[Test]
		public void ElasticsearchAuthorizationException() => 
			SerializedException(new ElasticsearchAuthorizationException(TestResponse));

		[Test]
		public void ElasticsearchServerException() => 
			SerializedException(new ElasticsearchServerException(ServerError), e =>
			{
				e.Status.Should().Be(ServerError.Status);
				e.ExceptionType.Should().Be(ServerError.ExceptionType);
			});

		[Test]
		public void MaxRetryException() => SerializedException(new MaxRetryException("message"));

		[Test]
		public void SniffException() => SerializedException(new SniffException(new MaxRetryException("message")));

		[Test]
		public void PingException() => SerializedException(new PingException(new Uri("http://localhost:9200"), null));

		[Test]
		public void DispatchException() => SerializedException(new DispatchException(""));

		[Test]
		public void DslException() => SerializedException(new DslException(""));

		[Test]
		public void ReindexException() => SerializedException(new ReindexException(TestResponse));

		[Test]
		public void SnapshotException() => SerializedException(new SnapshotException(TestResponse, ""));



		private void SerializedException<TException>(TException original, Action<TException> assert = null)
			where TException : Exception
		{
            using (var ms = new MemoryStream())
            {
				var bf = new BinaryFormatter();
				Action act = () => bf.Serialize(ms, original);
				act.ShouldNotThrow();
                ms.Seek(0, 0);
				TException e = null;
                act = () => e = (TException)bf.Deserialize(ms);
				act.ShouldNotThrow();
				e.Should().NotBeNull();
				assert?.Invoke(e);
            }
		}

	}
}