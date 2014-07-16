using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Visitor;

namespace Nest.Tests.Unit.QueryParsers.Visitor
{

	/// <summary>
	/// Visitor that pretty prints a given query graph
	/// </summary>
	/// <remarks>Unfinished, purely for demo purposes</remarks>
	public class DslPrettyPrintVisitor : IQueryVisitor
	{
		private readonly StringBuilder _sb;
		private string _final;
		private ElasticInferrer _infer;

		public string PrettyPrint
		{
			get
			{
				if (string.IsNullOrEmpty(_final))
					_final = _sb.ToString();
				return _final;
			}
		}

		public DslPrettyPrintVisitor(IConnectionSettingsValues settings)
		{
			this._sb = new StringBuilder();
			this._infer = settings.Inferrer;
		}

		public virtual int Depth { get; set; }
		public virtual VisitorScope Scope { get; set; }

		protected bool IsVerbatim { get; set; }
		protected bool IsConditionless { get; set; }
		protected bool IsStrict { get; set; }
		
		public virtual void Visit(IQueryContainer baseQuery)
		{
			this.IsConditionless = baseQuery.IsConditionless;
			this.IsStrict = baseQuery.IsStrict;
			this.IsVerbatim = baseQuery.IsVerbatim;
		}
		public virtual void Visit(IQuery query)
		{
			//Write("");
		}
		public virtual void Visit(IFilterContainer filter)
		{
			this.IsConditionless = filter.IsConditionless;
			this.IsStrict = filter.IsStrict;
			this.IsVerbatim = filter.IsVerbatim;
		}
		public virtual void Visit(IFilter filter)
		{
		}
		
		
		private void Write(string queryType, Dictionary<string, string> properties)
		{
			properties = properties ?? new Dictionary<string, string>();
			var props = string.Join(", ", properties.Select(kv => string.Format("{0}: {1}", kv.Key, kv.Value)));
			var indent = new String(' ',(Depth -1) * 2);
			var scope = Enum.GetName(typeof(VisitorScope), this.Scope).ToLowerInvariant();
			_sb.AppendFormat("{0}{1}: {2} ({3}){4}", indent, scope, queryType, props, Environment.NewLine);
		}
		private void Write(string queryType, PropertyPathMarker fieldName = null)
		{
			this.Write(queryType, fieldName == null 
				? null 
				: new Dictionary<string, string> {{"field", this._infer.PropertyPath(fieldName)}});
		}

		public virtual void Visit(IBoolQuery query)
		{
			Write("bool");
		}

		public virtual void Visit(IBoostingQuery query)
		{
			Write("boosting");
		}

		public virtual void Visit(ICommonTermsQuery query)
		{
			Write("common_terms", query.Field);
		}

		public virtual void Visit(IConstantScoreQuery query)
		{
			Write("constant_score");
		}

		public virtual void Visit(ICustomBoostFactorQuery query)
		{
			Write("custom_boost_factor");
		}

		public virtual void Visit(ICustomFiltersScoreQuery query)
		{
			Write("custom_filters_score");
		}

		public virtual void Visit(ICustomScoreQuery query)
		{
			Write("custom_score");
		}

		public virtual void Visit(IDisMaxQuery query)
		{
			Write("dis_max");
		}

		public virtual void Visit(IFilteredQuery query)
		{
			Write("filtered");
		}

		public virtual void Visit(IFunctionScoreQuery query)
		{
			Write("function_score");
		}

		public virtual void Visit(IFuzzyQuery query)
		{
			Write("fuzzy", query.Field);
		}

		public virtual void Visit(IFuzzyLikeThisQuery query)
		{
			Write("fuzzy_like_this");
		}

		public virtual void Visit(IGeoShapeQuery query)
		{
			Write("geo_shape", query.Field);
		}

		public virtual void Visit(IHasChildQuery query)
		{
			Write("has_child");
		}

		public virtual void Visit(IHasParentQuery query)
		{
			Write("has_parent");
		}

		public virtual void Visit(IIdsQuery query)
		{
			Write("ids");
		}

		public virtual void Visit(IIndicesQuery query)
		{
			Write("indices");
		}

		public virtual void Visit(IMatchQuery query)
		{
			Write("match", query.Field);
		}

		public virtual void Visit(IMatchAllQuery query)
		{
			Write("match_all");
		}

		public virtual void Visit(IMoreLikeThisQuery query)
		{
			Write("more_like_this");
		}

		public virtual void Visit(IMultiMatchQuery query)
		{
			Write("multi_match");
		}

		public virtual void Visit(INestedQuery query)
		{
			Write("nested");
		}

		public virtual void Visit(IPrefixQuery query)
		{
			Write("prefix", query.Field);
		}

		public virtual void Visit(IQueryStringQuery query)
		{
			Write("query_string");
		}

		public virtual void Visit(IRangeQuery query)
		{
			Write("range");
		}

		public virtual void Visit(IRegexpQuery query)
		{
			Write("regexp");
		}

		public virtual void Visit(ISimpleQueryStringQuery query)
		{
			Write("simple_query_string");
		}

		public virtual void Visit(ISpanFirstQuery query)
		{
			Write("span_first");
		}

		public virtual void Visit(ISpanNearQuery query)
		{
			Write("span_near");
		}

		public virtual void Visit(ISpanNotQuery query)
		{
			Write("span_not");
		}

		public virtual void Visit(ISpanOrQuery query)
		{
			Write("span_or");
		}

		public virtual void Visit(ISpanTermQuery query)
		{
			Write("span_term");
		}

		public virtual void Visit(ITermQuery query)
		{
			Write("term", query.Field);
		}

		public virtual void Visit(IWildcardQuery query)
		{
			Write("wildcard");
		}

		public virtual void Visit(ITermsQuery query)
		{
			Write("terms");
		}

		public virtual void Visit(ITopChildrenQuery query)
		{
			Write("top_children");
		}

	
		

		public virtual void Visit(ITypeFilter filter)
		{
			Write("type");
		}

		public virtual void Visit(ITermsBaseFilter filter)
		{
			Write("terms");
		}

		public virtual void Visit(ITermFilter filter)
		{
			Write("term", filter.Field);
		}

		public virtual void Visit(IScriptFilter filter)
		{
			Write("script");
		}

		public virtual void Visit(IRegexpFilter filter)
		{
			Write("regexp");
		}

		public virtual void Visit(IRangeFilter filter)
		{
			Write("range");
		}

		public virtual void Visit(IQueryFilter filter)
		{
			Write("query");
		}

		public virtual void Visit(IPrefixFilter filter)
		{
			Write("prefix");
		}

		public virtual void Visit(IOrFilter filter)
		{
			Write("or");
		}

		public virtual void Visit(INotFilter filter)
		{
			Write("not");
		}

		public virtual void Visit(INestedFilter filter)
		{
			Write("nested");
		}

		public virtual void Visit(IMissingFilter filter)
		{
			Write("missing");
		}

		public virtual void Visit(IMatchAllFilter filter)
		{
			Write("match_all");
		}

		public virtual void Visit(ILimitFilter filter)
		{
			Write("limit");
		}

		public virtual void Visit(IIdsFilter filter)
		{
			Write("ids");
		}

		public virtual void Visit(IHasParentFilter filter)
		{
			Write("has_parent");
		}

		public virtual void Visit(IHasChildFilter filter)
		{
			Write("has_child");
		}

		public virtual void Visit(IGeoShapeBaseFilter filter)
		{
			Write("geo_shape");
		}

		public virtual void Visit(IGeoPolygonFilter filter)
		{
			Write("geo_polygon");
		}

		public virtual void Visit(IGeoDistanceRangeFilter filter)
		{
			Write("geo_distance_range");
		}

		public virtual void Visit(IGeoDistanceFilter filter)
		{
			Write("geo_distance");
		}

		public virtual void Visit(IGeoBoundingBoxFilter filter)
		{
			Write("geo_bounding_box");
		}

		public virtual void Visit(IExistsFilter filter)
		{
			Write("exists");
		}

		public virtual void Visit(IBoolFilter filter)
		{
			Write("bool");
		}

		public virtual void Visit(IAndFilter filter)
		{
			Write("and");
		}


	}
}
