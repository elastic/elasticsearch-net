// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(SourceField))]
	public interface ISourceField : IFieldMapping
	{
		[DataMember(Name ="compress")]
		bool? Compress { get; set; }

		[DataMember(Name ="compress_threshold")]
		string CompressThreshold { get; set; }

		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }

		[DataMember(Name ="excludes")]
		IEnumerable<string> Excludes { get; set; }

		[DataMember(Name ="includes")]
		IEnumerable<string> Includes { get; set; }
	}

	public class SourceField : ISourceField
	{
		public bool? Compress { get; set; }
		public string CompressThreshold { get; set; }
		public bool? Enabled { get; set; }
		public IEnumerable<string> Excludes { get; set; }
		public IEnumerable<string> Includes { get; set; }
	}

	public class SourceFieldDescriptor
		: DescriptorBase<SourceFieldDescriptor, ISourceField>, ISourceField
	{
		bool? ISourceField.Compress { get; set; }
		string ISourceField.CompressThreshold { get; set; }
		bool? ISourceField.Enabled { get; set; }
		IEnumerable<string> ISourceField.Excludes { get; set; }
		IEnumerable<string> ISourceField.Includes { get; set; }

		public SourceFieldDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);

		public SourceFieldDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		public SourceFieldDescriptor CompressionThreshold(string compressionThreshold) =>
			Assign(compressionThreshold, (a, v) =>
			{
				a.Compress = true;
				a.CompressThreshold = v;
			});

		public SourceFieldDescriptor Includes(IEnumerable<string> includes) => Assign(includes, (a, v) => a.Includes = v);

		public SourceFieldDescriptor Excludes(IEnumerable<string> excludes) => Assign(excludes, (a, v) => a.Excludes = v);
	}
}
