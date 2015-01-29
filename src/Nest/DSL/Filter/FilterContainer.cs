using System.Linq;
using Nest.DSL.Visitor;
using Newtonsoft.Json;

namespace Nest
{
	public class FilterContainer : IFilterContainer, ICustomJson
	{
		private IFilterContainer Self { get { return this; } }

		string IFilterContainer.FilterName { get; set; }

		string IFilterContainer.CacheKey { get; set; }

		bool? IFilterContainer.Cache { get; set; }

		string IFilterContainer.RawFilter { get; set; }

		[JsonIgnore]
		bool IFilterContainer.IsConditionless { get; set; }
		public bool IsConditionless
		{
			get { return Self.IsConditionless; }
			internal set { Self.IsConditionless = value;  }
		}

		[JsonIgnore]
		bool IFilterContainer.IsStrict { get; set; }
		public bool IsStrict { get { return Self.IsStrict; } }

		[JsonIgnore]
		bool IFilterContainer.IsVerbatim { get; set; }
		public bool IsVerbatim { get { return Self.IsVerbatim; } }

		IBoolFilter IFilterContainer.Bool { get; set; }

		IExistsFilter IFilterContainer.Exists { get; set; }

		IMissingFilter IFilterContainer.Missing { get; set; }

		IIdsFilter IFilterContainer.Ids { get; set; }

		IGeoBoundingBoxFilter IFilterContainer.GeoBoundingBox { get; set; }

		IGeoDistanceFilter IFilterContainer.GeoDistance { get; set; }

		IGeoDistanceRangeFilter IFilterContainer.GeoDistanceRange { get; set; }

        IGeoHashCellFilter IFilterContainer.GeoHashCell { get; set; }

		IGeoPolygonFilter IFilterContainer.GeoPolygon { get; set; }

		IGeoShapeBaseFilter IFilterContainer.GeoShape { get; set; }

		ILimitFilter IFilterContainer.Limit { get; set; }

		ITypeFilter IFilterContainer.Type { get; set; }

		IIndicesFilter IFilterContainer.Indices { get; set; }

		IMatchAllFilter IFilterContainer.MatchAll { get; set; }

		IHasChildFilter IFilterContainer.HasChild { get; set; }

		IHasParentFilter IFilterContainer.HasParent { get; set; }

		IRangeFilter IFilterContainer.Range { get; set; }

		IPrefixFilter IFilterContainer.Prefix { get; set; }

		ITermFilter IFilterContainer.Term { get; set; }

		ITermsBaseFilter IFilterContainer.Terms { get; set; }

		IQueryFilter IFilterContainer.Query { get; set; }

		IAndFilter IFilterContainer.And { get; set; }

		IOrFilter IFilterContainer.Or { get; set; }

		INotFilter IFilterContainer.Not { get; set; }

		IScriptFilter IFilterContainer.Script { get; set; }

		INestedFilter IFilterContainer.Nested { get; set; }

		IRegexpFilter IFilterContainer.Regexp { get; set; }

		public FilterContainer() { }
		public FilterContainer(PlainFilter filter)
		{
			PlainFilter.ToContainer(filter, this);
		}
		public static FilterContainer From(PlainFilter filter)
		{
			return PlainFilter.ToContainer(filter);
		}

		/// <summary>
		/// AND's two BaseFilters
		/// </summary>
		/// <returns>A new basefilter that represents the AND of the two</returns>
		public static FilterContainer operator &(FilterContainer leftContainer, FilterContainer rightContainer)
		{
			FilterContainer filterContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out filterContainer)) return filterContainer;

			return leftContainer.MergeMustFilters(rightContainer);
		}

		public static FilterContainer operator |(FilterContainer leftContainer, FilterContainer rightContainer)
		{
			
			FilterContainer filterContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out filterContainer)) return filterContainer;

			return leftContainer.MergeShouldFilters(rightContainer);
		}


		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(FilterContainer leftContainer, FilterContainer rightContainer,
			out FilterContainer filterContainer)
		{
			var combined = new[] {leftContainer, rightContainer};
			filterContainer = !combined.Any(bf => bf == null || bf.IsConditionless)
				? null
				: combined.FirstOrDefault(bf => bf != null && !bf.IsConditionless) ?? CreateEmptyContainer();
			return filterContainer != null;
		}

		public static FilterContainer operator !(FilterContainer filterContainer)
		{
			if (filterContainer == null || filterContainer.IsConditionless) return CreateEmptyContainer();

			IFilterContainer f = new FilterContainer();
			f.Bool = new BoolBaseFilterDescriptor();
			f.Bool.MustNot = new[] { filterContainer };
			return f as FilterContainer;
		}

		public static bool operator false(FilterContainer a)
		{
			return false;
		}

		public static bool operator true(FilterContainer a)
		{
			return false;
		}

		public void Accept(IQueryVisitor visitor)
		{
			var walker = new QueryFilterWalker();
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Filter;
			walker.Walk(this, visitor);
		}

		private static FilterContainer CreateEmptyContainer()
		{
			var q = new FilterContainer();
			((IFilterContainer)q).IsConditionless = true;
			return q;
		}

		object ICustomJson.GetCustomJson()
		{
			var f = ((IFilterContainer)this);
			if (f.RawFilter.IsNullOrEmpty()) return f;
			return new RawJson(f.RawFilter);
		}
	}
}
