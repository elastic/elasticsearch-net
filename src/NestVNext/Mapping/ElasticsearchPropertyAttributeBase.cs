//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Runtime.Serialization;
//using System.Text;

//namespace Nest.Mapping
//{
//	public abstract class ElasticsearchPropertyAttributeBase : Attribute, IProperty, IPropertyMapping
//	{
//		protected ElasticsearchPropertyAttributeBase(FieldType type) => Self.Type = type.GetStringValue();

//		public bool? AllowPrivate { get; set; } = true;

//		public bool Ignore { get; set; }

//		public string Name { get; set; }

//		public int Order { get; } = -2;

//		IDictionary<string, object> IProperty.LocalMetadata { get; set; }

//		IDictionary<string, string> IProperty.Meta { get; set; }

//		PropertyName IProperty.Name { get; set; }
//		private IProperty Self => this;
//		string IProperty.Type { get; set; }

//		public static ElasticsearchPropertyAttributeBase From(MemberInfo memberInfo) =>
//			memberInfo.GetCustomAttribute<ElasticsearchPropertyAttributeBase>(true);
//	}

//	/// <summary>
//	/// A document field mapping in Elasticsearch
//	/// </summary>
//	public interface IFieldMapping { }

//	public interface IProperty : IFieldMapping
//	{
//		/// <summary>
//		/// Local property metadata that will not be stored in Elasticsearch with the mappings
//		/// </summary>
//		[IgnoreDataMember]
//		IDictionary<string, object> LocalMetadata { get; set; }

//		/// <summary>
//		/// Metadata attached to the field. This metadata is stored in but opaque to Elasticsearch. It is
//		/// only useful for multiple applications that work on the same indices to share
//		/// meta information about fields such as units.
//		///<para></para>
//		/// Field metadata enforces at most 5 entries, that keys have a length that
//		/// is less than or equal to 20, and that values are strings whose length is less
//		///	than or equal to 50.
//		/// </summary>
//		[DataMember(Name = "meta")]
//		IDictionary<string, string> Meta { get; set; }

//		/// <summary>
//		/// The name of the property
//		/// </summary>
//		[IgnoreDataMember]
//		PropertyName Name { get; set; }

//		/// <summary>
//		/// The datatype of the property
//		/// </summary>
//		[DataMember(Name = "type")]
//		string Type { get; set; }
//	}
//}
