// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
