using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class ValidateQueryDescriptor<T> : QueryPathDescriptorBase<ValidateQueryDescriptor<T>, T>
		where T : class
	{
		
		internal bool? _Explain { get; set; }
		
		internal string _QueryStringQuery { get; set; }
		
		internal string _Source { get; set; }

		internal string _OperationThreading { get; set; }
	
		internal IgnoreIndicesOptions? _IgnoreIndices { get; set; }

		///<summary>Query in the Lucene query string syntax will use a GET and ?q= instead of POST</summary>
		public ValidateQueryDescriptor<T> UseSimpleQueryString(string query)
		{
			this._QueryStringQuery = query;
			return this;
		}

		///<summary>The URL-encoded query definition (instead of using the request body)</summary>
		public ValidateQueryDescriptor<T> Source(string source)
		{
			this._Source = source;
			return this;
		}

		///<summary>TODO: ?</summary>
		public ValidateQueryDescriptor<T> OperationThreading(string operationThreading)
		{
			this._OperationThreading = operationThreading;
			return this;
		}

		///<summary>When performed on multiple indices, allows to ignore `missing` ones</summary>
		public ValidateQueryDescriptor<T> IgnoreIndices(IgnoreIndicesOptions ignoreIndices)
		{
			this._IgnoreIndices = ignoreIndices;
			return this;
		}

		///<summary>Return detailed information about the error</summary>
		public ValidateQueryDescriptor<T> Explain()
		{
			this._Explain = true;
			return this;
		}

	}
}
