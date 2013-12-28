using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest.DSL.Descriptors
{

	public class RescoreQueryDescriptor<T> where T : class
	{
		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }
		internal bool _Verbatim { get; set; }
		internal string _RawQuery { get; set; }
		internal BaseQuery _Query { get; set; }


		[JsonProperty("rescore_query")]
		internal RawOrQueryDescriptor<T> _QueryOrRaw
		{
			get
			{
				if (this._RawQuery == null && this._Query == null)
					return null;
				return new RawOrQueryDescriptor<T>
				{
					Raw = this._RawQuery,
					Descriptor = this._Query
				};
			}
		}

		[JsonProperty("query_weight")]
		internal double? _QueryWeight { get; set; }

		[JsonProperty("rescore_query_weight")]
		internal double? _RescoreQueryWeight { get; set; }

		public virtual RescoreQueryDescriptor<T> QueryWeight(double queryWeight)
		{
			this._QueryWeight = queryWeight;
			return this;
		}

		public virtual RescoreQueryDescriptor<T> RescoreQueryWeight(double rescoreQueryWeight)
		{
			this._RescoreQueryWeight = rescoreQueryWeight;
			return this;
		}

		public virtual RescoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryDescriptor<T>().Strict(this._Strict);

			var bq = query(q);
			if (this._Strict && !this._Verbatim && bq.IsConditionless)
				throw new DslException("Query resulted in a conditionless query:\n{0}".F(JsonConvert.SerializeObject(bq, Formatting.Indented)));

			else if (bq.IsConditionless && !this._Verbatim)
				return this;
			this._Query = bq;
			return this;
		}
		/// <summary>
		/// Describe the query to perform using the static Query class
		/// </summary>
		public virtual RescoreQueryDescriptor<T> Query(BaseQuery query)
		{
			query.ThrowIfNull("query");
			if (query.IsConditionless && !this._Verbatim)
				return this;
			this._Query = query;
			return this;
		}

		/// <summary>
		/// Describe the query to perform as a raw json string
		/// </summary>
		public virtual RescoreQueryDescriptor<T> QueryRaw(string rawQuery)
		{
			rawQuery.ThrowIfNull("rawQuery");
			this._RawQuery = rawQuery;
			return this;
		}
		
		/// <summary>
		/// When strict is set, conditionless queries are treated as an exception. 
		/// </summary>
		public virtual RescoreQueryDescriptor<T> Strict(bool strict = true)
		{
			this._Strict = strict;
			return this;
		}

		/// <summary>
		/// When strict is set, conditionless queries are still send.
		/// </summary> 
		public virtual RescoreQueryDescriptor<T> Verbatim(bool verbatim = true)
		{
			this._Strict = verbatim;
			this._Verbatim = verbatim;
			return this;
		}
	}


	public class RescoreDescriptor<T> where T : class
	{
		[JsonProperty("window_size")]
		internal int? _WindowSize { get; set; }

		[JsonProperty("query")]
		internal RescoreQueryDescriptor<T> _Query { get; set; }

		public virtual RescoreDescriptor<T> RescoreQuery(Func<RescoreQueryDescriptor<T>, RescoreQueryDescriptor<T>> rescoreQuerySelector)
		{
			rescoreQuerySelector.ThrowIfNull("rescoreQuerySelector");
			this._Query = rescoreQuerySelector(new RescoreQueryDescriptor<T>());
			return this;
		}

		public virtual RescoreDescriptor<T> WindowSize(int windowSize)
		{
			this._WindowSize = windowSize;
			return this;
		}

	
	}
}
