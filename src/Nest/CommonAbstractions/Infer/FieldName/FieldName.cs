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
	public class FieldName : IEquatable<FieldName>, IUrlParameter
	{
		public string Name { get; set; }
		public Expression Expression { get; set; }
		public PropertyInfo Property { get; set; }
		public double? Boost { get; set; }

		private string ComparisonValue;

		public static FieldName Create(string name, double? boost = null)
		{
			FieldName fieldName = name;
			fieldName.Boost = boost;
			return fieldName;
		}

		public static FieldName Create(Expression expression, double? boost = null)
		{
			FieldName fieldName = expression;
			fieldName.Boost = boost;
			return fieldName;
		}

		public static implicit operator FieldName(string name)
		{
			return name == null ? null : new FieldName
			{
				Name = name,
				ComparisonValue = name
			};
		}

		public static implicit operator FieldName(Expression expression)
		{
			return expression == null ? null : new FieldName
			{
				Expression = expression,
				ComparisonValue = ((expression as LambdaExpression).Body as MemberExpression).Member.Name
			};
		}

		public static implicit operator FieldName(PropertyInfo property)
		{
			return property == null ? null : new FieldName
			{
				Property = property,
				ComparisonValue = property.Name
			};
		}

		public override int GetHashCode()
		{
			return (ComparisonValue != null) ? ComparisonValue.GetHashCode() : 0;	
		}

		bool IEquatable<FieldName>.Equals(FieldName other)
		{
			return Equals(other);
		}

		public override bool Equals(object obj)
		{
			var other = obj as FieldName;
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
			return infer.FieldName(this);
		}
	}
}
