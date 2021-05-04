// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.CodeStandards.Serialization
{
	public class PrimitiveObjectFormatterTests
	{
		private static DynamicValue GetValue(string json)
		{
			var bytes = Encoding.UTF8.GetBytes(json);
			var client = TestClient.FixedInMemoryClient(bytes);
			var response = client.LowLevel.Search<DynamicResponse>(PostData.Empty);
			return response.Body["value"];
		}

		[U]
		public void ReadDoubleWhenContainsDecimalPoint()
		{
			var value = GetValue(@"{""value"":0.43066659210472646}");
			value.Value.GetType().Should().Be<double>();
			value.Value.Should().Be(0.43066659210472646);
		}

		[U]
		public void ReadDoubleWhenContainsENotation()
		{
			var value = GetValue(@"{""value"":1.7976931348623157E+308}");
			value.Value.GetType().Should().Be<double>();
			value.Value.Should().Be(double.MaxValue);
		}

		[U]
		public void ReadDoubleWhenGreaterThanLongMaxValue()
		{
			var value = GetValue($"{{\"value\":{((double)long.MaxValue) + 1}}}");
			value.Value.GetType().Should().Be<double>();
			value.Value.Should().Be(((double)long.MaxValue) + 1);
		}

		[U]
		public void ReadDoubleWhenLessThanLongMinValue()
		{
			var value = GetValue($"{{\"value\":{((double)long.MinValue) - 1}}}");
			value.Value.GetType().Should().Be<double>();
			value.Value.Should().Be(((double)long.MinValue) - 1);
		}

		[U]
		public void ReadLongWhenLongMaxValue()
		{
			var value = GetValue($"{{\"value\":{long.MaxValue}}}");
			value.Value.GetType().Should().Be<long>();
			value.Value.Should().Be(long.MaxValue);
		}

		[U]
		public void ReadLongWhenLessThanLongMaxValue()
		{
			var value = GetValue($"{{\"value\":{long.MaxValue - 1}}}");
			value.Value.GetType().Should().Be<long>();
			value.Value.Should().Be(long.MaxValue - 1);
		}

		[U]
		public void ReadLongWhenGreaterThanLongMinValue()
		{
			var value = GetValue($"{{\"value\":{long.MinValue + 1}}}");
			value.Value.GetType().Should().Be<long>();
			value.Value.Should().Be(long.MinValue + 1);
		}

		[U]
		public void ReadLongWhenLongMinValue()
		{
			var value = GetValue($"{{\"value\":{long.MinValue}}}");
			value.Value.GetType().Should().Be<long>();
			value.Value.Should().Be(long.MinValue);
		}
	}
}

