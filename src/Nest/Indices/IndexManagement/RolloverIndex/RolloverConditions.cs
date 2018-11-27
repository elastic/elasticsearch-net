using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Conditions that must be satisfied for a new index to be created
	/// with the rollover index API
	/// </summary>
	[DataContract]
	[ReadAs(typeof(RolloverConditions))]
	public interface IRolloverConditions
	{
		/// <summary>
		/// The maximum age of the index
		/// </summary>
		[DataMember(Name ="max_age")]
		Time MaxAge { get; set; }

		/// <summary>
		/// The maximum number of documents
		/// </summary>
		[DataMember(Name ="max_docs")]
		long? MaxDocs { get; set; }

		/// <summary>
		/// The maximum size of the index e.g. "5gb"
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.1.0+
		/// </remarks>
		[DataMember(Name ="max_size")]
		string MaxSize { get; set; }
	}

	/// <inheritdoc />
	public class RolloverConditions : IRolloverConditions
	{
		/// <inheritdoc />
		public Time MaxAge { get; set; }

		/// <inheritdoc />
		public long? MaxDocs { get; set; }

		/// <inheritdoc />
		public string MaxSize { get; set; }
	}

	/// <inheritdoc cref="IRolloverConditions" />
	public class RolloverConditionsDescriptor
		: DescriptorBase<RolloverConditionsDescriptor, IRolloverConditions>, IRolloverConditions
	{
		Time IRolloverConditions.MaxAge { get; set; }
		long? IRolloverConditions.MaxDocs { get; set; }
		string IRolloverConditions.MaxSize { get; set; }

		/// <inheritdoc cref="IRolloverConditions.MaxAge" />
		public RolloverConditionsDescriptor MaxAge(Time maxAge) => Assign(a => a.MaxAge = maxAge);

		/// <inheritdoc cref="IRolloverConditions.MaxDocs" />
		public RolloverConditionsDescriptor MaxDocs(long? maxDocs) => Assign(a => a.MaxDocs = maxDocs);

		/// <inheritdoc cref="IRolloverConditions.MaxSize" />
		public RolloverConditionsDescriptor MaxSize(string maxSize) => Assign(a => a.MaxSize = maxSize);
	}
}
