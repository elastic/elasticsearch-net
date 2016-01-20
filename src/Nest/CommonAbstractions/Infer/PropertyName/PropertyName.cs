using System;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(PropertyNameJsonConverter))]
	public class PropertyName : IEquatable<PropertyName>, IUrlParameter
	{
		public string Name { get; set; }
		public Expression Expression { get; set; }
		public PropertyInfo Property { get; set; }

		private object ComparisonValue;

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
				ComparisonValue = property
			};
		}

		public override int GetHashCode()
		{
			return ComparisonValue?.GetHashCode() ?? 0;	
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

			return ComparisonValue.Equals(other.ComparisonValue);
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
