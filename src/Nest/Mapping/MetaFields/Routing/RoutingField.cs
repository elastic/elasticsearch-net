// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(RoutingField))]
	public interface IRoutingField : IFieldMapping
	{
		[DataMember(Name ="required")]
		bool? Required { get; set; }
	}

	public class RoutingField : IRoutingField
	{
		public bool? Required { get; set; }
	}

	public class RoutingFieldDescriptor<T>
		: DescriptorBase<RoutingFieldDescriptor<T>, IRoutingField>, IRoutingField
	{
		bool? IRoutingField.Required { get; set; }

		public RoutingFieldDescriptor<T> Required(bool? required = true) => Assign(required, (a, v) => a.Required = v);
	}
}
