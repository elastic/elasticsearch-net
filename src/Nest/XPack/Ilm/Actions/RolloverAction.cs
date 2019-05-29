using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The Rollover Action rolls an alias over to a new index when the existing index meets one of the rollover conditions.
	/// </summary>
	/// <remarks>
	/// Phases allowed: hot.
	/// </remarks>
	public interface IRolloverLifecycleAction : ILifecycleAction
	{
		/// <summary>
		/// Max time elapsed from index creation.
		/// </summary>
		[JsonProperty("max_age")]
		Time MaximumAge { get; set; }

		/// <summary>
		/// Max number of documents an index is to contain before rolling over.
		/// </summary>
		[JsonProperty("max_docs")]
		long? MaximumDocuments { get; set; }

		/// <summary>
		/// Max primary shard index storage size using byte notation (e.g. 40gb or 40000000000b).
		/// </summary>
		[JsonProperty("max_size")]
		string MaximumSizeAsString { get; set; }

		/// <summary>
		/// Max primary shard index storage size in bytes.
		/// </summary>
		[JsonIgnore]
		[Obsolete("Use MaximumSizeAsString property instead")]
		long? MaximumSize { get; set; }
	}

	public class RolloverLifecycleAction : IRolloverLifecycleAction
	{
		/// <inheritdoc />
		public Time MaximumAge { get; set; }

		/// <inheritdoc />
		public long? MaximumDocuments { get; set; }

		/// <inheritdoc />
		public string MaximumSizeAsString { get; set; }

		/// <inheritdoc />
		[Obsolete("Use MaximumSizeAsString property instead")]
		public long? MaximumSize
		{
			get => BytesValueConverter.ToBytes(MaximumSizeAsString);
			set
			{
				if (value == null)
				{
					MaximumSizeAsString = null;
					return;
				}
				MaximumSizeAsString = value + "b";
			}
		}
	}

	public static class BytesValueConverter
	{
		public static long? ToBytes(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}

			if (value.EndsWith("pb")) return Convert.ToInt64(value.Substring(0, value.Length - 2)) * 1_125_899_906_842_624;
			if (value.EndsWith("tb")) return Convert.ToInt64(value.Substring(0, value.Length - 2)) * 1_099_511_627_776;
			if (value.EndsWith("gb")) return Convert.ToInt64(value.Substring(0, value.Length - 2)) * 1_073_741_824;
			if (value.EndsWith("mb")) return Convert.ToInt64(value.Substring(0, value.Length - 2)) * 1_048_576;
			if (value.EndsWith("kb")) return Convert.ToInt64(value.Substring(0, value.Length - 2)) * 1_024;

			if (value.EndsWith("p")) return Convert.ToInt64(value.Substring(0, value.Length - 1)) * 1_125_899_906_842_624;
			if (value.EndsWith("t")) return Convert.ToInt64(value.Substring(0, value.Length - 1)) * 1_099_511_627_776;
			if (value.EndsWith("g")) return Convert.ToInt64(value.Substring(0, value.Length - 1)) * 1_073_741_824;
			if (value.EndsWith("m")) return Convert.ToInt64(value.Substring(0, value.Length - 1)) * 1_048_576;
			if (value.EndsWith("k")) return Convert.ToInt64(value.Substring(0, value.Length - 1)) * 1_024;

			if (value.EndsWith("b")) return Convert.ToInt64(value.Substring(0, value.Length - 1));

			// Assume bytes
			return Convert.ToInt64(value);
		}
	}

	public class RolloverLifecycleActionDescriptor
		: DescriptorBase<RolloverLifecycleActionDescriptor, IRolloverLifecycleAction>, IRolloverLifecycleAction
	{
		private string _maximumSizeAsString;

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumAge" />
		Time IRolloverLifecycleAction.MaximumAge { get; set; }

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumDocuments" />
		long? IRolloverLifecycleAction.MaximumDocuments { get; set; }

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumSizeAsString" />
		string IRolloverLifecycleAction.MaximumSizeAsString { get => _maximumSizeAsString; set => _maximumSizeAsString = value; }

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumSize" />
		[Obsolete("Use MaximumSizeAsString property instead")]
		long? IRolloverLifecycleAction.MaximumSize
		{
			get => BytesValueConverter.ToBytes(_maximumSizeAsString);
			set
			{
				if (value == null)
				{
					_maximumSizeAsString = null;
					return;
				}
				_maximumSizeAsString = value + "b";
			}
		}

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumSizeAsString" />
		public RolloverLifecycleActionDescriptor MaximumSizeAsString(string maximumSizeAsString) => Assign(maximumSizeAsString, (a, v) => a.MaximumSizeAsString = maximumSizeAsString);

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumSize" />
		[Obsolete("Use MaximumSizeAsString property instead")]
#pragma warning disable 618
		public RolloverLifecycleActionDescriptor MaximumSize(long? maximumSize) => Assign(maximumSize, (a, v) => a.MaximumSize = maximumSize);
#pragma warning restore 618

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumAge" />
		public RolloverLifecycleActionDescriptor MaximumAge(Time maximumAge) => Assign(maximumAge, (a, v) => a.MaximumAge = maximumAge);

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumDocuments" />
		public RolloverLifecycleActionDescriptor MaximumDocuments(long? maximumDocuments)
			=> Assign(maximumDocuments, (a, v) => a.MaximumDocuments = maximumDocuments);
	}
}
