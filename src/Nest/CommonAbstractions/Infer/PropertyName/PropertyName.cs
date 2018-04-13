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
		private static int TypeHashCode { get; } = typeof(PropertyName).GetHashCode();

		public string Name { get; }
		public Expression Expression { get; }
		public PropertyInfo Property { get; }
		public bool CacheableExpression { get; }

		private readonly object _comparisonValue;
		private readonly Type _type;

		internal string DebugDisplay =>
			$"{Expression?.ToString() ?? PropertyDebug ?? Name}{(_type == null ? "" : " typeof: " + _type.Name)}";

		private string PropertyDebug => Property == null ? null : $"PropertyInfo: {Property.Name}";

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

		public static implicit operator PropertyName(string name) => name.IsNullOrEmpty() ? null : new PropertyName(name);

		public static implicit operator PropertyName(Expression expression) => expression == null ? null : new PropertyName(expression);

		public static implicit operator PropertyName(PropertyInfo property) => property == null ? null : new PropertyName(property);

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (_comparisonValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (_type?.GetHashCode() ?? 0);
				return result;
			}
		}

		public bool Equals(PropertyName other) => EqualsMarker(other);

		public override bool Equals(object obj) =>
			obj is string s ? this.EqualsString(s) : (obj is PropertyName r) && this.EqualsMarker(r);

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == this.Name;

		public bool EqualsMarker(PropertyName other)
		{
			return _type != null
				? other != null && _type == other._type && _comparisonValue.Equals(other._comparisonValue)
				: other != null && _comparisonValue.Equals(other._comparisonValue);
		}

		public static bool operator ==(PropertyName left, PropertyName right) => Equals(left, right);

		public static bool operator !=(PropertyName left, PropertyName right) => !Equals(left, right);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new ArgumentNullException(nameof(settings), $"Can not resolve {nameof(PropertyName)} if no {nameof(IConnectionSettingsValues)} is provided");

			return nestSettings.Inferrer.PropertyName(this);
		}
	}
}
