/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
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
