using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISourceFilter
	{
		[JsonProperty("include")]
		IEnumerable<PropertyPathMarker> Include { get; set; }

		[JsonProperty("exclude")]
		IEnumerable<PropertyPathMarker> Exclude { get; set; }
	}

	public class SourceFilter : ISourceFilter
	{
		public IEnumerable<PropertyPathMarker> Include { get; set; }
		public IEnumerable<PropertyPathMarker> Exclude { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SearchSourceDescriptor<T> : ISourceFilter where T : class 
 	{
		private ISourceFilter Self => this;

		[JsonProperty("include")]
		IEnumerable<PropertyPathMarker> ISourceFilter.Include { get; set; } 

		[JsonProperty("exclude")]
		IEnumerable<PropertyPathMarker> ISourceFilter.Exclude { get; set; } 

		public SearchSourceDescriptor<T> Include(params string[] fields)
		{
			Self.Include = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Include(params Expression<Func<T, object>>[] fields)
		{
			Self.Include = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Include(Func<FluentFieldList<T>, FluentFieldList<T>>  fields)
		{
			Self.Include = fields(new FluentFieldList<T>()).ToList();
			return this;
		}
		public SearchSourceDescriptor<T> Exclude(params string[] fields)
		{
			Self.Exclude = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Exclude(params Expression<Func<T, object>>[] fields)
		{
			Self.Exclude = fields.Select(f => (PropertyPathMarker) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Exclude(Func<FluentFieldList<T>, FluentFieldList<T>>  fields)
		{
			Self.Exclude = fields(new FluentFieldList<T>()).ToList();
			return this;
		}
	}
}
