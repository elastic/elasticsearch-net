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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetRollupIndexCapabilitiesResponse, IndexName, RollupIndexCapabilities>))]
	public class GetRollupIndexCapabilitiesResponse : DictionaryResponseBase<IndexName, RollupIndexCapabilities>
	{
		public IReadOnlyDictionary<IndexName, RollupIndexCapabilities> Indices => Self.BackingDictionary;
	}

	public class RollupIndexCapabilities
	{
		[DataMember(Name = "rollup_jobs")]
		public IReadOnlyCollection<RollupIndexCapabilitiesJob> RollupJobs { get; internal set; }
	}

	public class RollupIndexCapabilitiesJob
	{
		[DataMember(Name = "fields")]
		public RollupFieldsIndexCapabilitiesDictionary Fields { get; internal set; }

		[DataMember(Name = "index_pattern")]
		public string IndexPattern { get; internal set; }

		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		[DataMember(Name = "rollup_index")]
		public string RollupIndex { get; internal set; }
	}

	public class RollupFieldsIndexCapabilities : IsADictionaryBase<string, string> { }

	[JsonFormatter(typeof(Converter))]
	public class RollupFieldsIndexCapabilitiesDictionary : ResolvableDictionaryProxy<Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>>
	{
		internal RollupFieldsIndexCapabilitiesDictionary(IConnectionConfigurationValues c,
			IReadOnlyDictionary<Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>> b
		) : base(c, b) { }

		public IReadOnlyCollection<RollupFieldsIndexCapabilities> Field<T>(Expression<Func<T, object>> selector) => this[selector];

		public IReadOnlyCollection<RollupFieldsIndexCapabilities> Field<T, TValue>(Expression<Func<T, TValue>> selector) => this[selector];

		internal class Converter : ResolvableDictionaryFormatterBase
				<RollupFieldsIndexCapabilitiesDictionary, Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>>
		{
			protected override RollupFieldsIndexCapabilitiesDictionary Create(
				IConnectionSettingsValues s, Dictionary<Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>> d
			) => new RollupFieldsIndexCapabilitiesDictionary(s, d);
		}
	}
}
