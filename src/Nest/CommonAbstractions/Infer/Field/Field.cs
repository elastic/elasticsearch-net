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
		public string Name { get; private set; }

		public Expression Expression { get; private set; }

		public PropertyInfo Property { get; private set; }

		public double? Boost { get; set; }

		private object ComparisonValue { get; set; }

		public Fields And<T>(Expression<Func<T, object>> field) where T : class =>
			new Fields(new [] { this, field });

		public Fields And(string field) => new Fields(new [] { this, field });

		public Field(string name, double? boost = null)
		{
			if (!name.IsNullOrEmpty())
			{
				double? b;
				Name = ParseFieldName(name, out b);
				Boost = b ?? boost;
				ComparisonValue = Name;
			}
		}

		public Field(Expression expression, double? boost = null)
		{
			if (expression != null)
			{
				Expression = expression;
				Boost = boost;
				ComparisonValue = ComparisonValueFromExpression(expression);
			}
		}

		public Field(PropertyInfo property, double? boost = null)
		{
			if (property != null)
			{
				Property = property;
				Boost = boost;
				ComparisonValue = property;
			}
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
			return name.IsNullOrEmpty() ? null : new Field(name);
		}

		public static implicit operator Field(Expression expression)
		{
			return expression == null ? null : new Field(expression);
		}

		public static implicit operator Field(PropertyInfo property)
		{
			return property == null ? null : new Field(property);
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
	}
}
