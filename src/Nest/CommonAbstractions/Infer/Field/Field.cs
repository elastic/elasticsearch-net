using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldJsonConverter))]
	public class Field : IEquatable<Field>, IUrlParameter
	{
		private readonly object _comparisonValue;
		private readonly Type _type;

		public string Name { get; }

		public Expression Expression { get; }

		public PropertyInfo Property { get; }

		public double? Boost { get; set; }

		public bool CachableExpression { get; }

		public Fields And<T>(Expression<Func<T, object>> field) where T : class =>
			new Fields(new [] { this, field });

		public Fields And(string field) => new Fields(new [] { this, field });

		public Field(string name, double? boost = null)
		{
			if (name.IsNullOrEmpty()) return;
			double? b;
			Name = ParseFieldName(name, out b);
			Boost = b ?? boost;
			_comparisonValue = Name;
		}

		public Field(Expression expression, double? boost = null)
		{
			if (expression == null) return;
			Expression = expression;
			Boost = boost;
			Type type;
			_comparisonValue = expression.ComparisonValueFromExpression(out type);
			_type = type;
			CachableExpression = !new HasConstantExpressionVisitor(expression).Found;
		}

		public Field(PropertyInfo property, double? boost = null)
		{
			if (property == null) return;
			Property = property;
			Boost = boost;
			_comparisonValue = property;
			_type = property.DeclaringType;
		}

		private static string ParseFieldName(string name, out double? boost)
		{
			boost = null;
			if (name == null) return null;

			var parts = name.Split(new [] { '^' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length > 1)
			{
				name = parts[0];
				boost = double.Parse(parts[1], CultureInfo.InvariantCulture);
			}
			return name;
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

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _comparisonValue?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (_type?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		public bool Equals(Field other)
		{
			return _type != null
				? other != null && _type == other._type && _comparisonValue.Equals(other._comparisonValue)
				: other != null && _comparisonValue.Equals(other._comparisonValue);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return this.Equals(obj as Field);
		}

		public static bool operator ==(Field x, Field y)
		{
			return Equals(x, y);
		}

		public static bool operator !=(Field x, Field y)
		{
			return !(x == y);
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
