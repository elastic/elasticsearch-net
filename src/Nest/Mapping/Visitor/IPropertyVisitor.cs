using System.Reflection;

namespace Nest
{
	public interface IPropertyVisitor
	{
		void Visit(IStringProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(ITextProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IKeywordProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IDateProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IAttachmentProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(ICompletionProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IIpProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IMurmur3HashProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(ITokenCountProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
	}
}
