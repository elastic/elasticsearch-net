using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

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

        protected ElasticsearchPropertyAttribute(string typeName)
        {
            Self.Type = typeName;
        }

		protected ElasticsearchPropertyAttribute(Type type)
		{
			Self.Type = type;
		}

		public static ElasticsearchPropertyAttribute From(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttribute<ElasticsearchPropertyAttribute>(true);
		}
	}
}