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
		IEnumerable<Field> Include { get; set; }

		[JsonProperty("exclude")]
		IEnumerable<Field> Exclude { get; set; }
	}

	public class SourceFilter : ISourceFilter
	{
		public static SourceFilter ExcludeAll { get; } = new SourceFilter { Exclude = new Field[] {"*"} };
		public static SourceFilter IncludeAll { get; } = new SourceFilter { Include = new Field[] {"*"} };

		public IEnumerable<Field> Include { get; set; }
		public IEnumerable<Field> Exclude { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SearchSourceDescriptor<T> : ISourceFilter where T : class 
 	{
		private ISourceFilter Self => this;

		[JsonProperty("include")]
		IEnumerable<Field> ISourceFilter.Include { get; set; } 

		[JsonProperty("exclude")]
		IEnumerable<Field> ISourceFilter.Exclude { get; set; } 

		public SearchSourceDescriptor<T> Include(params string[] fields)
		{
			Self.Include = fields.Select(f => (Field) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Include(params Expression<Func<T, object>>[] fields)
		{
			Self.Include = fields.Select(f => (Field) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Include(Func<FluentFieldList<T>, FluentFieldList<T>>  fields)
		{
			Self.Include = fields(new FluentFieldList<T>()).ToList();
			return this;
		}
		public SearchSourceDescriptor<T> Exclude(params string[] fields)
		{
			Self.Exclude = fields.Select(f => (Field) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Exclude(params Expression<Func<T, object>>[] fields)
		{
			Self.Exclude = fields.Select(f => (Field) f).ToList();
			return this;
		} 
		public SearchSourceDescriptor<T> Exclude(Func<FluentFieldList<T>, FluentFieldList<T>>  fields)
		{
			Self.Exclude = fields(new FluentFieldList<T>()).ToList();
			return this;
		}
	}
}
