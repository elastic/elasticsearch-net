using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Interface)]
	internal class ConvertAsAttribute : Attribute
	{
		public ConvertAsAttribute(Type convertType) => ConvertType = convertType;

		public Type ConvertType { get; }
	}
}