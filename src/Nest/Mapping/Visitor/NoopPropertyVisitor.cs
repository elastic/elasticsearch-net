using System.Reflection;

namespace Nest
{
	public class NoopPropertyVisitor : IPropertyVisitor
	{
		public virtual void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(ICompletionProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IMurmur3HashProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(ITokenCountProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IIpProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IDateProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(ITextProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IKeywordProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => null;

		public virtual bool SkipProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => false;

		public void Visit(IProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
			if (type is INestedProperty nestedType)
				Visit(nestedType, propertyInfo, attribute);

			if (type is IObjectProperty objectType)
				Visit(objectType, propertyInfo, attribute);

			if (type is IBinaryProperty binaryType)
				Visit(binaryType, propertyInfo, attribute);

			if (type is IBooleanProperty booleanType)
				Visit(booleanType, propertyInfo, attribute);

			if (type is IDateProperty dateType)
				Visit(dateType, propertyInfo, attribute);

			if (type is INumberProperty numberType)
				Visit(numberType, propertyInfo, attribute);

			if (type is ITextProperty textType)
				Visit(textType, propertyInfo, attribute);

			if (type is IKeywordProperty keywordType)
				Visit(keywordType, propertyInfo, attribute);

			if (type is IGeoShapeProperty geoShapeType)
				Visit(geoShapeType, propertyInfo, attribute);

			if (type is IGeoPointProperty geoPointType)
				Visit(geoPointType, propertyInfo, attribute);

			if (type is ICompletionProperty completionType)
				Visit(completionType, propertyInfo, attribute);

			if (type is IIpProperty ipType)
				Visit(ipType, propertyInfo, attribute);

			if (type is IMurmur3HashProperty murmurType)
				Visit(murmurType, propertyInfo, attribute);

			if (type is ITokenCountProperty tokenCountType)
				Visit(tokenCountType, propertyInfo, attribute);
		}
	}
}
