// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(Analysis))]
	public interface IAnalysis
	{
		[DataMember(Name ="analyzer")]
		IAnalyzers Analyzers { get; set; }

		[DataMember(Name ="char_filter")]
		ICharFilters CharFilters { get; set; }

		[DataMember(Name ="normalizer")]
		INormalizers Normalizers { get; set; }

		[DataMember(Name ="filter")]
		ITokenFilters TokenFilters { get; set; }

		[DataMember(Name ="tokenizer")]
		ITokenizers Tokenizers { get; set; }
	}

	public class Analysis : IAnalysis
	{
		public IAnalyzers Analyzers { get; set; }
		public ICharFilters CharFilters { get; set; }

		public INormalizers Normalizers { get; set; }
		public ITokenFilters TokenFilters { get; set; }
		public ITokenizers Tokenizers { get; set; }
	}

	[DataContract]
	public class AnalysisDescriptor : DescriptorBase<AnalysisDescriptor, IAnalysis>, IAnalysis
	{
		IAnalyzers IAnalysis.Analyzers { get; set; }
		ICharFilters IAnalysis.CharFilters { get; set; }
		INormalizers IAnalysis.Normalizers { get; set; }
		ITokenFilters IAnalysis.TokenFilters { get; set; }
		ITokenizers IAnalysis.Tokenizers { get; set; }

		public AnalysisDescriptor Analyzers(Func<AnalyzersDescriptor, IPromise<IAnalyzers>> selector) =>
			Assign(selector, (a, v) => a.Analyzers = v?.Invoke(new AnalyzersDescriptor())?.Value);

		public AnalysisDescriptor CharFilters(Func<CharFiltersDescriptor, IPromise<ICharFilters>> selector) =>
			Assign(selector, (a, v) => a.CharFilters = v?.Invoke(new CharFiltersDescriptor())?.Value);

		public AnalysisDescriptor TokenFilters(Func<TokenFiltersDescriptor, IPromise<ITokenFilters>> selector) =>
			Assign(selector, (a, v) => a.TokenFilters = v?.Invoke(new TokenFiltersDescriptor())?.Value);

		public AnalysisDescriptor Tokenizers(Func<TokenizersDescriptor, IPromise<ITokenizers>> selector) =>
			Assign(selector, (a, v) => a.Tokenizers = v?.Invoke(new TokenizersDescriptor())?.Value);

		public AnalysisDescriptor Normalizers(Func<NormalizersDescriptor, IPromise<INormalizers>> selector) =>
			Assign(selector, (a, v) => a.Normalizers = v?.Invoke(new NormalizersDescriptor())?.Value);
	}
}
