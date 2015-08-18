using System.Collections.Generic;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SourceFieldMapping>))]
	public interface ISourceFieldMapping : ISpecialField
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

	public class SourceFieldMapping : ISourceFieldMapping
	{
		public bool? Enabled { get; set; }
		public bool? Compress { get; set; }
		public string CompressThreshold { get; set; }
		public IEnumerable<string> Includes { get; set; }
		public IEnumerable<string> Excludes { get; set; }
	}

	public class SourceFieldMappingDescriptor : ISourceFieldMapping
	{
		private ISourceFieldMapping Self => this;

        bool? ISourceFieldMapping.Enabled { get; set; }
		
		bool? ISourceFieldMapping.Compress { get; set; }

		string ISourceFieldMapping.CompressThreshold { get; set; }

		IEnumerable<string> ISourceFieldMapping.Includes { get; set; }

		IEnumerable<string> ISourceFieldMapping.Excludes { get; set; }

		public SourceFieldMappingDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
	
		public SourceFieldMappingDescriptor Compress(bool compress = true)
		{
			Self.Compress = compress;
			return this;
		}

		public SourceFieldMappingDescriptor CompressionThreshold(string compressionThreshold)
		{
			Self.Compress = true;
			Self.CompressThreshold = compressionThreshold;
			return this;
		}

		public SourceFieldMappingDescriptor Includes(IEnumerable<string> includes)
		{
			Self.Includes = includes;
			return this;
		}

		public SourceFieldMappingDescriptor Excludes(IEnumerable<string> excludes)
		{
			Self.Excludes = excludes;
			return this;
		}
    }
}