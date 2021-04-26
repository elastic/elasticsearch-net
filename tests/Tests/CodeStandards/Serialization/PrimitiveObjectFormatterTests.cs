/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

