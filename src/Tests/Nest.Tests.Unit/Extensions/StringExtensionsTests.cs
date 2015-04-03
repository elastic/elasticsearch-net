using NUnit.Framework;
using Elasticsearch.Net;

namespace Nest.Tests.Unit.Extensions
{
	[TestFixture]
	public class StringExtensionsTests
	{
		[Test]
		public void CanConvertQueryStringToNameValueCollectionWithQuestionMark()
		{
			// Arrange
			var queryString = "?test1=one&test2=two";

			// Act
			var queryCollection = queryString.ToNameValueCollection();

			// Assert
			Assert.IsTrue(queryCollection["test1"] == "one");
			Assert.IsTrue(queryCollection["test2"] == "two");
		}

		[Test]
		public void CanConvertQueryStringToNameValueCollectionWithoutQuestionMark()
		{
			// Arrange
			var queryString = "test1=testone&test2=testtwo";

			// Act
			var queryCollection = queryString.ToNameValueCollection();

			// Assert
			Assert.IsTrue(queryCollection["test1"] == "testone");
			Assert.IsTrue(queryCollection["test2"] == "testtwo");
		}

		private const int s = 1000;
		private const int m = 60 * s;
		private const int h = 60 * m;
		private const int d = 24 * h;
		private const int w = 7 * d;

		/// <summary />
		[TestCase("0s", 0)]
		[TestCase("1s", 1 * s)]
		[TestCase("-1s", -1 * s)]
		[TestCase("1.5s", 1.5 * s)]
		[TestCase("90s", 90 * s)]
		[TestCase("1m", 1 * m)]
		[TestCase("1h", 1 * h)]
		[TestCase("1d", 1 * d)]
		[TestCase("1w", 1 * w)]
		[TestCase("0", 0)]
		[TestCase("1", 1)]
		public void ParseElasticSearchTimeUnit_Success(string value, double timeInMillis)
		{
			// act
			Assert.That(value.ToTimeSpan().Value.TotalMilliseconds, Is.EqualTo(timeInMillis));
		}

		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase("1.5")]
		[TestCase("2s2")]
		[TestCase("2ss")]
		[TestCase("4m2s")]
		[TestCase("2M")]
		[TestCase("2y")]
		[TestCase("s")]
		public void ParseElasticSearchTimeUnit_Fail_ReturnsNull(string value)
		{
			// act
			Assert.That(value.ToTimeSpan(), Is.Null);
		} 

	}
}
