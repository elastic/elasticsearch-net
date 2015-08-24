using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	//TODO REMOVE
	public class AnalysisDescriptor
	{
		internal readonly AnalysisSettings _AnalysisSettings;

		public AnalysisDescriptor()
		{
			this._AnalysisSettings = new AnalysisSettings();
		}
		internal AnalysisDescriptor(AnalysisSettings settings)
		{
			this._AnalysisSettings = settings;
		}

		public AnalysisDescriptor Analyzers(
			Func<FluentDictionary<string, AnalyzerBase>, FluentDictionary<string, AnalyzerBase>> analyzerSelector)
		{
			return this;
		}
		
		public AnalysisDescriptor Tokenizers(
			Func<FluentDictionary<string, TokenizerBase>, FluentDictionary<string, TokenizerBase>> tokenizerSelector)
		{
			return this;
		}

		public AnalysisDescriptor TokenFilters(
			Func<FluentDictionary<string, TokenFilterBase>, FluentDictionary<string, TokenFilterBase>> tokenFiltersSelecter)
		{
			return this;
		}

		public AnalysisDescriptor CharFilters(
			Func<FluentDictionary<string, CharFilterBase>, FluentDictionary<string, CharFilterBase>> charFiltersSelecter)
		{
			return this;
		}
	}
}
