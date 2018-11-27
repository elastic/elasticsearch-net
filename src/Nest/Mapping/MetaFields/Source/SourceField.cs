using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(SourceField))]
	public interface ISourceField : IFieldMapping
	{
		[DataMember(Name ="compress")]
		bool? Compress { get; set; }

		[DataMember(Name ="compress_threshold")]
		string CompressThreshold { get; set; }

		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }

		[DataMember(Name ="excludes")]
		IEnumerable<string> Excludes { get; set; }

		[DataMember(Name ="includes")]
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

		public SourceFieldDescriptor Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);

		public SourceFieldDescriptor Compress(bool? compress = true) => Assign(a => a.Compress = compress);

		public SourceFieldDescriptor CompressionThreshold(string compressionThreshold) =>
			Assign(a =>
			{
				a.Compress = true;
				a.CompressThreshold = compressionThreshold;
			});

		public SourceFieldDescriptor Includes(IEnumerable<string> includes) => Assign(a => a.Includes = includes);

		public SourceFieldDescriptor Excludes(IEnumerable<string> excludes) => Assign(a => a.Excludes = excludes);
	}
}
