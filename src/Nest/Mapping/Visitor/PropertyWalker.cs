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
		private IPropertyVisitor _visitor;

		public PropertyWalker(Type type) : this(type, null) { }

		public PropertyWalker(Type type, IPropertyVisitor visitor)
		{
			_type = GetUnderlyingType(type);
			_visitor = visitor ?? new NoopPropertyVisitor();
		}

		public Dictionary<FieldName, IElasticsearchProperty> GetProperties()
		{
			var properties = new Dictionary<FieldName, IElasticsearchProperty>();
			foreach(var propertyInfo in _type.GetProperties())
			{
				var attribute = ElasticsearchPropertyAttribute.From(propertyInfo);
				if (attribute != null && attribute.Ignore)
					continue;
				var property = GetProperty(propertyInfo, attribute);
				properties.Add(propertyInfo.Name, property);
			}
			return properties;
		}

		private IElasticsearchProperty GetProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute)
		{
			var elasticType = GetElasticType(propertyInfo, attribute);
			var objectType = elasticType as IObjectProperty;
			if (objectType != null)
			{
				var walker = new PropertyWalker(propertyInfo.PropertyType, _visitor);
				objectType.Properties = walker.GetProperties();
			}
			_visitor.Visit(elasticType, propertyInfo, attribute);
			return elasticType;	
		}

		private IElasticsearchProperty GetElasticType(PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute)
		{
			var elasticType = _visitor.Visit(propertyInfo, attribute);
			if (elasticType != null)
				return elasticType;
			return (attribute != null) 
				? attribute.ToProperty() 
				: InferElasticType(propertyInfo.PropertyType);
		}

		private IElasticsearchProperty InferElasticType(Type type)
		{
			type = GetUnderlyingType(type);

			if (type == typeof(string))
				return new StringProperty();

			if (type.IsEnum)
				return new NumberProperty(NumberType.Integer);

			if (type.IsValueType)
			{
				switch (type.Name)
				{
					case "Int32":
					case "UInt32":
						return new NumberProperty(NumberType.Integer);
					case "Int16":
					case "UInt16":
						return new NumberProperty(NumberType.Short);
					case "Byte":
					case "SByte":
						return new NumberProperty(NumberType.Byte);
					case "Int64":
					case "UInt64":
						return new NumberProperty(NumberType.Long);
					case "Single":
						return new NumberProperty(NumberType.Float);
					case "Decimal":
					case "Double":
						return new NumberProperty(NumberType.Double);
					case "DateTime":
						return new DateProperty();
					case "Boolean":
						return new BooleanProperty();
				}
			}
			
			return new ObjectProperty();
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
