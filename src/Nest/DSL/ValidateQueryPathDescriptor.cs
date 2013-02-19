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
	public class ValidateQueryPathDescriptor : ValidateQueryPathDescriptor<dynamic> { }
	public class ValidateQueryPathDescriptor<T> : QueryPathDescriptor<T>, IQueryPathDescriptor where T : class
	{
		internal bool _Explain { get; set; }
		internal string _QueryStringQuery { get; set; }
		public ValidateQueryPathDescriptor<T> UseSimpleQueryString(string query)
		{
			this._QueryStringQuery = query;
			return this;
		}
		public ValidateQueryPathDescriptor<T> Explain()
		{
			this._Explain = true;
			return this;
		}
		public override IDictionary<string, string> GetUrlParams()
		{
			var dict = new Dictionary<string, string>();
			if (this._Explain)
				dict.Add("explain", "true");
			if (!this._QueryStringQuery.IsNullOrEmpty())
				dict.Add("q", this._QueryStringQuery);
			return dict;
		}

		#region newing querypath funcs for fluent interface sake
		public new ValidateQueryPathDescriptor<T> Indices(IEnumerable<string> indices)
		{
			base.Indices(indices);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> Index(string index)
		{
			base.Index(index);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> Types(IEnumerable<string> types)
		{
			base.Types(types);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> Types(params string[] types)
		{
			base.Types(types);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> Types(IEnumerable<Type> types)
		{
			base.Types(types);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> Types(params Type[] types)
		{
			base.Types(types);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> Type(string type)
		{
			base.Type(type);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> Type(Type type)
		{
			base.Type(type);
			return this;
		}
		public new ValidateQueryPathDescriptor<T> AllIndices()
		{
			base.AllIndices();
			return this;
		}
		public new ValidateQueryPathDescriptor<T> AllTypes()
		{
			base.AllTypes();
			return this;
		}
		#endregion
	}
}
