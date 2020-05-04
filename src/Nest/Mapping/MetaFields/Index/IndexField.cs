// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;

namespace Nest
{
	[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
	[ReadAs(typeof(IndexField))]
	public interface IIndexField : IFieldMapping
	{
		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }
	}

	[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
	public class IndexField : IIndexField
	{
		public bool? Enabled { get; set; }
	}

	[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
	public class IndexFieldDescriptor
		: DescriptorBase<IndexFieldDescriptor, IIndexField>, IIndexField
	{
		bool? IIndexField.Enabled { get; set; }

		public IndexFieldDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);
	}
}
