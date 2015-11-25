using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nest 
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class ElasticsearchPropertyAttribute : Attribute, IProperty, IPropertyMapping
	{
		IProperty Self => this;

		PropertyName IProperty.Name { get; set; }
		TypeName IProperty.Type { get; set; }
		string IProperty.IndexName { get; set; }
		bool? IProperty.Store { get; set; }
		bool? IProperty.DocValues { get; set; }
		IProperties IProperty.Fields { get; set; }
		SimilarityOption? IProperty.Similarity { get; set; }
		Fields IProperty.CopyTo { get; set; }

		public string Name { get; set; }
		public bool Ignore { get; set; }
		public bool DocValues { get { return Self.DocValues.GetValueOrDefault(); } set { Self.DocValues = value; } }
		public string IndexName { get { return Self.IndexName; } set { Self.IndexName = value; } }
		public SimilarityOption Similarity { get { return Self.Similarity.GetValueOrDefault(); } set { Self.Similarity = value; } }
		public bool Store { get { return Self.Store.GetValueOrDefault(); } set { Self.Store = value; } }

		public ElasticsearchPropertyAttribute(TypeName type)
		{
			Self.Type = type;
		}

		public static ElasticsearchPropertyAttribute From(MemberInfo memberInfo)
		{
			var attributes = memberInfo.GetCustomAttributes(typeof(ElasticsearchPropertyAttribute), true);
			if (attributes.HasAny())
				return ((ElasticsearchPropertyAttribute)attributes.First());
			return null;
		}
	}
}