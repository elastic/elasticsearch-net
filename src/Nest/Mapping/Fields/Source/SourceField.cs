using System.Collections.Generic;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<SourceField>))]
	public interface ISourceField : ISpecialField
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

	public class SourceFieldDescriptor : ISourceField
	{
		private ISourceField Self => this;

        bool? ISourceField.Enabled { get; set; }
		
		bool? ISourceField.Compress { get; set; }

		string ISourceField.CompressThreshold { get; set; }

		IEnumerable<string> ISourceField.Includes { get; set; }

		IEnumerable<string> ISourceField.Excludes { get; set; }

		public SourceFieldDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
	
		public SourceFieldDescriptor Compress(bool compress = true)
		{
			Self.Compress = compress;
			return this;
		}

		public SourceFieldDescriptor CompressionThreshold(string compressionThreshold)
		{
			Self.Compress = true;
			Self.CompressThreshold = compressionThreshold;
			return this;
		}

		public SourceFieldDescriptor Includes(IEnumerable<string> includes)
		{
			Self.Includes = includes;
			return this;
		}

		public SourceFieldDescriptor Excludes(IEnumerable<string> excludes)
		{
			Self.Excludes = excludes;
			return this;
		}
    }
}