/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// The store module allows you to control how index data is stored and accessed on disk.
	/// </summary>
	[StringEnum]
	public enum FileSystemStorageImplementation
	{
		/// <summary>
		/// The Simple FS type is a straightforward implementation of file system storage (maps to
		///  Lucene SimpleFsDirectory) using a random access file. This implementation has poor
		///  concurrent performance (multiple threads will bottleneck). It is usually better to use
		///   the niofs when you need index persistence.
		/// </summary>
		[EnumMember(Value = "simplefs")]
		Simple,

		/// <summary>
		/// The NIO FS type stores the shard index on the file system (maps to Lucene NIOFSDirectory)
		/// using NIO. It allows multiple threads to read from the same file concurrently. It is not
		/// recommended on Windows because of a bug in the SUN Java implementation.
		/// </summary>
		[EnumMember(Value = "niofs")]
		// ReSharper disable once InconsistentNaming
		NIO,

		/// <summary>
		/// The MMap FS type stores the shard index on the file system (maps to Lucene MMapDirectory)
		///  by mapping a file into memory (mmap). Memory mapping uses up a portion of the virtual memory
		///   address space in your process equal to the size of the file being mapped. Before using this class,
		///   be sure you have allowed plenty of virtual address space.
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
