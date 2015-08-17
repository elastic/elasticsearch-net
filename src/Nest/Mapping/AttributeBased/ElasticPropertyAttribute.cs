using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nest 
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public abstract class ElasticPropertyAttribute : Attribute, IPropertyMapping
	{
		public bool DocValues { get; set; }
		public string IndexName { get; set; }
		public string Name { get; set; }
		public bool Ignore { get; set; }
		public SimilarityOption? Similarity { get; set; }
		public bool Store { get; set; }

		public abstract IElasticType ToElasticType();

		public static ElasticPropertyAttribute From(MemberInfo memberInfo)
		{
			var attributes = memberInfo.GetCustomAttributes(typeof(ElasticPropertyAttribute), true);
			if (attributes.HasAny())
				return ((ElasticPropertyAttribute)attributes.First());
			return null;
		}
	}
}