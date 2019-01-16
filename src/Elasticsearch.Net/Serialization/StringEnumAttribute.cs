using System;

namespace Elasticsearch.Net
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum)]
	public class StringEnumAttribute : Attribute { }
}
