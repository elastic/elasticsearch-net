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

using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(CronExpressionFormatter))]
	public class CronExpression : ScheduleBase, IEquatable<CronExpression>
	{
		private readonly string _expression;

		public CronExpression(string expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));
			if (expression.Length == 0) throw new ArgumentException("must have a length", nameof(expression));

			_expression = expression;
		}

		public bool Equals(CronExpression other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return string.Equals(_expression, other._expression);
		}

		public static implicit operator CronExpression(string expression) =>
			new CronExpression(expression);

		public override string ToString() => _expression;

		internal override void WrapInContainer(IScheduleContainer container) => container.Cron = this;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((CronExpression)obj);
		}

		public override int GetHashCode() => _expression?.GetHashCode() ?? 0;

		public static bool operator ==(CronExpression left, CronExpression right) => Equals(left, right);

		public static bool operator !=(CronExpression left, CronExpression right) => !Equals(left, right);
	}

	internal class CronExpressionFormatter : IJsonFormatter<CronExpression>
	{
		public CronExpression Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var expression = reader.ReadString();
			return new CronExpression(expression);
		}

		public void Serialize(ref JsonWriter writer, CronExpression value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) writer.WriteNull();
			else writer.WriteString(value.ToString());
		}
	}
}
