using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SourceField>))]
	public interface ISourceField : IFieldMapping
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("compress")]
		bool? Compress { get; set; }

		[JsonProperty("compress_threshold")]
		string CompressThreshold { get; set; }

		[JsonProperty("includes")]
		IEnumerable<string> Includes { get; set; }

		[JsonProperty("excludes")]
		IEnumerable<string> Excludes { get; set; }
	}

	public class SourceField : ISourceField
	{
		public bool? Enabled { get; set; }
		public bool? Compress { get; set; }
		public string CompressThreshold { get; set; }
		public IEnumerable<string> Includes { get; set; }
		public IEnumerable<string> Excludes { get; set; }
	}

	public class SourceFieldDescriptor 
		: DescriptorBase<SourceFieldDescriptor, ISourceField>, ISourceField
	{
        bool? ISourceField.Enabled { get; set; }
		bool? ISourceField.Compress { get; set; }
		string ISourceField.CompressThreshold { get; set; }
		IEnumerable<string> ISourceField.Includes { get; set; }
		IEnumerable<string> ISourceField.Excludes { get; set; }

		public SourceFieldDescriptor Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);

		public SourceFieldDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		public SourceFieldDescriptor CompressionThreshold(string compressionThreshold) =>
			Assign(a => { a.Compress = true; a.CompressThreshold = compressionThreshold; });

		public SourceFieldDescriptor Includes(IEnumerable<string> includes) => Assign(a => a.Includes = includes);

		public SourceFieldDescriptor Excludes(IEnumerable<string> excludes) => Assign(a => a.Excludes = excludes);
    }
}