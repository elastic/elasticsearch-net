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
			tokenizerSelector.ThrowIfNull("tokenizerSelector");
			var tokenizers = new FluentDictionary<string, TokenizerBase>(this._AnalysisSettings.Tokenizers);
			var newTokenizers = tokenizerSelector(tokenizers);
			this._AnalysisSettings.Tokenizers = newTokenizers;

			return this;
		}

		public AnalysisDescriptor TokenFilters(
			Func<FluentDictionary<string, TokenFilterBase>, FluentDictionary<string, TokenFilterBase>> tokenFiltersSelecter)
		{
			tokenFiltersSelecter.ThrowIfNull("tokenFiltersSelecter");
			var tokenFilters = new FluentDictionary<string, TokenFilterBase>(this._AnalysisSettings.TokenFilters);
			var newTokenFilters = tokenFiltersSelecter(tokenFilters);
			this._AnalysisSettings.TokenFilters = newTokenFilters;

			return this;
		}

		public AnalysisDescriptor CharFilters(
			Func<FluentDictionary<string, CharFilterBase>, FluentDictionary<string, CharFilterBase>> charFiltersSelecter)
		{
			charFiltersSelecter.ThrowIfNull("charFiltersSelecter");
			var charFilters = new FluentDictionary<string, CharFilterBase>(this._AnalysisSettings.CharFilters);
			var newCharFilters = charFiltersSelecter(charFilters);
			this._AnalysisSettings.CharFilters = newCharFilters;

			return this;
		}
	}
}
