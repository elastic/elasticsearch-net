using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SourceField>))]
	public interface ISourceField : IFieldMapping
	{
		[JsonProperty("compress")]
		bool? Compress { get; set; }

		[JsonProperty("compress_threshold")]
		string CompressThreshold { get; set; }

		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("excludes")]
		IEnumerable<string> Excludes { get; set; }

		[JsonProperty("includes")]
		IEnumerable<string> Includes { get; set; }
	}

	public class SourceField : ISourceField
	{
		public bool? Compress { get; set; }
		public string CompressThreshold { get; set; }
		public bool? Enabled { get; set; }
		public IEnumerable<string> Excludes { get; set; }
		public IEnumerable<string> Includes { get; set; }
	}

	public class SourceFieldDescriptor
		: DescriptorBase<SourceFieldDescriptor, ISourceField>, ISourceField
	{
		bool? ISourceField.Compress { get; set; }
		string ISourceField.CompressThreshold { get; set; }
		bool? ISourceField.Enabled { get; set; }
		IEnumerable<string> ISourceField.Excludes { get; set; }
		IEnumerable<string> ISourceField.Includes { get; set; }

		public SourceFieldDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);

		public SourceFieldDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		public SourceFieldDescriptor CompressionThreshold(string compressionThreshold) =>
			Assign(compressionThreshold, (a, v) =>
			{
				a.Compress = true;
				a.CompressThreshold = v;
			});

		public SourceFieldDescriptor Includes(IEnumerable<string> includes) => Assign(includes, (a, v) => a.Includes = v);

		public SourceFieldDescriptor Excludes(IEnumerable<string> excludes) => Assign(excludes, (a, v) => a.Excludes = v);
	}
}
