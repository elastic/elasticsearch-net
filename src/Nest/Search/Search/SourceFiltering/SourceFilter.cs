// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
