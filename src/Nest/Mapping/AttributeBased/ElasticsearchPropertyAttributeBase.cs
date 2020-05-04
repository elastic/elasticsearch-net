// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[DataContract]
	public abstract class ElasticsearchPropertyAttributeBase : Attribute, IProperty, IPropertyMapping, IJsonProperty
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
}
