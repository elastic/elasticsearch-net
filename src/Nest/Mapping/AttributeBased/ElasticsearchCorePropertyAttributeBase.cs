// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[DataContract]
	public abstract class ElasticsearchCorePropertyAttributeBase : ElasticsearchPropertyAttributeBase, ICoreProperty
	{
		protected ElasticsearchCorePropertyAttributeBase(FieldType type) : base(type) { }

		/// <inheritdoc cref="ICoreProperty" />
		public string Similarity
		{
			set => Self.Similarity = value;
			get => Self.Similarity;
		}

		/// <inheritdoc cref="ICoreProperty" />
		public bool Store
		{
			get => Self.Store.GetValueOrDefault();
			set => Self.Store = value;
		}

		Fields ICoreProperty.CopyTo { get; set; }

		IProperties ICoreProperty.Fields { get; set; }

		private ICoreProperty Self => this;

		string ICoreProperty.Similarity { get; set; }

		bool? ICoreProperty.Store { get; set; }

		public static new ElasticsearchCorePropertyAttributeBase From(MemberInfo memberInfo) =>
			memberInfo.GetCustomAttribute<ElasticsearchCorePropertyAttributeBase>(true);
	}
}
