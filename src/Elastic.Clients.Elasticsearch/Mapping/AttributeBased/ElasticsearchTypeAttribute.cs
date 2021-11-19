// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using Elastic.Transport.Extensions;
using System.Xml.Linq;

namespace Elastic.Clients.Elasticsearch
{
	[AttributeUsage(AttributeTargets.Property)]
	public class IgnoreAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyNameAttribute : Attribute/*, IJsonProperty*/
	{
		public PropertyNameAttribute(string name) => Name = name;

		public string Name { get; set; }
		public int Order { get; } = -1;
		public bool Ignore { get; set; }
		public bool? AllowPrivate { get; set; } = true;
	}

	public enum FieldType
	{
	}

	/// <summary>
	/// A document field mapping in Elasticsearch
	/// </summary>
	public interface IFieldMapping { }

	public interface IProperty : IFieldMapping
	{
		/// <summary>
		/// Local property metadata that will not be stored in Elasticsearch with the mappings
		/// </summary>
		[IgnoreDataMember]
		IDictionary<string, object> LocalMetadata { get; set; }

		/// <summary>
		/// Metadata attached to the field. This metadata is stored in but opaque to Elasticsearch. It is
		/// only useful for multiple applications that work on the same indices to share
		/// meta information about fields such as units.
		///<para></para>
		/// Field metadata enforces at most 5 entries, that keys have a length that
		/// is less than or equal to 20, and that values are strings whose length is less
		///	than or equal to 50.
		/// </summary>
		[DataMember(Name = "meta")]
		IDictionary<string, string> Meta { get; set; }

		/// <summary>
		/// The name of the property
		/// </summary>
		[IgnoreDataMember]
		PropertyName Name { get; set; }

		/// <summary>
		/// The datatype of the property
		/// </summary>
		[DataMember(Name = "type")]
		string Type { get; set; }
	}

	/// <summary>
	/// A mapping for a property from a CLR type
	/// </summary>
	public interface IPropertyWithClrOrigin
	{
		/// <summary>
		/// The CLR property to which the mapping relates
		/// </summary>
		PropertyInfo ClrOrigin { get; set; }
	}

	/// <inheritdoc cref="IProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class PropertyBase : IProperty, IPropertyWithClrOrigin
	{
		protected PropertyBase(FieldType type) => ((IProperty)this).Type = type.GetStringValue();

		/// <inheritdoc />
		public IDictionary<string, object> LocalMetadata { get; set; }

		/// <inheritdoc />
		public IDictionary<string, string> Meta { get; set; }

		/// <inheritdoc />
		public PropertyName Name { get; set; }

		protected string DebugDisplay => $"Type: {((IProperty)this).Type ?? "<empty>"}, Name: {Name?.DebugDisplay ?? "<empty>"} ";

		public override string ToString() => DebugDisplay;

		/// <summary>
		/// Override for the property type, used for custom mappings
		/// </summary>
		protected string TypeOverride { get; set; }

		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }

		string IProperty.Type
		{
			get => TypeOverride;
			set => TypeOverride = value;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public abstract class ElasticsearchPropertyAttributeBase : Attribute, IProperty/*, IPropertyMapping, IJsonProperty*/
	{
		protected ElasticsearchPropertyAttributeBase(FieldType type) => Self.Type = type.GetStringValue();

		public bool? AllowPrivate { get; set; } = true;

		public bool Ignore { get; set; }

		public string Name { get; set; }

		public int Order { get; } = -2;

		IDictionary<string, object> IProperty.LocalMetadata { get; set; }

		IDictionary<string, string> IProperty.Meta { get; set; }

		PropertyName IProperty.Name { get; set; }
		private IProperty Self => this;

		string IProperty.Type { get; set; }

		public static ElasticsearchPropertyAttributeBase From(MemberInfo memberInfo) =>
			memberInfo.GetCustomAttribute<ElasticsearchPropertyAttributeBase>(true);
	}

	/// <summary>
	/// Applied to a CLR type to override the name of a CLR type and the property from which an _id is inferred
	/// when serializing an instance of the type.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ElasticsearchTypeAttribute : Attribute
	{
		private static readonly ConcurrentDictionary<Type, ElasticsearchTypeAttribute> CachedTypeLookups = new();

		/// <summary>
		/// The property on CLR type to use as the _id of the document
		/// </summary>
		public string IdProperty { get; set; }

		/// <summary>
		/// The name of the CLR type for serialization
		/// </summary>
		public string RelationName { get; set; }

		/// <inheritdoc cref="RelationName"/>
		[Obsolete("Deprecated. Please use " + nameof(RelationName))]
		public string Name
		{
			get => RelationName;
			set => RelationName = value;
		}

		/// <summary>
		/// Gets the first <see cref="ElasticsearchTypeAttribute"/> from a given CLR type
		/// </summary>
		public static ElasticsearchTypeAttribute From(Type type)
		{
			if (CachedTypeLookups.TryGetValue(type, out var attr))
				return attr;

			var attributes = type.GetCustomAttributes(typeof(ElasticsearchTypeAttribute), true);
			if (attributes.HasAny())
				attr = (ElasticsearchTypeAttribute)attributes.First();

			CachedTypeLookups.TryAdd(type, attr);
			return attr;
		}
	}
}
