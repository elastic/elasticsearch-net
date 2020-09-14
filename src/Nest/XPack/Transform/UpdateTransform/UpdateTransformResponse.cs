using System;
using System.Runtime.Serialization;

namespace Nest
{
	public class UpdateTransformResponse : ResponseBase
	{
		/// <summary>
		/// The identifier for the transform
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		/// <summary>
		/// Free text description of the transform.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; internal set; }

		/// <inheritdoc cref="TransformSource"/>
		[DataMember(Name = "source")]
		public ITransformSource Source { get; internal set; }

		/// <inheritdoc cref="TransformDestination"/>
		[DataMember(Name = "dest")]
		public ITransformDestination Destination { get; internal set; }

		/// <summary>
		/// The interval between checks for changes in the source indices when the transform is running continuously.
		/// Also determines the retry interval in the event of transient failures while the transform is searching or indexing.
		/// The minimum value is 1s and the maximum is 1h. The default value is 1m.
		/// </summary>
		[DataMember(Name = "frequency")]
		public Time Frequency { get; internal set; }

		/// <inheritdoc cref="TransformPivot"/>
		[DataMember(Name = "pivot")]
		public ITransformPivot Pivot { get; internal set; }

		[DataMember(Name = "sync")]
		public ITransformSyncContainer Sync { get; internal set; }

		/// <summary>
		/// The version
		/// </summary>
		[DataMember(Name = "version")]
		public string Version { get; internal set; }

		/// <summary>
		/// The creation time in milliseconds since epoch
		/// </summary>
		[DataMember(Name = "create_time")]
		public long CreateTime { get; internal set; }

		/// <summary>
		/// The creation time
		/// </summary>
		[IgnoreDataMember]
		public DateTimeOffset CreateTimeDateTime => DateTimeUtil.UnixEpoch.AddMilliseconds(CreateTime);
	}
}
