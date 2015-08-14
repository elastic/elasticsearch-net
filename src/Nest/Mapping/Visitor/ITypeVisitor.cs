using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nest
{
	public interface ITypeVisitor 
	{
		void Visit(IStringType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(INumberType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IBooleanType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IDateType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IBinaryType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(INestedType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IObjectType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IGeoPointType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IGeoShapeType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IAttachmentType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(ICompletionType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IIpType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(IMurmur3HashType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
		void Visit(ITokenCountType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);

		void Visit(IElasticType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);

		IElasticType Visit(PropertyInfo propertyInfo, IElasticPropertyAttribute attribute);
	}
}
