using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests.Framework.SerializationTests
{
	public class ExceptionSerializationTests
	{
		private readonly IElasticsearchSerializer _elasticsearchNetSerializer;

		private readonly Exception _exception = new Exception("outer_exception",
			new InnerException("inner_exception",
				new InnerInnerException("inner_inner_exception")));

		public ExceptionSerializationTests()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connection = new InMemoryConnection();
			var values = new ConnectionConfiguration(pool, connection);
			var lowlevelClient = new ElasticLowLevelClient(values);
			_elasticsearchNetSerializer = lowlevelClient.Serializer;
		}

		//TODO Needs exception formatter
		[U(Skip="Needs Exception formatter")]
		public void LowLevelExceptionSerializationMatchesJsonNet()
		{
			var serialized = _elasticsearchNetSerializer.SerializeToString(_exception);

			object CreateException(Type exceptionType, string message, int depth)
			{
				return new
				{
					Depth = depth,
					ClassName = exceptionType.FullName,
					Message = message,
					Source = (object)null,
					StackTraceString = (object)null,
					RemoteStackTraceString = (object)null,
					RemoteStackIndex = 0,
					HResult = -2146233088,
					HelpURL = (object)null
				};
			}

			var simpleJsonException = new[]
			{
				CreateException(typeof(Exception), "outer_exception", 0),
				CreateException(typeof(InnerException), "inner_exception", 1),
				CreateException(typeof(InnerInnerException), "inner_inner_exception", 2),
			};

			var jArray = JArray.Parse(serialized);
			var jArray2 = JArray.Parse(JsonConvert.SerializeObject(simpleJsonException));

			JToken.DeepEquals(jArray, jArray2).Should().BeTrue();
		}

		public class InnerException : Exception
		{
			public InnerException(string message, Exception innerException) : base(message, innerException) { }
		}

		public class InnerInnerException : Exception
		{
			public InnerInnerException(string message) : base(message) { }
		}
	}
}
