// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Utility class for converting between epoch time and <see cref="DateTimeOffset"/> objects.
/// </summary>
internal static class DateTimeHelper
{
	private const int DaysPerYear = 365;
	private const int DaysPer4Years = DaysPerYear * 4 + 1; // 1461
	private const int DaysPer100Years = DaysPer4Years * 25 - 1; // 36524
	private const int DaysPer400Years = DaysPer100Years * 4 + 1; // 146097
	private const int DaysTo1970 = DaysPer400Years * 4 + DaysPer100Years * 3 + DaysPer4Years * 17 + DaysPerYear; // 719,162
	private const int DaysTo10000 = DaysPer400Years * 25 - 366; // 3652059

	private const long DateTimeMinTicks = 0;
	private const long DateTimeMaxTicks = DaysTo10000 * TimeSpan.TicksPerDay - 1;
	private const long DateTimeUnixEpochTicks = DaysTo1970 * TimeSpan.TicksPerDay;

	private const long UnixMinSeconds = DateTimeMinTicks / TimeSpan.TicksPerSecond - UnixEpochSeconds;
	private const long UnixMaxSeconds = DateTimeMaxTicks / TimeSpan.TicksPerSecond - UnixEpochSeconds;
	private const long UnixEpochSeconds = DateTimeUnixEpochTicks / TimeSpan.TicksPerSecond; // 62,135,596,800
	private const long UnixEpochMilliseconds = DateTimeUnixEpochTicks / TimeSpan.TicksPerMillisecond; // 62,135,596,800,000
	private const long UnixEpochNanoseconds = DateTimeUnixEpochTicks / TicksPerNanosecond;

	private const long TicksPerNanosecond = 100;

	/// <summary>
	/// Converts epoch time in milliseconds to a UTC <see cref="DateTimeOffset"/> object.
	/// </summary>
	/// <param name="milliseconds">The number of milliseconds since Unix epoch (<c>January 1st, 1970, 00:00:00 UTC</c>).</param>
	/// <returns>A <see cref="DateTimeOffset"/> object representing the specified epoch time.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when the milliseconds value would result in a <see cref="DateTimeOffset"/> outside the valid range.
	/// </exception>
	public static DateTimeOffset FromEpochMilliseconds(long milliseconds)
	{
		const long minMilliseconds = DateTimeMinTicks / TimeSpan.TicksPerMillisecond - UnixEpochMilliseconds;
		const long maxMilliseconds = DateTimeMaxTicks / TimeSpan.TicksPerMillisecond - UnixEpochMilliseconds;

		if (milliseconds is < minMilliseconds or > maxMilliseconds)
		{
			throw new ArgumentOutOfRangeException(nameof(milliseconds));
		}

		var ticks = milliseconds * TimeSpan.TicksPerMillisecond + DateTimeUnixEpochTicks;
		return new DateTimeOffset(ticks, TimeSpan.Zero);
	}

	/// <summary>
	/// Converts a <see cref="DateTimeOffset"/> to epoch time in milliseconds.
	/// </summary>
	/// <param name="dateTime">The <see cref="DateTimeOffset"/> to convert.</param>
	/// <returns>The number of milliseconds since Unix epoch.</returns>
	public static long ToEpochMilliseconds(DateTimeOffset dateTime)
	{
		var milliseconds = dateTime.UtcDateTime.Ticks / TimeSpan.TicksPerMillisecond;
		return milliseconds - UnixEpochMilliseconds;
	}

	/// <summary>
	/// Converts epoch time in seconds to a <see cref="DateTimeOffset"/> object.
	/// </summary>
	/// <param name="seconds">The number of seconds since Unix epoch (<c>January 1st, 1970, 00:00:00 UTC</c>).</param>
	/// <returns>A <see cref="DateTimeOffset"/> object representing the specified epoch time.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when the seconds value would result in a <see cref="DateTimeOffset"/> outside the valid range.
	/// </exception>
	public static DateTimeOffset FromEpochSeconds(long seconds)
	{
		if (seconds is < UnixMinSeconds or > UnixMaxSeconds)
		{
			throw new ArgumentOutOfRangeException(nameof(seconds));
		}

		var ticks = seconds * TimeSpan.TicksPerSecond + DateTimeUnixEpochTicks;
		return new DateTimeOffset(ticks, TimeSpan.Zero);
	}

	/// <summary>
	/// Converts a <see cref="DateTimeOffset"/> to epoch time in seconds.
	/// </summary>
	/// <param name="dateTime">The <see cref="DateTimeOffset"/> to convert.</param>
	/// <returns>The number of seconds since Unix epoch.</returns>
	public static long ToEpochSeconds(DateTimeOffset dateTime)
	{
		var seconds = dateTime.UtcDateTime.Ticks / TimeSpan.TicksPerSecond;
		return seconds - UnixEpochSeconds;
	}

	/// <summary>
	/// Converts epoch time in nanoseconds to a <see cref="DateTimeOffset"/> object.
	/// </summary>
	/// <param name="nanoseconds">The number of nanoseconds since Unix epoch (<c>January 1st, 1970, 00:00:00 UTC</c>).</param>
	/// <returns>A <see cref="DateTimeOffset"/> object representing the specified epoch time,</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when the nanoseconds value would result in a <see cref="DateTimeOffset"/> outside the valid range.
	/// </exception>
	public static DateTimeOffset FromEpochNanoseconds(long nanoseconds)
	{
		const long minNanoseconds = DateTimeMinTicks / TicksPerNanosecond - UnixEpochNanoseconds;
		const long maxNanoseconds = DateTimeMaxTicks / TicksPerNanosecond - UnixEpochNanoseconds;

		if (nanoseconds is < minNanoseconds or > maxNanoseconds)
		{
			throw new ArgumentOutOfRangeException(nameof(nanoseconds));
		}

		var ticks = nanoseconds * TicksPerNanosecond + DateTimeUnixEpochTicks;
		return new DateTimeOffset(ticks, TimeSpan.Zero);
	}

	/// <summary>
	/// Converts a <see cref="DateTimeOffset"/> to epoch time in nanoseconds.
	/// </summary>
	/// <param name="dateTime">The <see cref="DateTimeOffset"/> to convert.</param>
	/// <returns>The number of nanoseconds since Unix epoch.</returns>
	public static long ToEpochNanoseconds(DateTimeOffset dateTime)
	{
		var seconds = dateTime.UtcDateTime.Ticks / TicksPerNanosecond;
		return seconds - UnixEpochNanoseconds;
	}
}
