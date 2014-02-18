using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Search
{
	public class SourceDescriptor<T> where T : class 
 	{
		[JsonProperty("include")]
		internal IEnumerable<PropertyPathMarker> _Include { get; set; } 

		[JsonProperty("exclude")]
		internal IEnumerable<PropertyPathMarker> _Exclude { get; set; } 

		public SourceDescriptor<T> Include(params string[] fields)
		{
			this._Include = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SourceDescriptor<T> Include(params Expression<Func<T, object>>[] fields)
		{
			this._Include = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SourceDescriptor<T> Include(Func<FluentFieldList<T>, FluentFieldList<T>>  fields)
		{
			this._Include = fields(new FluentFieldList<T>()).ToList();
			return this;
		}
		public SourceDescriptor<T> Exclude(params string[] fields)
		{
			this._Exclude = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SourceDescriptor<T> Exclude(params Expression<Func<T, object>>[] fields)
		{
			this._Exclude = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SourceDescriptor<T> Exclude(Func<FluentFieldList<T>, FluentFieldList<T>>  fields)
		{
			this._Exclude = fields(new FluentFieldList<T>()).ToList();
			return this;
		}
	}
}
