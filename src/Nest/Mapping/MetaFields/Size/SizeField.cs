// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(SizeField))]
	public interface ISizeField : IFieldMapping
	{
		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }
	}

	public class SizeField : ISizeField
	{
		public bool? Enabled { get; set; }
	}

	public class SizeFieldDescriptor
		: DescriptorBase<SizeFieldDescriptor, ISizeField>, ISizeField
	{
		bool? ISizeField.Enabled { get; set; }

		public SizeFieldDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);
	}
}
