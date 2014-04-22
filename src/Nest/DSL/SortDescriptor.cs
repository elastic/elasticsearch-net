using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest.DSL.Descriptors
{
	public class SortDescriptor<T> where T : class
	{
		internal PropertyPathMarker _Field { get; set; }

		[JsonProperty("missing")]
		internal string _Missing { get; set; }

		[JsonProperty("order")]
		internal string _Order { get; set; }

        [JsonProperty("mode")]
        internal string _Mode { get; set; }

		[JsonProperty("nested_filter")]
		internal BaseFilterDescriptor NestedFilterDescriptor { get; set; }

		[JsonProperty("nested_path")]
		internal PropertyPathMarker _NestedPath { get; set; }

		[JsonProperty("ignore_unmapped")]
		internal bool? _IgnoreUnmappedFields { get; set; }

		public virtual SortDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}

		public virtual SortDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

		public virtual SortDescriptor<T> MissingLast()
		{
			this._Missing = "_last";
			return this;
		}
		public virtual SortDescriptor<T> MissingFirst()
		{
			this._Missing = "_first";
			return this;
		}
		public virtual SortDescriptor<T> MissingValue(string value)
		{
			this._Missing = value;
			return this;
		}
		public virtual SortDescriptor<T> IgnoreUnmappedFields(bool ignore = true)
		{
			this._IgnoreUnmappedFields = ignore;
			return this;
		}
		public virtual SortDescriptor<T> Ascending()
		{
			this._Order = "asc";
			return this;
		}
		public virtual SortDescriptor<T> Descending()
		{
			this._Order = "desc";
			return this;
		}

        public virtual SortDescriptor<T> NestedMin()
        {
            this._Mode = "min";
            return this;
        }

        public virtual SortDescriptor<T> NestedMax()
        {
            this._Mode = "max";
            return this;
        }

        public virtual SortDescriptor<T> NestedSum()
        {
            this._Mode = "sum";
            return this;
        }

        public virtual SortDescriptor<T> NestedAvg()
        {
            this._Mode = "avg";
            return this;
        }

		public virtual SortDescriptor<T> NestedFilter(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");

			var filter = new FilterDescriptorDescriptor<T>();
			NestedFilterDescriptor = filterSelector(filter);
			return this;
		}
		public virtual SortDescriptor<T> NestedPath(string path)
		{
			_NestedPath = path;
			return this;
		}
		public SortDescriptor<T> NestedPath(Expression<Func<T, object>> objectPath)
		{
			_NestedPath = objectPath;
			return this;
		}

		/// <summary>
		/// Pass true to sort ascending false to sort descending
		/// </summary>
		public virtual SortDescriptor<T> ToggleSort(bool ascending)
		{
			this._Order = ascending ? "asc" : "desc";
			return this;
		}
	}
}
