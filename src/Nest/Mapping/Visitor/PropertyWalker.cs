using Nest.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nest
{
	public class PropertyWalker
	{
		private Type _type;
		private ITypeVisitor _visitor;

		public PropertyWalker(Type type) : this(type, null) { }

		public PropertyWalker(Type type, ITypeVisitor visitor)
		{
			_type = GetUnderlyingType(type);
			_visitor = visitor ?? new NoopTypeVisitor();
		}

		public Dictionary<FieldName, IElasticType> GetProperties()
		{
			var properties = new Dictionary<FieldName, IElasticType>();
			foreach(var propertyInfo in _type.GetProperties())
			{
				var attribute = ElasticPropertyAttribute.From(propertyInfo);
				if (attribute != null && attribute.Ignore)
					continue;
				var property = GetProperty(propertyInfo, attribute);
				properties.Add(propertyInfo.Name, property);
			}
			return properties;
		}

		private IElasticType GetProperty(PropertyInfo propertyInfo, ElasticPropertyAttribute attribute)
		{
			var elasticType = GetElasticType(propertyInfo, attribute);
			var objectType = elasticType as IObjectType;
			if (objectType != null)
			{
				var walker = new PropertyWalker(propertyInfo.PropertyType, _visitor);
				objectType.Properties = walker.GetProperties();
			}
			_visitor.Visit(elasticType, propertyInfo, attribute);
			return elasticType;	
		}

		private IElasticType GetElasticType(PropertyInfo propertyInfo, ElasticPropertyAttribute attribute)
		{
			var elasticType = _visitor.Visit(propertyInfo, attribute);
			if (elasticType != null)
				return elasticType;
			return (attribute != null) 
				? attribute.ToElasticType() 
				: InferElasticType(propertyInfo.PropertyType);
		}

		private IElasticType InferElasticType(Type type)
		{
			type = GetUnderlyingType(type);

			if (type == typeof(string))
				return new StringType();

			if (type.IsEnum)
				return new NumberType(NumberTypeName.Integer);

			if (type.IsValueType)
			{
				switch (type.Name)
				{
					case "Int32":
					case "UInt32":
						return new NumberType(NumberTypeName.Integer);
					case "Int16":
					case "UInt16":
						return new NumberType(NumberTypeName.Short);
					case "Byte":
					case "SByte":
						return new NumberType(NumberTypeName.Byte);
					case "Int64":
					case "UInt64":
						return new NumberType(NumberTypeName.Long);
					case "Single":
						return new NumberType(NumberTypeName.Float);
					case "Decimal":
					case "Double":
						return new NumberType(NumberTypeName.Double);
					case "DateTime":
						return new DateType();
					case "Boolean":
						return new BooleanType();
				}
			}
			
			return new ObjectType();
		}

		private Type GetUnderlyingType(Type type)
		{
			if (type.IsArray)
				return type.GetElementType();

			if (type.IsGenericType && type.GetGenericArguments().Length == 1 && (type.GetInterface("IEnumerable") != null || Nullable.GetUnderlyingType(type) != null))
				return type.GetGenericArguments()[0];

			return type;
		}
	}
}
