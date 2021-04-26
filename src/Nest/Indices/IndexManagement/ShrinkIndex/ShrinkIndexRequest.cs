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

namespace Nest
{
	[MapsApi("indices.shrink.json")]
	[ReadAs(typeof(ShrinkIndexRequest))]
	public partial interface IShrinkIndexRequest
	{
		[DataMember(Name ="aliases")]
		IAliases Aliases { get; set; }

		[DataMember(Name ="settings")]
		IIndexSettings Settings { get; set; }
	}

	public partial class ShrinkIndexRequest
	{
		public IAliases Aliases { get; set; }

		public IIndexSettings Settings { get; set; }
	}

	public partial class ShrinkIndexDescriptor
	{
		IAliases IShrinkIndexRequest.Aliases { get; set; }
		IIndexSettings IShrinkIndexRequest.Settings { get; set; }

		public ShrinkIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(selector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		public ShrinkIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(selector, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
