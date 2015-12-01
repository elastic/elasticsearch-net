using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Elasticsearch.Net.Connection;
using System.Reflection;

namespace Nest
{
	[ContractJsonConverter(typeof(PropertyNameJsonConverter))]
	public class PropertyName : IEquatable<PropertyName>, IUrlParameter
	{
		public string Name { get; set; }
		public Expression Expression { get; set; }
		public PropertyInfo Property { get; set; }

		private string ComparisonValue;

		public static PropertyName Create(string name, double? boost = null)
		{
			PropertyName propertyName = name;
			return propertyName;
		}

		public static PropertyName Create(Expression expression, double? boost = null)
		{
			PropertyName propertyName = expression;
			return propertyName;
		}

		public static implicit operator PropertyName(string name)
		{
			return name == null ? null : new PropertyName
			{
				Name = name,
				ComparisonValue = name
			};
		}

		public static implicit operator PropertyName(Expression expression)
		{
			return expression == null ? null : new PropertyName
			{
				Expression = expression,
				ComparisonValue = ((expression as LambdaExpression)?.Body as MemberExpression)?.Member.Name ?? expression.ToString()
			};
		}

		public static implicit operator PropertyName(PropertyInfo property)
		{
			return property == null ? null : new PropertyName
			{
				Property = property,
				ComparisonValue = property.Name
			};
		}

		public override int GetHashCode()
		{
			return (ComparisonValue != null) ? ComparisonValue.GetHashCode() : 0;	
		}

		bool IEquatable<PropertyName>.Equals(PropertyName other)
		{
			return Equals(other);
		}

		public override bool Equals(object obj)
		{
			var other = obj as PropertyName;
			if (other == null)
				return false;
			return ComparisonValue == other.ComparisonValue;
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass field name on querysting but it could not be resolved because no nest settings are available");
			var infer = new ElasticInferrer(nestSettings);
			return infer.PropertyName(this);
		}
	}
}
