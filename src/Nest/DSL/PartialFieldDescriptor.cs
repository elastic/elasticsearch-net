using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class PartialFieldDescriptor<T> where T : class
	{
		internal string _Field { get; set; }

		[JsonProperty("include")]
		internal IEnumerable<PropertyPathMarker> _Include { get; set; }

		[JsonProperty("exclude")]
		internal IEnumerable<PropertyPathMarker> _Exclude { get; set; }

		public PartialFieldDescriptor<T> PartialField(string field)
		{
			this._Field = field;
			return this;
		}

		public PartialFieldDescriptor<T> Include(params string[] paths)
		{
			this._Include = paths.Select(p => (PropertyPathMarker) p);
			return this;
		}

		public PartialFieldDescriptor<T> Exclude(params string[] paths)
		{
			this._Include = paths.Select(p => (PropertyPathMarker) p);
			return this;
		}
		public PartialFieldDescriptor<T> Include(params Expression<Func<T, object>>[] paths)
		{
			this._Include = paths.Select(p => (PropertyPathMarker) p);
			return this;
		}
		public PartialFieldDescriptor<T> Exclude(params Expression<Func<T, object>>[] paths)
		{
			this._Exclude = paths.Select(p => (PropertyPathMarker) p);
			return this;
		}
	}
}
