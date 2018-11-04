using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(PropertyNameJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class PropertyName : IEquatable<PropertyName>, IUrlParameter
	{
		private readonly object _comparisonValue;
		private readonly Type _type;

		public PropertyName(string name)
		{
			Name = name;
			_comparisonValue = name;
		}

		public PropertyName(Expression expression)
		{
			Expression = expression;
			CacheableExpression = !new HasVariableExpressionVisitor(expression).Found;
			_comparisonValue = expression.ComparisonValueFromExpression(out var type);
			_type = type;
		}

		public PropertyName(PropertyInfo property)
		{
			Property = property;
			_comparisonValue = property;
			_type = property.DeclaringType;
		}

		public bool CacheableExpression { get; }
		public Expression Expression { get; }
		public string Name { get; }
		public PropertyInfo Property { get; }

		internal string DebugDisplay =>
			$"{Expression?.ToString() ?? PropertyDebug ?? Name}{(_type == null ? "" : " typeof: " + _type.Name)}";

		private string PropertyDebug => Property == null ? null : $"PropertyInfo: {Property.Name}";

		public bool Equals(PropertyName other) => _type != null
			? other != null && _type == other._type && _comparisonValue.Equals(other._comparisonValue)
			: other != null && _comparisonValue.Equals(other._comparisonValue);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new Exception($"Tried to get the string representation of {nameof(PropertyName)}:'{DebugDisplay}' " +
					"but it could not be resolved because no connection settings are available");

			return nestSettings.Inferrer.PropertyName(this);
		}

		public static implicit operator PropertyName(string name) => name == null ? null : new PropertyName(name);

		public static implicit operator PropertyName(Expression expression) => expression == null ? null : new PropertyName(expression);

		public static implicit operator PropertyName(PropertyInfo property) => property == null ? null : new PropertyName(property);

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _comparisonValue?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (_type?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals(obj as PropertyName);
		}

		public static bool operator ==(PropertyName x, PropertyName y) => Equals(x, y);

		public static bool operator !=(PropertyName x, PropertyName y) => !(x == y);
	}
}
