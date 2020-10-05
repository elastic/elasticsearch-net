using System.Collections.Generic;
using System.Collections.Specialized;
using Elastic.Transport;
using FluentAssertions;
using Xunit;

namespace Tests.Extensions
{
	public class NameValueCollectionExtensionsTests
	{
		[Theory]
		[MemberData(nameof(QueryStringTestData))]
		public void ToQueryString_ReturnsExpectedString(NameValueCollection nvc, string expected) =>
			RequestData.ToQueryString(nvc).Should().Be(expected);

		public static IEnumerable<object[]> QueryStringTestData =>
			new List<object[]>
			{
				new object[] { new NameValueCollection
				{
					{ "q", "title:\"The Right Way\" AND mod_date:[20020101 TO 20030101]" },
					{ "from", "10000" },
					{ "request_cache", bool.TrueString },
					{ "size", "100" }
				}, "?q=title%3A%22The%20Right%20Way%22%20AND%20mod_date%3A%5B20020101%20TO%2020030101%5D&from=10000&request_cache=True&size=100" },

				new object[] { new NameValueCollection
				{
					{ "q", "name:john~1 AND (age:[30 TO 40} OR surname:K*) AND -city" },
				}, "?q=name%3Ajohn~1%20AND%20%28age%3A%5B30%20TO%2040%7D%20OR%20surname%3AK%2A%29%20AND%20-city" },

				new object[] { new NameValueCollection
				{
					{ "q", null },
				}, "?q" },

				new object[] { new NameValueCollection
				{
					{ "emoji", "😅"}
				}, "?emoji=%F0%9F%98%85" },

				new object[] { new NameValueCollection
				{
					{ "€", "€"}
				}, "?%E2%82%AC=%E2%82%AC" }
			};
	}
}
