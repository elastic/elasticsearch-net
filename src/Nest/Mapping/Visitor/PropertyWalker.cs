using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Nest
{
	public class PropertyWalker
	{
		private readonly Type _type;
		private readonly IPropertyVisitor _visitor;
		private readonly int _maxRecursion;
		private readonly ConcurrentDictionary<Type, int> _seenTypes;

		public PropertyWalker(Type type, IPropertyVisitor visitor, int maxRecursion = 0)
		{
			_type = GetUnderlyingType(type);
			_visitor = visitor ?? new NoopPropertyVisitor();
			_maxRecursion = maxRecursion;
			_seenTypes = new ConcurrentDictionary<Type, int>();
			_seenTypes.TryAdd(_type, 0);
		}

		private PropertyWalker(Type type, IPropertyVisitor visitor, int maxRecursion, ConcurrentDictionary<Type, int> seenTypes)
		{
			_type = type;
			_visitor = visitor;
			_maxRecursion = maxRecursion;
			_seenTypes = seenTypes;
		}

		public IProperties GetProperties(ConcurrentDictionary<Type, int> seenTypes = null, int maxRecursion = 0)
		{
			var properties = new Properties();

			int seen;
			if (seenTypes != null && seenTypes.TryGetValue(_type, out seen) && seen > maxRecursion)
				return properties;

			foreach (var propertyInfo in _type.AllPropertiesCached())
			{
				var attribute = ElasticsearchPropertyAttributeBase.From(propertyInfo);
				if (attribute != null && attribute.Ignore)
					continue;

				var property = GetProperty(propertyInfo, attribute);
				var withCLrOrigin = property as IPropertyWithClrOrigin;
				if (withCLrOrigin != null)
					withCLrOrigin.ClrOrigin = propertyInfo;
				properties.Add(propertyInfo, property);
			}

			return properties;
		}

		private IProperty GetProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
			var property = _visitor.Visit(propertyInfo, attribute);
			if (property != null)
				return property;

			if (propertyInfo.GetGetMethod().IsStatic)
				return null;

			property = attribute ?? InferProperty(propertyInfo.PropertyType);

			var objectProperty = property as IObjectProperty;
			if (objectProperty != null)
			{
				var type = GetUnderlyingType(propertyInfo.PropertyType);
				var seenTypes = new ConcurrentDictionary<Type, int>(_seenTypes);
				seenTypes.AddOrUpdate(type, 0, (t, i) => ++i);
				var walker = new PropertyWalker(type, _visitor, _maxRecursion, seenTypes);
				objectProperty.Properties = walker.GetProperties(seenTypes, _maxRecursion);
			}

			_visitor.Visit(property, propertyInfo, attribute);

			return property;
		}

		private IProperty InferProperty(Type type)
		{
			type = GetUnderlyingType(type);

			if (type == typeof(string))
				return new TextProperty
				{
					Fields = new Properties
					{
						{ "keyword", new KeywordProperty
							{
								IgnoreAbove = 256
							}
						}
					}
				};

			if (type.IsEnumType())
				return new NumberProperty(NumberType.Integer);

			if (type.IsValue())
			{
				switch (type.Name)
				{
					case "Int32":
					case "UInt16":
						return new NumberProperty(NumberType.Integer);
					case "Int16":
					case "Byte":
						return new NumberProperty(NumberType.Short);
					case "SByte":
						return new NumberProperty(NumberType.Byte);
					case "Int64":
					case "UInt32":
					case "TimeSpan":
						return new NumberProperty(NumberType.Long);
					case "Single":
						return new NumberProperty(NumberType.Float);
					case "Decimal":
					case "Double":
					case "UInt64":
						return new NumberProperty(NumberType.Double);
					case "DateTime":
					case "DateTimeOffset":
						return new DateProperty();
					case "Boolean":
						return new BooleanProperty();
					case "Char":
					case "Guid":
						return new KeywordProperty();
				}
			}

			if (type == typeof(GeoLocation))
				return new GeoPointProperty();

			if (type == typeof(CompletionField))
				return new CompletionProperty();

			if (type == typeof(DateRange))
				return new DateRangeProperty();

			if (type == typeof(DoubleRange))
				return new DoubleRangeProperty();

			if (type == typeof(FloatRange))
				return new FloatRangeProperty();

			if (type == typeof(IntegerRange))
				return new IntegerRangeProperty();

			if (type == typeof(LongRange))
				return new LongRangeProperty();

			return new ObjectProperty();
		}

		private static Type GetUnderlyingType(Type type)
		{
			if (type.IsArray)
				return type.GetElementType();

			var typeInfo = type.GetTypeInfo();
			if (typeInfo.IsGenericType && type.GetGenericArguments().Length == 1
				&& (typeInfo.ImplementedInterfaces.HasAny(t => t == typeof(IEnumerable)) || Nullable.GetUnderlyingType(type) != null))
				return type.GetGenericArguments()[0];

			return type;
		}
	}
}
