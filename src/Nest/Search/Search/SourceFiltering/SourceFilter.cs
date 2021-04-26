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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(SourceFilterFormatter))]
	public interface ISourceFilter
	{
		[DataMember(Name = "excludes")]
		Fields Excludes { get; set; }

		[DataMember(Name = "includes")]
		Fields Includes { get; set; }
	}

	public class SourceFilter : ISourceFilter
	{
		public static SourceFilter ExcludeAll { get; } = new SourceFilter { Excludes = new[] { "*" } };
		public Fields Excludes { get; set; }
		public static SourceFilter IncludeAll { get; } = new SourceFilter { Includes = new[] { "*" } };

		public Fields Includes { get; set; }
	}

	public class SourceFilterDescriptor<T> : DescriptorBase<SourceFilterDescriptor<T>, ISourceFilter>, ISourceFilter
		where T : class
	{
		Fields ISourceFilter.Excludes { get; set; }
		Fields ISourceFilter.Includes { get; set; }

		public SourceFilterDescriptor<T> Includes(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Includes = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SourceFilterDescriptor<T> IncludeAll() => Assign(new[] { "*" }, (a, v) => a.Includes = v);

		public SourceFilterDescriptor<T> Excludes(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Excludes = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SourceFilterDescriptor<T> ExcludeAll() => Assign(new[] { "*" }, (a, v) => a.Excludes = v);
	}
}
