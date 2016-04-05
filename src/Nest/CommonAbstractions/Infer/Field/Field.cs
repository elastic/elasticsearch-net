using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldJsonConverter))]
	public class Field : IEquatable<Field>, IUrlParameter
	{
		private string _name;
		private Expression _expression;
		private PropertyInfo _property;

		public string Name
		{
			get { return _name; }
			set
			{
				double? b;
				_name = ParseFieldName(value, out b);
				if (b.HasValue) Boost = b;
				SetComparisonValue(_name);
			}
		}

		public Expression Expression
		{
			get { return _expression; }
			set
			{
				_expression = value;
				var comparisonValue = ComparisonValueFromExpression(value);
				SetComparisonValue(comparisonValue);
			}
		}

		public PropertyInfo Property
		{
			get { return _property; }
			set
			{
				_property = value;
				SetComparisonValue(value);
			}
		}

		public double? Boost { get; set; }

		private object ComparisonValue { get; set; }

		public Fields And<T>(Expression<Func<T, object>> field) where T : class =>
			new Fields(new [] { this, field });

		public Fields And(string field) => new Fields(new [] { this, field });

		public static Field Create(string name, double? boost = null)
		{
			if (name.IsNullOrEmpty()) return null;

			Field field = name;
			if (!field.Boost.HasValue)
				field.Boost = boost;

			return field;
		}

		public static Field Create(Expression expression, double? boost = null)
		{
			Field field = expression;
			field.Boost = boost;
			return field;
		}

		private static string ParseFieldName(string name, out double? boost)
		{
			boost = null;
			if (name == null) return null;

			var parts = name.Split(new [] { '^' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length > 1)
			{
				name = parts[0];
				boost = Double.Parse(parts[1], CultureInfo.InvariantCulture);
			}
			return name;
		}

		private static object ComparisonValueFromExpression(Expression expression)
		{
			if (expression == null) return null;

			var lambda = expression as LambdaExpression;
			if (lambda == null)
				return expression.ToString();

			var memberExpression = lambda.Body as MemberExpression;
			if (memberExpression == null)
				return expression.ToString();

			return memberExpression;
		}

		public static implicit operator Field(string name)
		{
			return name.IsNullOrEmpty() ? null : new Field
			{
				Name = name
			};
		}

		public static implicit operator Field(Expression expression)
		{
			return expression == null ? null : new Field
			{
				Expression = expression
			};
		}

		public static implicit operator Field(PropertyInfo property)
		{
			return property == null ? null : new Field
			{
				Property = property
			};
		}

		public override int GetHashCode() => ComparisonValue?.GetHashCode() ?? 0;

		bool IEquatable<Field>.Equals(Field other) => Equals(other);

		public override bool Equals(object obj)
		{
			var other = obj as Field;
			if (other == null)
				return false;
			return ComparisonValue.Equals(other.ComparisonValue);
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass field name on querysting but it could not be resolved because no nest settings are available");
			var infer = new Inferrer(nestSettings);
			return infer.Field(this);
		}

		private void SetComparisonValue(object value)
		{
			if (ComparisonValue != null && value != null)
				throw new InvalidOperationException($"{nameof(ComparisonValue)} already has a value");

			ComparisonValue = value;
		}
	}
}
