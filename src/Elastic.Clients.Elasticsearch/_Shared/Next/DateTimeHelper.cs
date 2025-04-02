// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Utility class for converting between epoch time and DateTime objects.
/// </summary>
internal static class DateTimeHelper
{
	// Unix epoch starts at January 1st, 1970, 00:00:00 UTC.
	private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	// 1 Tick = 100 nanoseconds
	private const long TicksPerNanosecond = 100;

	/// <summary>
	/// Converts epoch time in milliseconds to a UTC <see cref="DateTime"/> object.
	/// </summary>
	/// <param name="milliseconds">The number of milliseconds since Unix epoch (<c>January 1st, 1970, 00:00:00 UTC</c>).</param>
	/// <returns>A <see cref="DateTime"/> object representing the specified epoch time.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when the milliseconds value would result in a <see cref="DateTime"/> outside the valid range.
	/// </exception>
	public static DateTime FromEpochMilliseconds(long milliseconds)
	{
		try
		{
#if NET6_0_OR_GREATER
			return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;
#else
			return Epoch.AddMilliseconds(milliseconds);
#endif
		}
		catch (ArgumentOutOfRangeException ex)
		{
			throw new ArgumentOutOfRangeException(
				$"The epoch milliseconds value {milliseconds} is outside the range of valid '{nameof(DateTime)}' values.", ex);
		}
	}

	/// <summary>
	/// Converts a <see cref="DateTime"/> to epoch time in milliseconds.
	/// </summary>
	/// <param name="dateTime">The <see cref="DateTime"/> to convert (will be treated as UTC if <see cref="DateTime.Kind"/> is unspecified).</param>
	/// <returns>The number of milliseconds since Unix epoch.</returns>
	public static long ToEpochMilliseconds(DateTime dateTime)
	{
		var utcDateTime = dateTime.Kind is DateTimeKind.Unspecified
			? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
			: dateTime.ToUniversalTime();

#if NET6_0_OR_GREATER
		return new DateTimeOffset(utcDateTime).ToUnixTimeMilliseconds();
#else
		var delta = utcDateTime - Epoch;

		return (long)delta.TotalMilliseconds;
#endif
	}

	/// <summary>
	/// Converts epoch time in seconds to a UTC <see cref="DateTime"/> object.
	/// </summary>
	/// <param name="seconds">The number of seconds since Unix epoch (<c>January 1st, 1970, 00:00:00 UTC</c>).</param>
	/// <returns>A <see cref="DateTime"/> object representing the specified epoch time.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when the seconds value would result in a <see cref="DateTime"/> outside the valid range.
	/// </exception>
	public static DateTime FromEpochSeconds(long seconds)
	{
		try
		{
#if NET6_0_OR_GREATER
			return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
#else
			return Epoch.AddSeconds(seconds);
#endif
		}
		catch (ArgumentOutOfRangeException ex)
		{
			throw new ArgumentOutOfRangeException(
				$"The epoch seconds value {seconds} is outside the range of valid {nameof(DateTime)} values.", ex);
		}
	}

	/// <summary>
	/// Converts a <see cref="DateTime"/> to epoch time in seconds.
	/// </summary>
	/// <param name="dateTime">The DateTime to convert (will be treated as UTC if <see cref="DateTime.Kind"/> is unspecified).</param>
	/// <returns>The number of seconds since Unix epoch.</returns>
	public static long ToEpochSeconds(DateTime dateTime)
	{
		var utcDateTime = dateTime.Kind == DateTimeKind.Unspecified
			? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
			: dateTime.ToUniversalTime();

#if NET6_0_OR_GREATER
		return new DateTimeOffset(utcDateTime).ToUnixTimeSeconds();
#else
		var delta = utcDateTime - Epoch;

		return (long)delta.TotalSeconds;
#endif
	}

	/// <summary>
	/// Converts epoch time in nanoseconds to a UTC <see cref="DateTime"/> object.
	/// </summary>
	/// <param name="nanoseconds">The number of nanoseconds since Unix epoch (<c>January 1st, 1970, 00:00:00 UTC</c>).</param>
	/// <returns>A <see cref="DateTime"/> object representing the specified epoch time,</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when the nanoseconds value would result in a <see cref="DateTime"/> outside the valid range.
	/// </exception>
	public static DateTime FromEpochNanoseconds(long nanoseconds)
	{
		try
		{
			// Convert nanoseconds to ticks (1 tick = 100 nanoseconds).
			var ticks = nanoseconds / TicksPerNanosecond;

			// Handle remainder for higher precision.
			var remainderNanoseconds = nanoseconds % TicksPerNanosecond;
			if (remainderNanoseconds >= TicksPerNanosecond / 2)
			{
				ticks++;
			}

#if NET6_0_OR_GREATER
			return DateTime.UnixEpoch.AddTicks(ticks);
#else
			return Epoch.AddTicks(ticks);
#endif
		}
		catch (ArgumentOutOfRangeException ex)
		{
			throw new ArgumentOutOfRangeException(
				$"The epoch nanoseconds value {nanoseconds} is outside the range of valid {nameof(DateTime)} values.", ex);
		}
	}

	/// <summary>
	/// Converts a <see cref="DateTime"/> to epoch time in nanoseconds.
	/// </summary>
	/// <param name="dateTime">The <see cref="DateTime"/> to convert (will be treated as UTC if <see cref="DateTime.Kind"/> is unspecified).</param>
	/// <returns>The number of nanoseconds since Unix epoch.</returns>
	public static long ToEpochNanoseconds(DateTime dateTime)
	{
		var utcDateTime = dateTime.Kind == DateTimeKind.Unspecified
			? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
			: dateTime.ToUniversalTime();

#if NET6_0_OR_GREATER
		return (utcDateTime - DateTime.UnixEpoch).Ticks * TicksPerNanosecond;
#else
		var delta = utcDateTime - Epoch;

		return delta.Ticks * TicksPerNanosecond;
#endif
	}
}
