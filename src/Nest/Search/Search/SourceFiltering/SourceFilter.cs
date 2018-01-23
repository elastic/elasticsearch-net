using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(SourceFilterJsonConverter))]
	public interface ISourceFilter
	{
		[JsonProperty("includes")]
		Fields Includes { get; set; }

		[JsonProperty("excludes")]
		Fields Excludes { get; set; }
	}

	public class SourceFilter : ISourceFilter
	{
		public static SourceFilter ExcludeAll { get; } = new SourceFilter { Excludes = new [] {"*"} };
		public static SourceFilter IncludeAll { get; } = new SourceFilter { Includes = new [] {"*"} };

		public Fields Includes { get; set; }
		public Fields Excludes { get; set; }
	}

	public class SourceFilterDescriptor<T> : DescriptorBase<SourceFilterDescriptor<T>, ISourceFilter>, ISourceFilter
		where T : class
	{
		Fields ISourceFilter.Includes { get; set; }

		Fields ISourceFilter.Excludes { get; set; }

		public SourceFilterDescriptor<T> Includes(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Includes = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SourceFilterDescriptor<T> IncludeAll() => Assign(a => a.Includes = new[] { "*" } );

		public SourceFilterDescriptor<T> Excludes(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Excludes = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SourceFilterDescriptor<T> ExcludeAll() => Assign(a => a.Excludes = new[] { "*" } );
	}
}
