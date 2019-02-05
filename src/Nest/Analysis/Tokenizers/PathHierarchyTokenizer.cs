using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	///  The path_hierarchy tokenizer takes something like this:
	/// <para>/something/something/else</para>
	/// <para>And produces tokens:</para>
	/// <para></para>
	/// <para>/something</para>
	/// <para>/something/something</para>
	/// <para>/something/something/else</para>
	/// </summary>
	public interface IPathHierarchyTokenizer : ITokenizer
	{
		/// <summary>
		/// The buffer size to use, defaults to 1024.
		/// </summary>
		[DataMember(Name ="buffer_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? BufferSize { get; set; }

		/// <summary>
		/// The character delimiter to use, defaults to /.
		/// </summary>
		[DataMember(Name ="delimiter")]
		char? Delimiter { get; set; }

		/// <summary>
		/// An optional replacement character to use. Defaults to the delimiter
		/// </summary>
		[DataMember(Name ="replacement")]
		char? Replacement { get; set; }

		/// <summary>
		/// Generates tokens in reverse order, defaults to false.
		/// </summary>
		[DataMember(Name ="reverse")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Reverse { get; set; }

		/// <summary>
		/// Controls initial tokens to skip, defaults to 0.
		/// </summary>
		[DataMember(Name ="skip")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? Skip { get; set; }
	}

	/// <inheritdoc />
	public class PathHierarchyTokenizer : TokenizerBase, IPathHierarchyTokenizer
	{
		public PathHierarchyTokenizer() => Type = "path_hierarchy";

		/// <summary />
		public int? BufferSize { get; set; }

		/// <summary />
		public char? Delimiter { get; set; }

		/// <summary />
		public char? Replacement { get; set; }

		/// <summary />
		public bool? Reverse { get; set; }

		/// <summary />
		public int? Skip { get; set; }
	}

	/// <inheritdoc />
	public class PathHierarchyTokenizerDescriptor
		: TokenizerDescriptorBase<PathHierarchyTokenizerDescriptor, IPathHierarchyTokenizer>, IPathHierarchyTokenizer
	{
		protected override string Type => "path_hierarchy";

		int? IPathHierarchyTokenizer.BufferSize { get; set; }
		char? IPathHierarchyTokenizer.Delimiter { get; set; }
		char? IPathHierarchyTokenizer.Replacement { get; set; }
		bool? IPathHierarchyTokenizer.Reverse { get; set; }
		int? IPathHierarchyTokenizer.Skip { get; set; }

		/// <inheritdoc />
		public PathHierarchyTokenizerDescriptor BufferSize(int? bufferSize) => Assign(a => a.BufferSize = bufferSize);

		/// <inheritdoc />
		public PathHierarchyTokenizerDescriptor Skip(int? skip) => Assign(a => a.Skip = skip);

		/// <inheritdoc />
		public PathHierarchyTokenizerDescriptor Reverse(bool? reverse = true) => Assign(a => a.Reverse = reverse);

		/// <inheritdoc />
		public PathHierarchyTokenizerDescriptor Delimiter(char? delimiter) => Assign(a => a.Delimiter = delimiter);

		/// <inheritdoc />
		public PathHierarchyTokenizerDescriptor Replacement(char? replacement) =>
			Assign(a => a.Replacement = replacement);
	}
}
