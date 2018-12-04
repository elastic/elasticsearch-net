using System;
using Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(SourceFilterFormatter))]
	public interface ISourceFilter
	{
		Fields Excludes { get; set; }

		Fields Includes { get; set; }
	}

	public class SourceFilter : ISourceFilter
	{
		public static SourceFilter ExcludeAll { get; } = new SourceFilter { Excludes = new[] { "*" } };
		public Fields Excludes { get; set; }
		public static SourceFilter IncludeAll { get; } = new SourceFilter { Includes = new[] { "*" } };

		public Fields Includes { get; set; }
	}

	public class SourceFilterDescriptor<T> : DescriptorBase<SourceFilterDescriptor<T>, ISourceFilter>, ISourceFilter
		where T : class
	{
		Fields ISourceFilter.Excludes { get; set; }
		Fields ISourceFilter.Includes { get; set; }

		public SourceFilterDescriptor<T> Includes(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Includes = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SourceFilterDescriptor<T> IncludeAll() => Assign(a => a.Includes = new[] { "*" });

		public SourceFilterDescriptor<T> Excludes(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Excludes = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SourceFilterDescriptor<T> ExcludeAll() => Assign(a => a.Excludes = new[] { "*" });
	}
}
