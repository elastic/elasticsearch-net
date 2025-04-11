// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Serialization;

public static class TimeSpanHelper
{
	// 1 Tick = 100 nanoseconds
	private const long NanosecondsPerTick = 100;

	/// <summary>
	/// Converts nanoseconds to a <see cref="TimeSpan"/> value.
	/// </summary>
	/// <param name="nanoseconds">The number of nanoseconds.</param>
	/// <returns>A <see cref="TimeSpan"/> representing the specified duration.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when the nanoseconds value would result in a <see cref="TimeSpan"/> outside the valid range.
	/// </exception>
	public static TimeSpan FromNanoseconds(long nanoseconds)
	{
		try
		{
			// Convert nanoseconds to ticks (1 tick = 100 nanoseconds).
			var ticks = nanoseconds / NanosecondsPerTick;

			// Handle remainder for higher precision.
			var remainderNanoseconds = nanoseconds % NanosecondsPerTick;
			if (remainderNanoseconds >= NanosecondsPerTick / 2)
			{
				ticks++;
			}

			return new TimeSpan(ticks);
		}
		catch (ArgumentOutOfRangeException ex)
		{
			throw new ArgumentOutOfRangeException(
				$"The nanoseconds value {nanoseconds} is outside the range of valid {nameof(TimeSpan)} values.", ex);
		}
	}

	/// <summary>
	/// Converts a <see cref="TimeSpan"/> to nanoseconds.
	/// </summary>
	/// <param name="timeSpan">The <see cref="TimeSpan"/> value to convert.</param>
	/// <returns>The equivalent number of nanoseconds.</returns>
	public static long ToNanoseconds(TimeSpan timeSpan)
	{
		return timeSpan.Ticks * NanosecondsPerTick;
	}
}
