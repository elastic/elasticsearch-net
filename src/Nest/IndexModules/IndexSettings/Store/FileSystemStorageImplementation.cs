using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// The store module allows you to control how index data is stored and accessed on disk.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FileSystemStorageImplementation
	{
		/// <summary>
		///The Simple FS type is a straightforward implementation of file system storage (maps to 
		/// Lucene SimpleFsDirectory) using a random access file. This implementation has poor 
		/// concurrent performance (multiple threads will bottleneck). It is usually better to use
		///  the niofs when you need index persistence. 
		/// </summary>
		[EnumMember(Value = "simplefs")]
		Simple,

		/// <summary>
		/// The NIO FS type stores the shard index on the file system (maps to Lucene NIOFSDirectory)
		/// using NIO. It allows multiple threads to read from the same file concurrently. It is not 
		/// recommended on Windows because of a bug in the SUN Java implementation./
		/// </summary>
		[EnumMember(Value = "niofs")]
		NIO,

		/// <summary>
		///The MMap FS type stores the shard index on the file system (maps to Lucene MMapDirectory) 
		/// by mapping a file into memory (mmap). Memory mapping uses up a portion of the virtual memory
		///  address space in your process equal to the size of the file being mapped. Before using this class,
		///  be sure you have allowed plenty of virtual address space.
		/// </summary>
		[EnumMember(Value = "mmapfs")]
		MMap,

		/// <summary>
		/// The default type is a hybrid of NIO FS and MMapFS, which chooses the best file system for each
		/// type of file. Currently only the Lucene term dictionary and doc values files are memory mapped to 
		/// reduce the impact on the operating system. All other files are opened using Lucene NIOFSDirectory.
		/// </summary>
		[EnumMember(Value = "default_fs")]
		Default,
	}
}