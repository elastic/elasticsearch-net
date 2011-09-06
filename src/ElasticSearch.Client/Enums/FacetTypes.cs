using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	/// <summary>
	/// Fancy name for a pretty hairy switch case return;
	/// </summary>
	public class FacetTypeTranslator
	{
		public string GetFacetTypeNameFor<T>() where T :  Facet
		{
			switch (typeof(T).Name)
			{
				case "TermStatsFacet":
					return "terms_stats";
				case "StatisticalFacet":
					return "statistical";
				case "TermFacet":
					return "terms";
				case "HistogramFacet":
					return "histogram";
				case "DateHistogramFacet":
					return "date_histogram";
				case "DateRangeFacet":
				case "RangeFacet":
					return "range";
				case "GeoDistanceFacet":
					return "geo_distance";
				

			}
			return null;
		}
	}
}
