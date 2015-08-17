using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nest
{
	public interface ITypeVisitor 
	{
		void Visit(IStringType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(INumberType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IBooleanType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IDateType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IBinaryType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(INestedType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IObjectType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IGeoPointType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IGeoShapeType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IAttachmentType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(ICompletionType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IIpType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(IMurmur3HashType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
		void Visit(ITokenCountType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);

		void Visit(IElasticType type, PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);

		IElasticType Visit(PropertyInfo propertyInfo, ElasticPropertyAttribute attribute);
	}
}
