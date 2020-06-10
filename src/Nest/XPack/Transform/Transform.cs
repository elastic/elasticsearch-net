using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A transform.
	/// </summary>
	public class Transform
	{
		/// <summary>
		/// The identifier for the transform
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Free text description of the transform.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <inheritdoc cref="TransformSource"/>
		[DataMember(Name = "source")]
		public ITransformSource Source { get; set; }

		/// <inheritdoc cref="TransformDestination"/>
		[DataMember(Name = "dest")]
		public ITransformDestination Destination { get; set; }

		/// <summary>
		/// The interval between checks for changes in the source indices when the transform is running continuously.
		/// Also determines the retry interval in the event of transient failures while the transform is searching or indexing.
		/// The minimum value is 1s and the maximum is 1h. The default value is 1m.
		/// </summary>
		[DataMember(Name = "frequency")]
		public Time Frequency { get; set; }

		/// <inheritdoc cref="TransformPivot"/>
		[DataMember(Name = "pivot")]
		public ITransformPivot Pivot { get; set; }

		/// <inheritdoc cref="ITransformSyncContainer"/>
		[DataMember(Name = "sync")]
		public ITransformSyncContainer Sync { get; set; }

		/// <inheritdoc cref="ITransformSettings"/>
		[DataMember(Name = "settings")]
		public ITransformSettings Settings { get; set; }
	}
}
