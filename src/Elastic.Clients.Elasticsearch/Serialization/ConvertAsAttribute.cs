using System;

namespace Elastic.Clients.Elasticsearch
{
	[AttributeUsage(AttributeTargets.Interface)]
	internal class ConvertAsAttribute : Attribute
	{
		public ConvertAsAttribute(Type convertType) => ConvertType = convertType;

		public Type ConvertType { get; }
	}

	[AttributeUsage(AttributeTargets.Interface)]
	internal class FieldNameQueryAttribute : Attribute
	{
		public FieldNameQueryAttribute(Type convertType) => ConvertType = convertType;

		public Type ConvertType { get; }
	}
}
