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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(IdsQueryDescriptor))]
	public interface IIdsQuery : IQuery
	{
		[DataMember(Name = "values")]
		IEnumerable<Id> Values { get; set; }
	}

	public class IdsQuery : QueryBase, IIdsQuery
	{
		public IEnumerable<Id> Values { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Ids = this;

		internal static bool IsConditionless(IIdsQuery q) => !q.Values.HasAny();
	}

	public class IdsQueryDescriptor
		: QueryDescriptorBase<IdsQueryDescriptor, IIdsQuery>
			, IIdsQuery
	{
		protected override bool Conditionless => IdsQuery.IsConditionless(this);
		IEnumerable<Id> IIdsQuery.Values { get; set; }

		public IdsQueryDescriptor Values(params Id[] values) => Assign(values, (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<Id> values) => Values(values?.ToArray());

		// TODO 8.x remove params on Values already implicitly converting to Id
		public IdsQueryDescriptor Values(params string[] values) => Assign(values?.Select(v => (Id)v), (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<string> values) => Values(values.ToArray());

		public IdsQueryDescriptor Values(params long[] values) => Assign(values?.Select(v => (Id)v), (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<long> values) => Values(values.ToArray());

		public IdsQueryDescriptor Values(params Guid[] values) => Assign(values?.Select(v => (Id)v), (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<Guid> values) => Values(values.ToArray());
	}
}
