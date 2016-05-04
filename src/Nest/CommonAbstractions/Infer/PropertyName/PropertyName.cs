using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(PropertyNameJsonConverter))]
	public class PropertyName : IEquatable<PropertyName>, IUrlParameter
	{
		private object _comparisonValue;
		private Type _type;

		public string Name { get; set; }
		public Expression Expression { get; set; }
		public PropertyInfo Property { get; set; }
		public bool CacheableExpression { get; private set; }

		public static implicit operator PropertyName(string name)
		{
			return name == null ? null : new PropertyName
			{
				Name = name,
				_comparisonValue = name
			};
		}

		public static implicit operator PropertyName(Expression expression)
		{
			if (expression == null) return null;

			Type type;
			return new PropertyName
			{
				Expression = expression,
				CacheableExpression = !new HasConstantExpressionVisitor(expression).Found,
				_comparisonValue = expression.ComparisonValueFromExpression(out type),
				_type = type
			};
		}

		public static implicit operator PropertyName(PropertyInfo property)
		{
			return property == null ? null : new PropertyName
			{
				Property = property,
				_comparisonValue = property
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

		bool IEquatable<PropertyName>.Equals(PropertyName other)
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
			return ((IEquatable<PropertyName>)this).Equals(obj as PropertyName);
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
				throw new Exception("Tried to pass field name on querystring but it could not be resolved because no nest settings are available");
			var infer = new Inferrer(nestSettings);
			return infer.PropertyName(this);
		}
	}
}
