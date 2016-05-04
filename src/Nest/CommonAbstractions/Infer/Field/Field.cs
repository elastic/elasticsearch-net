using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;
using System.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldJsonConverter))]
	public class Field : IEquatable<Field>, IUrlParameter
	{
		private string _name;
		private Expression _expression;
		private PropertyInfo _property;
		private Type _type;
		private object _comparisonValue;

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
				Type type;
				var comparisonValue = value.ComparisonValueFromExpression(out type);
				_type = type;
				SetComparisonValue(comparisonValue);
				CacheableExpression = !new HasConstantExpressionVisitor(value).Found;
			}
		}

		public PropertyInfo Property
		{
			get { return _property; }
			set
			{
				_property = value;
				SetComparisonValue(value);
				_type = value.DeclaringType;
			}
		}

		public double? Boost { get; set; }

		public bool CacheableExpression { get; private set; }

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
			if (expression == null) return null;

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
				boost = double.Parse(parts[1], CultureInfo.InvariantCulture);
			}
			return name;
		}

		public static implicit operator Field(string name)
		{
			return name.IsNullOrEmpty() ? null : new Field
			{
				Name = name,
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

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _comparisonValue?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (_type?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		bool IEquatable<Field>.Equals(Field other)
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
			return ((IEquatable<Field>)this).Equals(obj as Field);
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

		private void SetComparisonValue(object value)
		{
			if (_comparisonValue != null && value != null)
				throw new InvalidOperationException($"{nameof(_comparisonValue)} already has a value");

			_comparisonValue = value;
		}
	}
}
