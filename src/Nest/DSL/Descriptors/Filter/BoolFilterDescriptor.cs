using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
  internal static class BoolBaseFilterDescriptorExtensions
  {
    internal static bool CanJoinMust(this BoolBaseFilterDescriptor bq)
    {
      return bq == null || (bq != null && bq._CanJoinMust());
    }
    internal static bool CanJoinMustNot(this BoolBaseFilterDescriptor bq)
    {
      return bq == null || (bq != null && bq._CanJoinMustNot());
    }
    internal static bool CanJoinShould(this BoolBaseFilterDescriptor bq)
    {
      return bq == null || (bq != null && bq._CanJoinShould());
    }
    internal static IEnumerable<BaseFilter> MergeMustFilters(this BaseFilter lbq, BaseFilter rbq)
    {
      var lBoolDescriptor = lbq.BoolFilterDescriptor;
      var lHasMustFilters = lBoolDescriptor != null &&
        lBoolDescriptor._MustFilters.HasAny();

      var rBoolDescriptor = rbq.BoolFilterDescriptor;
      var rHasMustFilters = rBoolDescriptor != null &&
        rBoolDescriptor._MustFilters.HasAny();

      var lq = lHasMustFilters
      ? lBoolDescriptor._MustFilters
      : new[] { lbq };
      var rq = rHasMustFilters ? rBoolDescriptor._MustFilters : new[] { rbq };

      return lq.Concat(rq);
    }
    internal static IEnumerable<BaseFilter> MergeShouldFilters(this BaseFilter lbq, BaseFilter rbq)
    {
      var lBoolDescriptor = lbq.BoolFilterDescriptor;
      var lHasShouldFilters = lBoolDescriptor != null &&
        lBoolDescriptor._ShouldFilters.HasAny();

      var rBoolDescriptor = rbq.BoolFilterDescriptor;
      var rHasShouldFilters = rBoolDescriptor != null &&
        rBoolDescriptor._ShouldFilters.HasAny();


      var lq = lHasShouldFilters ? lBoolDescriptor._ShouldFilters : new[] { lbq };
      var rq = rHasShouldFilters ? rBoolDescriptor._ShouldFilters : new[] { rbq };

      return lq.Concat(rq);
    }
    internal static IEnumerable<BaseFilter> MergeMustNotFilters(this BaseFilter lbq, BaseFilter rbq)
    {
      var lBoolDescriptor = lbq.BoolFilterDescriptor;
      var lHasMustNotFilters = lBoolDescriptor != null &&
        lBoolDescriptor._MustNotFilters.HasAny();

      var rBoolDescriptor = rbq.BoolFilterDescriptor;
      var rHasMustNotFilters = rBoolDescriptor != null &&
        rBoolDescriptor._MustNotFilters.HasAny();


      var lq = lHasMustNotFilters ? lBoolDescriptor._MustNotFilters : Enumerable.Empty<BaseFilter>();
      var rq = rHasMustNotFilters ? rBoolDescriptor._MustNotFilters : Enumerable.Empty<BaseFilter>();
      if (!lq.HasAny() && !rq.HasAny())
        return null;

      return lq.Concat(rq);
    }
  }


	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	internal class BoolBaseFilterDescriptor : FilterBase
	{
		[JsonProperty("must")]
		internal IEnumerable<BaseFilter> _MustFilters { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<BaseFilter> _ShouldFilters { get; set; }

    [JsonProperty("must_not")]
    internal IEnumerable<BaseFilter> _MustNotFilters { get; set; }

    internal bool _HasOnlyMustNot()
    {
      return _MustNotFilters.HasAny() && !_ShouldFilters.HasAny() && !_MustFilters.HasAny();
    }

    internal bool _CanJoinMust()
    {
      return !_ShouldFilters.HasAny();
    }
    internal bool _CanJoinShould()
    {
      return (_ShouldFilters.HasAny() && !_MustFilters.HasAny() && !_MustNotFilters.HasAny())
        || !_ShouldFilters.HasAny();
    }
    internal bool _CanJoinMustNot()
    {
      return !_ShouldFilters.HasAny();
    }

	}

	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class BoolFilterDescriptor<T> : FilterBase where T : class
	{
		[JsonProperty("must")]
		internal IEnumerable<FilterDescriptor<T>> _MustFilters { get; set; }

		[JsonProperty("must_not")]
		internal IEnumerable<FilterDescriptor<T>> _MustNotFilters { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<FilterDescriptor<T>> _ShouldFilters { get; set; }

    public BoolFilterDescriptor<T> Must(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustFilters = descriptors;
			return this;
		}
    public BoolFilterDescriptor<T> MustNot(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustNotFilters = descriptors;
			return this;
		}
    public BoolFilterDescriptor<T> Should(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._ShouldFilters = descriptors;
			return this;
		}
	}
}
