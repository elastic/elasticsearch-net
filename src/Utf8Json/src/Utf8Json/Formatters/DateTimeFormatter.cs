using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Utf8Json.Internal;

namespace Utf8Json.Formatters
{
    public sealed class DateTimeFormatter : IJsonFormatter<DateTime>
    {
        readonly string formatString;

        public DateTimeFormatter()
        {
            this.formatString = null;
        }

        public DateTimeFormatter(string formatString)
        {
            this.formatString = formatString;
        }

        public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString(value.ToString(formatString));
        }

        public DateTime Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var str = reader.ReadString();
            if (formatString == null)
            {
                return DateTime.Parse(str, CultureInfo.InvariantCulture);
            }
            else
            {
                return DateTime.ParseExact(str, formatString, CultureInfo.InvariantCulture);
            }
        }
    }

    public sealed class NullableDateTimeFormatter : IJsonFormatter<DateTime?>
    {
        readonly DateTimeFormatter innerFormatter;

        public NullableDateTimeFormatter()
        {
            this.innerFormatter = new DateTimeFormatter();
        }

        public NullableDateTimeFormatter(string formatString)
        {
            this.innerFormatter = new DateTimeFormatter(formatString);
        }

        public void Serialize(ref JsonWriter writer, DateTime? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            innerFormatter.Serialize(ref writer, value.Value, formatterResolver);
        }

        public DateTime? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            return innerFormatter.Deserialize(ref reader, formatterResolver);
        }
    }

    public sealed class ISO8601DateTimeFormatter : IJsonFormatter<DateTime>
    {
        public static readonly IJsonFormatter<DateTime> Default = new ISO8601DateTimeFormatter();

        public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver)
        {
            var year = value.Year;
            var month = value.Month;
            var day = value.Day;
            var hour = value.Hour;
            var minute = value.Minute;
            var second = value.Second;
            var nanosec = value.Ticks % TimeSpan.TicksPerSecond;

            const int baseLength = 19 + 2; // {YEAR}-{MONTH}-{DAY}T{Hour}:{Minute}:{Second} + quotation
            const int nanosecLength = 8; // .{nanoseconds}

            switch (value.Kind)
            {
                case DateTimeKind.Local:
                    // +{Hour}:{Minute}
                    writer.EnsureCapacity(baseLength + ((nanosec == 0) ? 0 : nanosecLength) + 6);
                    break;
                case DateTimeKind.Utc:
                    // Z
                    writer.EnsureCapacity(baseLength + ((nanosec == 0) ? 0 : nanosecLength) + 1);
                    break;
                case DateTimeKind.Unspecified:
                default:
                    writer.EnsureCapacity(baseLength + ((nanosec == 0) ? 0 : nanosecLength));
                    break;
            }

            writer.WriteRawUnsafe((byte)'\"');

            if (year < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
                writer.WriteRawUnsafe((byte)'0');
                writer.WriteRawUnsafe((byte)'0');
            }
            else if (year < 100)
            {
                writer.WriteRawUnsafe((byte)'0');
                writer.WriteRawUnsafe((byte)'0');
            }
            else if (year < 1000)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(year);
            writer.WriteRawUnsafe((byte)'-');

            if (month < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(month);
            writer.WriteRawUnsafe((byte)'-');

            if (day < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(day);

            writer.WriteRawUnsafe((byte)'T');

            if (hour < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(hour);
            writer.WriteRawUnsafe((byte)':');

            if (minute < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(minute);
            writer.WriteRawUnsafe((byte)':');

            if (second < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(second);

            if (nanosec != 0)
            {
                writer.WriteRawUnsafe((byte)'.');

                if (nanosec < 10)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 100)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 1000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 10000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 100000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 1000000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                }

                writer.WriteInt64(nanosec);
            }

            switch (value.Kind)
            {
                case DateTimeKind.Local:
                    // should not use `BaseUtcOffset` - https://stackoverflow.com/questions/10019267/is-there-a-generic-timezoneinfo-for-central-europe
                    var localOffset = TimeZoneInfo.Local.GetUtcOffset(value);
                    var minus = (localOffset < TimeSpan.Zero);
                    if (minus) localOffset = localOffset.Negate();
                    var h = localOffset.Hours;
                    var m = localOffset.Minutes;
                    writer.WriteRawUnsafe(minus ? (byte)'-' : (byte)'+');
                    if (h < 10)
                    {
                        writer.WriteRawUnsafe((byte)'0');
                    }
                    writer.WriteInt32(h);
                    writer.WriteRawUnsafe((byte)':');
                    if (m < 10)
                    {
                        writer.WriteRawUnsafe((byte)'0');
                    }
                    writer.WriteInt32(m);
                    break;
                case DateTimeKind.Utc:
                    writer.WriteRawUnsafe((byte)'Z');
                    break;
                case DateTimeKind.Unspecified:
                default:
                    break;
            }

            writer.WriteRawUnsafe((byte)'\"');
        }

        public DateTime Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var str = reader.ReadStringSegmentUnsafe();
            var array = str.Array;
            var i = str.Offset;
            var len = str.Count;
            var to = str.Offset + str.Count;

            // YYYY
            if (len == 4)
            {
                var y = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                return new DateTime(y, 1, 1);
            }

            // YYYY-MM
            if (len == 7)
            {
                var y = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                if (array[i++] != (byte)'-') goto ERROR;
                var m = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                return new DateTime(y, m, 1);
            }

            // YYYY-MM-DD
            if (len == 10)
            {
                var y = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                if (array[i++] != (byte)'-') goto ERROR;
                var m = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                if (array[i++] != (byte)'-') goto ERROR;
                var d = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                return new DateTime(y, m, d);
            }

            // range-first section requires 19
            if (len < 19) goto ERROR;

            var year = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)'-') goto ERROR;
            var month = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)'-') goto ERROR;
            var day = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');

            if (array[i++] != (byte)'T') goto ERROR;

            var hour = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)':') goto ERROR;
            var minute = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)':') goto ERROR;
            var second = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');

            int ticks = 0;
            if (i < to && array[i] == '.')
            {
                i++;

                // *7.
                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1000000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 100000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 10000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 100;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 10;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1;
                i++;

                // others, lack of precision
                while (i < to && NumberConverter.IsNumber(array[i]))
                {
                    i++;
                }
            }

            END_TICKS:
            var kind = DateTimeKind.Unspecified;
            if (i < to && array[i] == 'Z')
            {
                kind = DateTimeKind.Utc;
            }
            else if (i < to && array[i] == '-' || array[i] == '+')
            {
                if (!(i + 5 < to)) goto ERROR;

                kind = DateTimeKind.Local;
                var minus = array[i++] == '-';

                var h = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                i++;
                var m = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');

                var offset = new TimeSpan(h, m, 0);
                if (minus) offset = offset.Negate();

                return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc).AddTicks(ticks).Subtract(offset).ToLocalTime();
            }

            return new DateTime(year, month, day, hour, minute, second, kind).AddTicks(ticks);

            ERROR:
            throw new InvalidOperationException("invalid datetime format. value:" + StringEncoding.UTF8.GetString(str.Array, str.Offset, str.Count));
        }
    }

    public sealed class UnixTimestampDateTimeFormatter : IJsonFormatter<DateTime>
    {
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver)
        {
            var ticks = (long)(value.ToUniversalTime() - UnixEpoch).TotalSeconds;
            writer.WriteQuotation();
            writer.WriteInt64(ticks);
            writer.WriteQuotation();
        }

        public DateTime Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var str = reader.ReadStringSegmentUnsafe();
            int readCount;
            var ticks = NumberConverter.ReadUInt64(str.Array, str.Offset, out readCount);

            return UnixEpoch.AddSeconds(ticks);
        }
    }

    public sealed class DateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset>
    {
        readonly string formatString;

        public DateTimeOffsetFormatter()
        {
            this.formatString = null;
        }

        public DateTimeOffsetFormatter(string formatString)
        {
            this.formatString = formatString;
        }

        public void Serialize(ref JsonWriter writer, DateTimeOffset value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString(value.ToString(formatString));
        }

        public DateTimeOffset Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var str = reader.ReadString();
            if (formatString == null)
            {
                return DateTimeOffset.Parse(str, CultureInfo.InvariantCulture);
            }
            else
            {
                return DateTimeOffset.ParseExact(str, formatString, CultureInfo.InvariantCulture);
            }
        }
    }

    public sealed class NullableDateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset?>
    {
        readonly DateTimeOffsetFormatter innerFormatter;

        public NullableDateTimeOffsetFormatter()
        {
            this.innerFormatter = new DateTimeOffsetFormatter();
        }

        public NullableDateTimeOffsetFormatter(string formatString)
        {
            this.innerFormatter = new DateTimeOffsetFormatter(formatString);
        }

        public void Serialize(ref JsonWriter writer, DateTimeOffset? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            innerFormatter.Serialize(ref writer, value.Value, formatterResolver);
        }

        public DateTimeOffset? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            return innerFormatter.Deserialize(ref reader, formatterResolver);
        }
    }

    public sealed class ISO8601DateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset>
    {
        public static readonly IJsonFormatter<DateTimeOffset> Default = new ISO8601DateTimeOffsetFormatter();

        public void Serialize(ref JsonWriter writer, DateTimeOffset value, IJsonFormatterResolver formatterResolver)
        {
            var year = value.Year;
            var month = value.Month;
            var day = value.Day;
            var hour = value.Hour;
            var minute = value.Minute;
            var second = value.Second;
            var nanosec = value.Ticks % TimeSpan.TicksPerSecond;

            const int baseLength = 19 + 2; // {YEAR}-{MONTH}-{DAY}T{Hour}:{Minute}:{Second} + quotation
            const int nanosecLength = 8; // .{nanoseconds}

            // +{Hour}:{Minute}
            writer.EnsureCapacity(baseLength + ((nanosec == 0) ? 0 : nanosecLength) + 6);

            writer.WriteRawUnsafe((byte)'\"');

            if (year < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
                writer.WriteRawUnsafe((byte)'0');
                writer.WriteRawUnsafe((byte)'0');
            }
            else if (year < 100)
            {
                writer.WriteRawUnsafe((byte)'0');
                writer.WriteRawUnsafe((byte)'0');
            }
            else if (year < 1000)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(year);
            writer.WriteRawUnsafe((byte)'-');

            if (month < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(month);
            writer.WriteRawUnsafe((byte)'-');

            if (day < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(day);

            writer.WriteRawUnsafe((byte)'T');

            if (hour < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(hour);
            writer.WriteRawUnsafe((byte)':');

            if (minute < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(minute);
            writer.WriteRawUnsafe((byte)':');

            if (second < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(second);

            if (nanosec != 0)
            {
                writer.WriteRawUnsafe((byte)'.');

                if (nanosec < 10)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 100)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 1000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 10000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 100000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                    writer.WriteRawUnsafe((byte)'0');
                }
                else if (nanosec < 1000000)
                {
                    writer.WriteRawUnsafe((byte)'0');
                }

                writer.WriteInt64(nanosec);
            }

            var localOffset = value.Offset;
            var minus = (localOffset < TimeSpan.Zero);
            if (minus) localOffset = localOffset.Negate();
            var h = localOffset.Hours;
            var m = localOffset.Minutes;
            writer.WriteRawUnsafe(minus ? (byte)'-' : (byte)'+');
            if (h < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(h);
            writer.WriteRawUnsafe((byte)':');
            if (m < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(m);

            writer.WriteRawUnsafe((byte)'\"');
        }

        public DateTimeOffset Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var str = reader.ReadStringSegmentUnsafe();
            var array = str.Array;
            var i = str.Offset;
            var len = str.Count;
            var to = str.Offset + str.Count;

            // YYYY
            if (len == 4)
            {
                var y = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                return new DateTimeOffset(y, 1, 1, 0, 0, 0, TimeSpan.Zero);
            }

            // YYYY-MM
            if (len == 7)
            {
                var y = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                if (array[i++] != (byte)'-') goto ERROR;
                var m = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                return new DateTimeOffset(y, m, 1, 0, 0, 0, TimeSpan.Zero);
            }

            // YYYY-MM-DD
            if (len == 10)
            {
                var y = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                if (array[i++] != (byte)'-') goto ERROR;
                var m = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                if (array[i++] != (byte)'-') goto ERROR;
                var d = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                return new DateTimeOffset(y, m, d, 0, 0, 0, TimeSpan.Zero);
            }

            // range-first section requires 19
            if (array.Length < 19) goto ERROR;

            var year = (array[i++] - (byte)'0') * 1000 + (array[i++] - (byte)'0') * 100 + (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)'-') goto ERROR;
            var month = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)'-') goto ERROR;
            var day = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');

            if (array[i++] != (byte)'T') goto ERROR;

            var hour = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)':') goto ERROR;
            var minute = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)':') goto ERROR;
            var second = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');

            int ticks = 0;
            if (i < to && array[i] == '.')
            {
                i++;

                // *7.
                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1000000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 100000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 10000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 100;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 10;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1;
                i++;

                // others, lack of precision
                while (i < to && NumberConverter.IsNumber(array[i]))
                {
                    i++;
                }
            }

            END_TICKS:

            if (i < to && array[i] == '-' || array[i] == '+')
            {
                if (!(i + 5 < to)) goto ERROR;

                var minus = array[i++] == '-';

                var h = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
                i++;
                var m = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');

                var offset = new TimeSpan(h, m, 0);
                if (minus) offset = offset.Negate();

                return new DateTimeOffset(year, month, day, hour, minute, second, offset).AddTicks(ticks);
            }

            return new DateTimeOffset(year, month, day, hour, minute, second, TimeSpan.Zero).AddTicks(ticks);

            ERROR:
            throw new InvalidOperationException("invalid datetime format. value:" + StringEncoding.UTF8.GetString(str.Array, str.Offset, str.Count));
        }
    }

    public sealed class TimeSpanFormatter : IJsonFormatter<TimeSpan>
    {
#if NETSTANDARD
        readonly string formatString;

        public TimeSpanFormatter()
        {
            this.formatString = null;
        }

        public TimeSpanFormatter(string formatString)
        {
            this.formatString = formatString;
        }
#endif

        public void Serialize(ref JsonWriter writer, TimeSpan value, IJsonFormatterResolver formatterResolver)
        {
#if NETSTANDARD
            writer.WriteString(value.ToString(formatString));
#else
            writer.WriteString(value.ToString());
#endif
        }

        public TimeSpan Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var str = reader.ReadString();
#if NETSTANDARD
            if (formatString == null)
            {
                return TimeSpan.Parse(str, CultureInfo.InvariantCulture);
            }
            else
            {
                return TimeSpan.ParseExact(str, formatString, CultureInfo.InvariantCulture);
            }
#else
            return TimeSpan.Parse(str);
#endif
        }
    }

    public sealed class NullableTimeSpanFormatter : IJsonFormatter<TimeSpan?>
    {
        readonly TimeSpanFormatter innerFormatter;

        public NullableTimeSpanFormatter()
        {
            this.innerFormatter = new TimeSpanFormatter();
        }

#if NETSTANDARD
        public NullableTimeSpanFormatter(string formatString)
        {
            this.innerFormatter = new TimeSpanFormatter(formatString);
        }
#endif

        public void Serialize(ref JsonWriter writer, TimeSpan? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            innerFormatter.Serialize(ref writer, value.Value, formatterResolver);
        }

        public TimeSpan? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            return innerFormatter.Deserialize(ref reader, formatterResolver);
        }
    }

    public sealed class ISO8601TimeSpanFormatter : IJsonFormatter<TimeSpan>
    {
        public static readonly IJsonFormatter<TimeSpan> Default = new ISO8601TimeSpanFormatter();

        static byte[] minValue = StringEncoding.UTF8.GetBytes("\"" + TimeSpan.MinValue.ToString() + "\"");

        public void Serialize(ref JsonWriter writer, TimeSpan value, IJsonFormatterResolver formatterResolver)
        {
            // can not negate, use cache
            if (value == TimeSpan.MinValue)
            {
                writer.WriteRaw(minValue);
                return;
            }

            var minus = value < TimeSpan.Zero;
            if (minus) value = value.Negate();
            var day = value.Days;
            var hour = value.Hours;
            var minute = value.Minutes;
            var second = value.Seconds;
            var nanosecond = value.Ticks % TimeSpan.TicksPerSecond;

            const int maxDayLength = 8 + 1; // {Day}.
            const int baseLength = 8 + 2; // {Hour}:{Minute}:{Second} + quotation
            const int nanosecLength = 8; // .{nanoseconds}

            writer.EnsureCapacity(baseLength + ((maxDayLength == 0) ? 0 : maxDayLength) + ((nanosecond == 0) ? 0 : nanosecLength) + 6);

            writer.WriteRawUnsafe((byte)'\"');

            if (minus)
            {
                writer.WriteRawUnsafe((byte)'-');
            }

            if (day != 0)
            {
                writer.WriteInt32(day);
                writer.WriteRawUnsafe((byte)'.');
            }

            if (hour < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(hour);
            writer.WriteRawUnsafe((byte)':');

            if (minute < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(minute);
            writer.WriteRawUnsafe((byte)':');

            if (second < 10)
            {
                writer.WriteRawUnsafe((byte)'0');
            }
            writer.WriteInt32(second);

            if (nanosecond != 0)
            {
                writer.WriteRawUnsafe((byte)'.');
                writer.WriteInt64(nanosecond);
            }

            writer.WriteRawUnsafe((byte)'\"');
        }

        public TimeSpan Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var str = reader.ReadStringSegmentUnsafe();
            var array = str.Array;
            var i = str.Offset;
            var len = str.Count;
            var to = str.Offset + str.Count;

            // check day exists
            bool hasDay = false;
            {
                bool foundDot = false;
                bool foundColon = false;
                for (int j = i; j < str.Count; j++)
                {
                    if (array[j] == '.')
                    {
                        if (foundColon)
                        {
                            break;
                        }
                        foundDot = true;
                    }
                    else if (array[j] == ':')
                    {
                        if (foundDot)
                        {
                            hasDay = true;
                        }
                        foundColon = true;
                    }
                }
            }

            // check sign
            var minus = false;
            if (array[i] == '-')
            {
                minus = true;
                i++;
            }

            var day = 0;
            if (hasDay)
            {
                var poolArray = BufferPool.Default.Rent();
                try
                {
                    for (; array[i] != '.'; i++)
                    {
                        poolArray[day++] = array[i];
                    }
                    day = new JsonReader(poolArray).ReadInt32();
                    i++; // skip '.'
                }
                finally
                {
                    BufferPool.Default.Return(poolArray);
                }
            }

            var hour = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)':') goto ERROR;
            var minute = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');
            if (array[i++] != (byte)':') goto ERROR;
            var second = (array[i++] - (byte)'0') * 10 + (array[i++] - (byte)'0');

            int ticks = 0;
            if (i < to && array[i] == '.')
            {
                i++;

                // *7.
                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1000000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 100000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 10000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1000;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 100;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 10;
                i++;

                if (!(i < to) || !NumberConverter.IsNumber(array[i])) goto END_TICKS;
                ticks += (array[i] - (byte)'0') * 1;
                i++;

                // others, lack of precision
                while (i < to && NumberConverter.IsNumber(array[i]))
                {
                    i++;
                }
            }

            END_TICKS:

            // be careful to overflow
            var ts = new TimeSpan(day, hour, minute, second);
            var tk = TimeSpan.FromTicks(ticks);
            return (minus)
                ? ts.Negate().Subtract(tk)
                : ts.Add(tk);

            ERROR:
            throw new InvalidOperationException("invalid datetime format. value:" + StringEncoding.UTF8.GetString(str.Array, str.Offset, str.Count));
        }
    }
}
