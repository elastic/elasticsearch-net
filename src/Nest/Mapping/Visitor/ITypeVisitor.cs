using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nest
{
	public interface ITypeVisitor 
	{
		void Visit(IStringProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IDateProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IAttachmentProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(ICompletionProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IIpProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IMurmur3HashProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(ITokenCountProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);

		void Visit(IElasticsearchProperty type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);

		IElasticsearchProperty Visit(PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
	}
}
