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

		public virtual void Visit(IAttachmentProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
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
		public virtual void Visit(IStringProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => null;

		public void Visit(IProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
			var nestedType = type as INestedProperty;
			if (nestedType != null)
				Visit(nestedType, propertyInfo, attribute);

			var objectType = type as IObjectProperty;
			if (objectType != null)
				Visit(objectType, propertyInfo, attribute);

			var binaryType = type as IBinaryProperty;
			if (binaryType != null)
				Visit(binaryType, propertyInfo, attribute);

			var booleanType = type as IBooleanProperty;
			if (booleanType != null)
				Visit(booleanType, propertyInfo, attribute);

			var dateType = type as IDateProperty;
			if (dateType != null)
				Visit(dateType, propertyInfo, attribute);

			var numberType = type as INumberProperty;
			if (numberType != null)
				Visit(numberType, propertyInfo, attribute);

			var textType = type as ITextProperty;
			if (textType != null)
				Visit(textType, propertyInfo, attribute);

			var keywordType = type as IKeywordProperty;
			if (keywordType != null)
				Visit(textType, propertyInfo, attribute);

			var attachmentType = type as IAttachmentProperty;
			if (attachmentType != null)
				Visit(attachmentType, propertyInfo, attribute);

			var geoShapeType = type as IGeoShapeProperty;
			if (geoShapeType != null)
				Visit(geoShapeType, propertyInfo, attribute);

			var geoPointType = type as IGeoPointProperty;
			if (geoPointType != null)
				Visit(geoPointType, propertyInfo, attribute);

			var completionType = type as ICompletionProperty;
			if (completionType != null)
				Visit(completionType, propertyInfo, attribute);

			var ipType = type as IIpProperty;
			if (ipType != null)
				Visit(ipType, propertyInfo, attribute);

			var murmurType = type as IMurmur3HashProperty;
			if (murmurType != null)
				Visit(murmurType, propertyInfo, attribute);

			var tokenCountType = type as ITokenCountProperty;
			if (tokenCountType != null)
				Visit(tokenCountType, propertyInfo, attribute);
		}
	}
}
