using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CronExpressionJsonConverter))]
	public class CronExpression : ScheduleBase, IEquatable<CronExpression>
	{
		private string _expression;

		public CronExpression(string expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));
			if (expression.Length == 0) throw new ArgumentException("must have a length", nameof(expression));

			this._expression = expression;
		}

		public static implicit operator CronExpression(string expression) =>
			new CronExpression(expression);

		public override string ToString() => _expression;

		internal override void WrapInContainer(IScheduleContainer container) => container.Cron = this;

		public bool Equals(CronExpression other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(_expression, other._expression);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((CronExpression)obj);
		}

		public override int GetHashCode()
		{
			return _expression?.GetHashCode() ?? 0;
		}

		public static bool operator ==(CronExpression left, CronExpression right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(CronExpression left, CronExpression right)
		{
			return !Equals(left, right);
		}
	}

	internal class CronExpressionJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null) writer.WriteNull();
			else writer.WriteValue(value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var expression = (string)reader.Value;
			return new CronExpression(expression);
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(CronExpression);
	}
}
