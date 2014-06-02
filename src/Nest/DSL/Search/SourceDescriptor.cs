using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Search
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

	public class SearchSourceDescriptor<T> : ISourceFilter where T : class 
 	{
		public ISourceFilter Self { get { return this; } }

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
