using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nest
{
	public interface IPropertyVisitor 
	{
		void Visit(IStringProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IDateProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IAttachmentProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(ICompletionProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IIpProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(IMurmur3HashProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
		void Visit(ITokenCountProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);

		void Visit(IElasticsearchProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);

		IElasticsearchProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttribute attribute);
	}
}
