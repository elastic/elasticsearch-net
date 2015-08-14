using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nest
{
	public class NoopTypeVisitor : ITypeVisitor
	{
		public virtual void Visit(IBooleanType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IBinaryType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IObjectType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IGeoShapeType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(ICompletionType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IMurmur3HashType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(ITokenCountType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IIpType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IAttachmentType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IGeoPointType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(INestedType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IDateType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(INumberType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual void Visit(IStringType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
		}

		public virtual IElasticType Visit(PropertyInfo propertyInfo, IElasticPropertyAttribute attribute) => null;

		public void Visit(IElasticType type, PropertyInfo propertyInfo, IElasticPropertyAttribute attribute)
		{
			var nestedType = type as INestedType;
			if (nestedType != null)
				Visit(nestedType, propertyInfo, attribute);

			var objectType = type as IObjectType;
			if (objectType != null)
				Visit(objectType, propertyInfo, attribute);

			var binaryType = type as IBinaryType;
			if (binaryType != null)
				Visit(binaryType, propertyInfo, attribute);

			var booleanType = type as IBooleanType;
			if (booleanType != null)
				Visit(booleanType, propertyInfo, attribute);

			var dateType = type as IDateType;
			if (dateType != null)
				Visit(dateType, propertyInfo, attribute);

			var numberType = type as INumberType;
			if (numberType != null)
				Visit(numberType, propertyInfo, attribute);

			var stringType = type as IStringType;
			if (stringType != null)
				Visit(stringType, propertyInfo, attribute);

			var attachmentType = type as IAttachmentType;
			if (attachmentType != null)
				Visit(attachmentType, propertyInfo, attribute);

			var geoShapeType = type as IGeoShapeType;
			if (geoShapeType != null)
				Visit(geoShapeType, propertyInfo, attribute);

			var geoPointType = type as IGeoPointType;
			if (geoPointType != null)
				Visit(geoPointType, propertyInfo, attribute);

			var completionType = type as ICompletionType;
			if (completionType != null)
				Visit(completionType, propertyInfo, attribute);

			var ipType = type as IIpType;
			if (ipType != null)
				Visit(ipType, propertyInfo, attribute);

			var murmurType = type as IMurmur3HashType;
			if (murmurType != null)
				Visit(murmurType, propertyInfo, attribute);

			var tokenCountType = type as ITokenCountType;
			if (tokenCountType != null)
				Visit(tokenCountType, propertyInfo, attribute);
		}
	}
}
