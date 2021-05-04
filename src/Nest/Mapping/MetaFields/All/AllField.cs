// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
	[ReadAs(typeof(AllField))]
	public interface IAllField : IFieldMapping
	{
		[DataMember(Name ="analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }

		[DataMember(Name ="omit_norms")]
		bool? OmitNorms { get; set; }

		[DataMember(Name ="search_analyzer")]
		string SearchAnalyzer { get; set; }

		[DataMember(Name ="similarity")]
		string Similarity { get; set; }

		[DataMember(Name ="store")]
		bool? Store { get; set; }

		[DataMember(Name ="store_term_vector_offsets")]
		bool? StoreTermVectorOffsets { get; set; }

		[DataMember(Name ="store_term_vector_payloads")]
		bool? StoreTermVectorPayloads { get; set; }

		[DataMember(Name ="store_term_vector_positions")]
		bool? StoreTermVectorPositions { get; set; }

		[DataMember(Name ="store_term_vectors")]
		bool? StoreTermVectors { get; set; }
	}

	[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
	public class AllField : IAllField
	{
		public string Analyzer { get; set; }
		public bool? Enabled { get; set; }
		public bool? OmitNorms { get; set; }
		public string SearchAnalyzer { get; set; }
		public string Similarity { get; set; }
		public bool? Store { get; set; }
		public bool? StoreTermVectorOffsets { get; set; }
		public bool? StoreTermVectorPayloads { get; set; }
		public bool? StoreTermVectorPositions { get; set; }
		public bool? StoreTermVectors { get; set; }
	}

	[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
	public class AllFieldDescriptor
		: DescriptorBase<AllFieldDescriptor, IAllField>, IAllField
	{
		string IAllField.Analyzer { get; set; }
		bool? IAllField.Enabled { get; set; }
		bool? IAllField.OmitNorms { get; set; }
		string IAllField.SearchAnalyzer { get; set; }
		string IAllField.Similarity { get; set; }
		bool? IAllField.Store { get; set; }
		bool? IAllField.StoreTermVectorOffsets { get; set; }
		bool? IAllField.StoreTermVectorPayloads { get; set; }
		bool? IAllField.StoreTermVectorPositions { get; set; }
		bool? IAllField.StoreTermVectors { get; set; }

		public AllFieldDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);

		public AllFieldDescriptor Store(bool? store = true) => Assign(store, (a, v) => a.Store = v);

		public AllFieldDescriptor StoreTermVectors(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectors = v);

		public AllFieldDescriptor StoreTermVectorOffsets(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectorOffsets = v);

		public AllFieldDescriptor StoreTermVectorPositions(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectorPositions = v);

		public AllFieldDescriptor StoreTermVectorPayloads(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectorPayloads = v);

		public AllFieldDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public AllFieldDescriptor SearchAnalyzer(string searchAnalyzer) => Assign(searchAnalyzer, (a, v) => a.SearchAnalyzer = v);

		public AllFieldDescriptor Similarity(string similarity) => Assign(similarity, (a, v) => a.Similarity = v);
	}
}
