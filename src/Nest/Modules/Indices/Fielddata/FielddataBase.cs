// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IFielddata
	{
		[DataMember(Name ="filter")]
		IFielddataFilter Filter { get; set; }

		[DataMember(Name ="loading")]
		FielddataLoading? Loading { get; set; }
	}

	public abstract class FielddataBase : IFielddata
	{
		public IFielddataFilter Filter { get; set; }
		public FielddataLoading? Loading { get; set; }
	}

	public abstract class FielddataDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IFielddata
		where TDescriptor : FielddataDescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IFielddata
	{
		IFielddataFilter IFielddata.Filter { get; set; }
		FielddataLoading? IFielddata.Loading { get; set; }

		public TDescriptor Filter(Func<FielddataFilterDescriptor, IFielddataFilter> filterSelector) =>
			Assign(filterSelector(new FielddataFilterDescriptor()), (a, v) => a.Filter = v);

		public TDescriptor Loading(FielddataLoading? loading) => Assign(loading, (a, v) => a.Loading = v);
	}
}
