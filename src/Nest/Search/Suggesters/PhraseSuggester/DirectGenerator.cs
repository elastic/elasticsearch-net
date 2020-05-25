// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DirectGenerator))]
	public interface IDirectGenerator
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name = "max_edits")]
		int? MaxEdits { get; set; }

		[DataMember(Name = "max_inspections")]
		float? MaxInspections { get; set; }

		[DataMember(Name = "max_term_freq")]
		float? MaxTermFrequency { get; set; }

		[DataMember(Name = "min_doc_freq")]
		float? MinDocFrequency { get; set; }

		[DataMember(Name = "min_word_length")]
		int? MinWordLength { get; set; }

		[DataMember(Name = "post_filter")]
		string PostFilter { get; set; }

		[DataMember(Name = "pre_filter")]
		string PreFilter { get; set; }

		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		[DataMember(Name = "size")]
		int? Size { get; set; }

		[DataMember(Name = "suggest_mode")]

		SuggestMode? SuggestMode { get; set; }
	}

	public class DirectGenerator : IDirectGenerator
	{
		public Field Field { get; set; }
		public int? MaxEdits { get; set; }
		public float? MaxInspections { get; set; }
		public float? MaxTermFrequency { get; set; }
		public float? MinDocFrequency { get; set; }
		public int? MinWordLength { get; set; }
		public string PostFilter { get; set; }
		public string PreFilter { get; set; }
		public int? PrefixLength { get; set; }
		public int? Size { get; set; }
		public SuggestMode? SuggestMode { get; set; }
	}

	public class DirectGeneratorDescriptor<T> : DescriptorBase<DirectGeneratorDescriptor<T>, IDirectGenerator>, IDirectGenerator
		where T : class
	{
		Field IDirectGenerator.Field { get; set; }
		int? IDirectGenerator.MaxEdits { get; set; }
		float? IDirectGenerator.MaxInspections { get; set; }
		float? IDirectGenerator.MaxTermFrequency { get; set; }
		float? IDirectGenerator.MinDocFrequency { get; set; }
		int? IDirectGenerator.MinWordLength { get; set; }
		string IDirectGenerator.PostFilter { get; set; }
		string IDirectGenerator.PreFilter { get; set; }
		int? IDirectGenerator.PrefixLength { get; set; }
		int? IDirectGenerator.Size { get; set; }
		SuggestMode? IDirectGenerator.SuggestMode { get; set; }

		public DirectGeneratorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DirectGeneratorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		public DirectGeneratorDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public DirectGeneratorDescriptor<T> SuggestMode(SuggestMode? mode) => Assign(mode, (a, v) => a.SuggestMode = v);

		public DirectGeneratorDescriptor<T> MinWordLength(int? length) => Assign(length, (a, v) => a.MinWordLength = v);

		public DirectGeneratorDescriptor<T> PrefixLength(int? length) => Assign(length, (a, v) => a.PrefixLength = v);

		public DirectGeneratorDescriptor<T> MaxEdits(int? maxEdits) => Assign(maxEdits, (a, v) => a.MaxEdits = v);

		public DirectGeneratorDescriptor<T> MaxInspections(float? maxInspections) => Assign(maxInspections, (a, v) => a.MaxInspections = v);

		public DirectGeneratorDescriptor<T> MinDocFrequency(float? frequency) => Assign(frequency, (a, v) => a.MinDocFrequency = v);

		public DirectGeneratorDescriptor<T> MaxTermFrequency(float? frequency) => Assign(frequency, (a, v) => a.MaxTermFrequency = v);

		public DirectGeneratorDescriptor<T> PreFilter(string preFilter) => Assign(preFilter, (a, v) => a.PreFilter = v);

		public DirectGeneratorDescriptor<T> PostFilter(string postFilter) => Assign(postFilter, (a, v) => a.PostFilter = v);
	}
}
