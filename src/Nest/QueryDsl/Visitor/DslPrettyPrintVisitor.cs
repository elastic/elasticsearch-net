using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public class DslPrettyPrintVisitor : IQueryVisitor
	{
		private readonly StringBuilder _sb;
		private string _final;
		private readonly Inferrer _infer;

		public string PrettyPrint
		{
			get
			{
				if (_final.IsNullOrEmpty())
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

		public bool IsVerbatim { get; set; }
		public bool IsConditionless { get; set; }
		public bool IsStrict { get; set; }

		public virtual void Visit(IQueryContainer baseQuery)
		{
			this.IsConditionless = baseQuery.IsConditionless;
			this.IsStrict = baseQuery.IsStrict;
			this.IsVerbatim = baseQuery.IsVerbatim;
		}

		public virtual void Visit(IQuery query) { }

		private void Write(string queryType, Dictionary<string, string> properties)
		{
			properties = properties ?? new Dictionary<string, string>();
			var props = string.Join(", ", properties.Select(kv => $"{kv.Key}: {kv.Value}"));
			var indent = new string('-',(Depth -1) * 2);
			var scope = this.Scope.GetStringValue().ToLowerInvariant();
			_sb.AppendFormat("{0}{1}: {2} ({3}){4}", indent, scope, queryType, props, Environment.NewLine);
		}
		private void Write(string queryType, Field field = null)
		{
			this.Write(queryType, field == null
				? null
				: new Dictionary<string, string> {{"field", this._infer.Field(field)}});
		}

		public virtual void Visit(IBoolQuery query) => Write("bool");

		public virtual void Visit(IBoostingQuery query) => Write("boosting");

		public virtual void Visit(ICommonTermsQuery query) => Write("common_terms", query.Field);

		public virtual void Visit(IConstantScoreQuery query) => Write("constant_score");

		public virtual void Visit(IDisMaxQuery query) => Write("dis_max");

		public virtual void Visit(ISpanContainingQuery query) => Write("span_containing");

		public virtual void Visit(ISpanWithinQuery query) => Write("span_within");

		public virtual void Visit(IDateRangeQuery query) => Write("date_range");

		public virtual void Visit(INumericRangeQuery query) => Write("numeric_range");

        public virtual void Visit(ITermRangeQuery query) => Write("term_range");

		public virtual void Visit(IFunctionScoreQuery query) => Write("function_core");

		public virtual void Visit(IFuzzyQuery query) => Write("fuzzy", query.Field);

		public virtual void Visit(IFuzzyNumericQuery query) => Write("fuzzy_numeric", query.Field);

		public virtual void Visit(IFuzzyDateQuery query) => Write("fuzzy_date", query.Field);

		public virtual void Visit(IFuzzyStringQuery query) => Write("fuzzy_string", query.Field);

		public virtual void Visit(IGeoShapeQuery query)
		{
			switch (query.Shape)
			{
				case null when query.IndexedShape != null:
					Write("geo_indexed_shape");
					break;
				case ICircleGeoShape circleGeoShape:
					Write("geo_shape_circle");
					break;
				case IEnvelopeGeoShape envelopeGeoShape:
					Write("geo_shape_envelope");
					break;
				case IGeometryCollection geometryCollection:
					Write("geo_shape_geometrycollection");
					break;
				case ILineStringGeoShape lineStringGeoShape:
					Write("geo_shape_linestring");
					break;
				case IMultiLineStringGeoShape multiLineStringGeoShape:
					Write("geo_shape_multi_linestring");
					break;
				case IMultiPointGeoShape multiPointGeoShape:
					Write("geo_shape_multi_point");
					break;
				case IMultiPolygonGeoShape multiPolygonGeoShape:
					Write("geo_shape_multi_polygon");
					break;
				case IPointGeoShape pointGeoShape:
					Write("geo_shape_point");
					break;
				case IPolygonGeoShape polygonGeoShape:
					Write("geo_shape_polygon");
					break;
				default:
					Write("geo_shape", query.Field);
					break;
			}

		}

		public virtual void Visit(IHasChildQuery query) => Write("has_child");

		public virtual void Visit(IHasParentQuery query) => Write("has_parent");

		public virtual void Visit(IIdsQuery query) => Write("ids");

		public virtual void Visit(IMatchQuery query) => Write("match", query.Field);

		public virtual void Visit(IMatchPhraseQuery query) => Write("match_phrase", query.Field);

		public virtual void Visit(IMatchPhrasePrefixQuery query) => Write("match_phrase_prefix", query.Field);

		public virtual void Visit(IMatchAllQuery query) => Write("match_all");

		public virtual void Visit(IMatchNoneQuery query) => Write("match_none");

		public virtual void Visit(IMoreLikeThisQuery query) => Write("more_like_this");

		public virtual void Visit(IMultiMatchQuery query) => Write("multi_match");

		public virtual void Visit(INestedQuery query) => Write("nested");

		public virtual void Visit(IPrefixQuery query) => Write("prefix");

		public virtual void Visit(IQueryStringQuery query) => Write("query_string");

		public virtual void Visit(IRangeQuery query) => Write("range");

		public virtual void Visit(IRegexpQuery query) => Write("regexp");

		public virtual void Visit(ISimpleQueryStringQuery query) => Write("simple_query_string");

		public virtual void Visit(ISpanFirstQuery query) => Write("span_first");

		public virtual void Visit(ISpanNearQuery query) => Write("span_near");

		public virtual void Visit(ISpanNotQuery query) => Write("span_not");

		public virtual void Visit(ISpanOrQuery query) => Write("span_or");

		public virtual void Visit(ISpanTermQuery query) => Write("span_term");

		public virtual void Visit(ISpanFieldMaskingQuery query) => Write("field_masking_span");

		public virtual void Visit(ITermQuery query) => Write("term", query.Field);

		public virtual void Visit(IWildcardQuery query) => Write("wildcard");

		public virtual void Visit(ITermsQuery query) => Write("terms");

		public virtual void Visit(ITypeQuery query) => Write("type");

		public virtual void Visit(IGeoPolygonQuery query) => Write("geo_polygon");

		public virtual void Visit(IGeoDistanceQuery query) => Write("geo_distance");

		public virtual void Visit(ISpanMultiTermQuery query) => Write("span_multi_term");

		public virtual void Visit(ISpanSubQuery query)=> Write("span_sub");

		public virtual void Visit(IConditionlessQuery query)=> Write("conditonless_query");

		public virtual void Visit(ISpanQuery query)=> Write("span");

		public virtual void Visit(IGeoBoundingBoxQuery query) => Write("geo_bounding_box");

		public virtual void Visit(IExistsQuery query) => Write("exists");

		public virtual void Visit(IScriptQuery query) => Write("script");

		public virtual void Visit(IRawQuery query) => Write("raw");

		public virtual void Visit(IPercolateQuery query) => Write("percolate");

		public virtual void Visit(IParentIdQuery query) => Write("parent_id");

		public virtual void Visit(ITermsSetQuery query) => Write("terms_set");
	}
}
