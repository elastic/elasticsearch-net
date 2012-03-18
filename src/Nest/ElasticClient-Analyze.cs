using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq.Expressions;

namespace Nest
{
	public partial class ElasticClient
	{
		public AnalyzeResponse Analyze(string text)
		{
			var index = this.Settings.DefaultIndex;
			return this._Analyze(new AnalyzeParams() { Index = index }, text);
		}
		public AnalyzeResponse Analyze(AnalyzeParams analyzeParams, string text)
		{
			analyzeParams.ThrowIfNull("analyzeParams");
			analyzeParams.Index.ThrowIfNull("analyzeParams.Index");
			return this._Analyze(analyzeParams, text);
		}
		public AnalyzeResponse Analyze<T>(Expression<Func<T, object>> selector, string text) where T : class
		{
			var index = this.Settings.DefaultIndex;
			return this.Analyze<T>(selector, index, text);
		}
		public AnalyzeResponse Analyze<T>(Expression<Func<T, object>> selector, string index, string text) where T : class
		{
			selector.ThrowIfNull("selector");
			var fieldName = PropertyNameResolver.Resolve(selector);
			var analyzeParams = new AnalyzeParams() { Index = index, Field = fieldName };
			return this._Analyze(analyzeParams, text);
		}
		private AnalyzeResponse _Analyze(AnalyzeParams analyzeParams, string text)
		{
			var path = this.CreatePath(analyzeParams.Index) + "_analyze?text=";
			path += Uri.EscapeDataString(text);

			if (!analyzeParams.Field.IsNullOrEmpty())
				path += "&field=" + analyzeParams.Field;
			else if (!analyzeParams.Analyzer.IsNullOrEmpty())
				path += "&analyzer=" + analyzeParams.Analyzer;

			var status = this.Connection.GetSync(path);
			var r = this.ToParsedResponse<AnalyzeResponse>(status);
			return r;
		}
	}
}
