// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.CodeStandards.Serialization
{
	public class PrimitiveObjectFormatterTests
	{
		private static T GetValue<T>(string json)
		{
			var bytes = Encoding.UTF8.GetBytes(json);
			var client = TestClient.FixedInMemoryClient(bytes);
			var response = client.LowLevel.Search<DynamicResponse>(PostData.Empty);
			return response.Get<T>("value");
		}

		[U]
		public void ReadDoubleWhenContainsDecimalPoint()
		{
			var value = GetValue<double>(@"{""value"":0.43066659210472646}");
			value.GetType().Should().Be<double>();
			value.Should().Be(0.43066659210472646);
		}

		[U]
		public void ReadDoubleWhenContainsENotation()
		{
			var value = GetValue<double>(@"{""value"":1.7976931348623157E+308}");
			value.GetType().Should().Be<double>();
			value.Should().Be(double.MaxValue);
		}

		[U]
		public void ReadDoubleWhenGreaterThanLongMaxValue()
		{
			var value = GetValue<double>($"{{\"value\":{((double)long.MaxValue) + 1}}}");
			value.GetType().Should().Be<double>();
			value.Should().Be(((double)long.MaxValue) + 1);
		}

		[U]
		public void ReadDoubleWhenLessThanLongMinValue()
		{
			var value = GetValue<double>($"{{\"value\":{((double)long.MinValue) - 1}}}");
			value.GetType().Should().Be<double>();
			value.Should().Be(((double)long.MinValue) - 1);
		}

		[U]
		public void ReadLongWhenLongMaxValue()
		{
			var value = GetValue<long>($"{{\"value\":{long.MaxValue}}}");
			value.GetType().Should().Be<long>();
			value.Should().Be(long.MaxValue);
		}

		[U]
		public void ReadLongWhenLessThanLongMaxValue()
		{
			var value = GetValue<long>($"{{\"value\":{long.MaxValue - 1}}}");
			value.GetType().Should().Be<long>();
			value.Should().Be(long.MaxValue - 1);
		}

		[U]
		public void ReadLongWhenGreaterThanLongMinValue()
		{
			var value = GetValue<long>($"{{\"value\":{long.MinValue + 1}}}");
			value.GetType().Should().Be<long>();
			value.Should().Be(long.MinValue + 1);
		}

		[U]
		public void ReadLongWhenLongMinValue()
		{
			var value = GetValue<long>($"{{\"value\":{long.MinValue}}}");
			value.GetType().Should().Be<long>();
			value.Should().Be(long.MinValue);
		}
	}
}

