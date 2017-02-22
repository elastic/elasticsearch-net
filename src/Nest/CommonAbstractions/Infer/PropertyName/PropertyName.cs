using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest_5_2_0
{
	[ContractJsonConverter(typeof(PropertyNameJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class PropertyName : IEquatable<PropertyName>, IUrlParameter
	{
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
			Type type;
			Expression = expression;
			CacheableExpression = !new HasVariableExpressionVisitor(expression).Found;
			_comparisonValue = expression.ComparisonValueFromExpression(out type);
			_type = type;
		}

		public PropertyName(PropertyInfo property)
		{
			Property = property;
			_comparisonValue = property;
			_type = property.DeclaringType;
		}

		public static implicit operator PropertyName(string name)
		{
			return name == null ? null : new PropertyName(name);
		}

		public static implicit operator PropertyName(Expression expression)
		{
			return expression == null ? null : new PropertyName(expression);
		}

		public static implicit operator PropertyName(PropertyInfo property)
		{
			return property == null ? null : new PropertyName(property);
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

		public bool Equals(PropertyName other)
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
			return this.Equals(obj as PropertyName);
		}

		public static bool operator ==(PropertyName x, PropertyName y)
		{
			return Equals(x, y);
		}

		public static bool operator !=(PropertyName x, PropertyName y)
		{
			return !(x == y);
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass field name on querysting but it could not be resolved because no nest settings are available");
			var infer = new Inferrer(nestSettings);
			return infer.PropertyName(this);
		}
	}
}
