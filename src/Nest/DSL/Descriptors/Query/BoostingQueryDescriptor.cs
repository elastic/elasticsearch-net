using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class BoolQueryDescriptor<T> where T : class
	{
		[JsonProperty("must")]
		internal IEnumerable<QueryDescriptor<T>> _MustQueries { get; set; }

		[JsonProperty("must_not")]
		internal IEnumerable<QueryDescriptor<T>> _MustNotQueries { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<QueryDescriptor<T>> _ShouldQueries { get; set; }

		[JsonProperty("minimum_number_should_match")]
		internal int? _MinimumNumberShouldMatches { get; set; }

		[JsonProperty("boost")]
		internal double? _Boost { get; set; }

		public BoolQueryDescriptor<T> MinimumNumberShouldMatch(int minimumShouldMatches)
		{
			this._MinimumNumberShouldMatches = minimumShouldMatches;
			return this;
		}
		public BoolQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}


		public BoolQueryDescriptor<T> Must(params Action<QueryDescriptor<T>>[] filters)
		{
			var descriptors = new List<QueryDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new QueryDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustQueries = descriptors;
			return this;
		}
		public BoolQueryDescriptor<T> MustNot(params Action<QueryDescriptor<T>>[] filters)
		{
			var descriptors = new List<QueryDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new QueryDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustNotQueries = descriptors;
			return this;
		}
		public BoolQueryDescriptor<T> Should(params Action<QueryDescriptor<T>>[] filters)
		{
			var descriptors = new List<QueryDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new QueryDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._ShouldQueries = descriptors;
			return this;
		}
	}
}
