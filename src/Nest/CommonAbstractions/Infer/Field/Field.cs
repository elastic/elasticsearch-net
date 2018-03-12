using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Field : IEquatable<Field>, IUrlParameter
	{
		private readonly object _comparisonValue;
		private readonly Type _type;

		public string Name { get; }

		public Expression Expression { get; }

		public PropertyInfo Property { get; }

		public double? Boost { get; set; }

		public bool CachableExpression { get; }

		internal string DebugDisplay =>
			$"{Expression?.ToString() ?? PropertyDebug ?? Name}{(Boost.HasValue ? "^" + Boost.Value: "")}{(_type == null ? "" : " typeof: " + _type.Name)}";

		private string PropertyDebug => Property == null ? null : $"PropertyInfo: {Property.Name}";

		public Fields And(Field field) => new Fields(new [] { this, field });

		public Fields And<T>(Expression<Func<T, object>> field, double? boost = null) where T : class =>
			new Fields(new [] { this, new Field(field, boost) });

		public Fields And(string field, double? boost = null) => new Fields(new [] { this, new Field(field, boost) });

		public Fields And(PropertyInfo property, double? boost = null) => new Fields(new [] { this, new Field(property, boost) });

		public Field(string name, double? boost = null)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			double? b;
			Name = ParseFieldName(name, out b);
			Boost = b ?? boost;
			_comparisonValue = Name;
		}

		public Field(Expression expression, double? boost = null)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
			Boost = boost;
			_comparisonValue = expression.ComparisonValueFromExpression(out var type);
			_type = type;
			CachableExpression = !new HasVariableExpressionVisitor(expression).Found;
		}

		public Field(PropertyInfo property, double? boost = null)
		{
			Property = property ?? throw new ArgumentNullException(nameof(property));
			Boost = boost;
			_comparisonValue = property;
			_type = property.DeclaringType;
		}

		private static string ParseFieldName(string name, out double? boost)
		{
			boost = null;
			if (name == null) return null;

			var parts = name.Split(new [] { '^' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length <= 1) return name;
			name = parts[0];
			boost = double.Parse(parts[1], CultureInfo.InvariantCulture);
			return name;
		}

		public static implicit operator Field(string name) => name.IsNullOrEmpty() ? null : new Field(name);

		public static implicit operator Field(Expression expression) => expression == null ? null : new Field(expression);

		public static implicit operator Field(PropertyInfo property) => property == null ? null : new Field(property);

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
				? other != null && _type == other._type && _comparisonValue.Equals(other._comparisonValue) && Boost.Equals(other.Boost)
				: other != null && _comparisonValue.Equals(other._comparisonValue) && Boost.Equals(other.Boost);
		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case string s: return this.Equals(s);
				case PropertyInfo p: return this.Equals(p);
				case Field f: return this.Equals(f);
				default: return false;
			}
		}

		public static bool operator ==(Field x, Field y) => Equals(x, y);

		public static bool operator !=(Field x, Field y)=> !Equals(x, y);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new ArgumentNullException(nameof(settings), $"Can not resolve {nameof(Field)} if no {nameof(IConnectionSettingsValues)} is provided");

			return nestSettings.Inferrer.Field(this);
		}

	}
}
