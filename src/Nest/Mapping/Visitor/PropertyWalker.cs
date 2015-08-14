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
				var attribute = ElasticAttributes.Property(propertyInfo);
				if (attribute != null && attribute.OptOut)
					continue;
				var property = GetProperty(propertyInfo, attribute);
				properties.Add(propertyInfo.Name, property);
			}
			return properties;
		}

		private IElasticType GetProperty(PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
			var elasticType = GetElasticType(propertyInfo, attribute);
			var objectType = elasticType as IObjectType;
			if (objectType != null)
			{
				var walker = new PropertyWalker(propertyInfo.PropertyType, _visitor);
				objectType.Properties = walker.GetProperties();
			}
			FillAttributeValues(elasticType, attribute);
			_visitor.Visit(elasticType, propertyInfo, attribute);
			return elasticType;	
		}

		private void FillAttributeValues(IElasticType type, IElasticPropertyAttribute attribute)
		{
			// TODO
		}

		private IElasticType GetElasticType(PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
			var elasticType = _visitor.Visit(propertyInfo, attribute);
			if (elasticType != null)
				return elasticType;
			return (attribute != null) 
				? GetElasticType(attribute) 
				: GetElasticType(propertyInfo.PropertyType);
		}

		private IElasticType GetElasticType(IElasticPropertyAttribute attribute)
		{
			switch(attribute.Type)
			{
				case FieldType.Attachment: return new AttachmentType();
				case FieldType.Binary: return new BinaryType();
				case FieldType.Boolean: return new BooleanType();
				case FieldType.Completion: return new CompletionType();
				case FieldType.Date: return new DateType();
				case FieldType.GeoPoint: return new GeoPointType();
				case FieldType.GeoShape: return new GeoShapeType();
				case FieldType.Ip: return new IpType();
				case FieldType.Nested: return new NestedType();
				case FieldType.Object: return new ObjectType();
				case FieldType.String: return new StringType();
				case FieldType.Byte: return new NumberType(NumberTypeName.Byte);
				case FieldType.Double: return new NumberType(NumberTypeName.Double);
				case FieldType.Float: return new NumberType(NumberTypeName.Float);
				case FieldType.Integer: return new NumberType(NumberTypeName.Integer);
				case FieldType.Long: return new NumberType(NumberTypeName.Long);
				case FieldType.Short: return new NumberType(NumberTypeName.Short);
				case FieldType.TokenCount: return new TokenCountType();
				case FieldType.Murmur3Hash: return new Murmur3HashType();
				default: throw new DslException(string.Format("A mapping from FieldType: {0} to IElasticType doesn't exist.", attribute.Type));
			}
		}

		private IElasticType GetElasticType(Type type)
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
