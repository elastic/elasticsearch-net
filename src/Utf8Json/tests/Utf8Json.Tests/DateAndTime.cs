using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Tests
{
    public class DateAndTime
    {
        [Fact]
        public void DateTimeOffsetTest()
        {
            DateTimeOffset now = new DateTime(DateTime.UtcNow.Ticks + TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time").BaseUtcOffset.Ticks, DateTimeKind.Local);
            var binary = JsonSerializer.Serialize(now);
            JsonSerializer.Deserialize<DateTimeOffset>(binary).Is(now);

            foreach (var item in new[] { TimeSpan.MaxValue, TimeSpan.MinValue, TimeSpan.MaxValue.Add(TimeSpan.FromTicks(-1)), TimeSpan.MinValue.Add(TimeSpan.FromTicks(1)), TimeSpan.Zero })
            {
                var ts = JsonSerializer.Deserialize<TimeSpan>(JsonSerializer.Serialize(item));
                ts.Is(item);
            }

            foreach (var item in new[] { DateTime.MaxValue, DateTime.MaxValue.AddTicks(-1), DateTime.MinValue.AddTicks(1) })
            {
                var ts = JsonSerializer.Deserialize<DateTime>(JsonSerializer.Serialize(item));
                ts.Is(item);
            }

            foreach (var item in new[] { DateTimeOffset.MinValue.ToUniversalTime(), DateTimeOffset.MaxValue.ToUniversalTime() })
            {
                var ts = JsonSerializer.Deserialize<DateTimeOffset>(JsonSerializer.Serialize(item));
                ts.Is(item);
            }

            foreach (var item in new[] { new DateTimeOffset(DateTime.MinValue.Ticks, new TimeSpan(-1, 0, 0)) })
            {
                var ts = JsonSerializer.Deserialize<DateTimeOffset>(JsonSerializer.Serialize(item));
                ts.Is(item);
            }
        }

        [Fact]
        public void Nullable()
        {
            DateTimeOffset? now = new DateTime(DateTime.UtcNow.Ticks + TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time").BaseUtcOffset.Ticks, DateTimeKind.Local);
            var binary = JsonSerializer.Serialize(now);
            JsonSerializer.Deserialize<DateTimeOffset?>(binary).ToString().Is(now.ToString());
        }

        [Fact]
        public void Misc()
        {
            {
                var dto = DateTime.UtcNow;
                var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
                var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTime>(serialized);
                var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);

                serialized2.Is(serialized);
            }

            {
                Console.WriteLine("DateTimeOffset.UtcNow");
                var dto = DateTimeOffset.UtcNow;
                var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
                var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTimeOffset>(serialized);
                var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);
                serialized2.Is(serialized);
            }

            {
                Console.WriteLine("DateTime.Now");
                var dto = DateTime.Now;
                var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
                var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTime>(serialized);
                var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);
                serialized2.Is(serialized);
            }


            {
                Console.WriteLine("DateTimeOffset.Now");
                var dto = DateTimeOffset.Now;
                var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
                var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTimeOffset>(serialized);
                var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);
                serialized2.Is(serialized);
            }
        }


        [Fact]
        public void ShortFormat()
        {
            JsonSerializer.Deserialize<DateTime>(JsonSerializer.Serialize("2017")).Is(new DateTime(2017, 1, 1));
            JsonSerializer.Deserialize<DateTime>(JsonSerializer.Serialize("2017-12")).Is(new DateTime(2017, 12, 1));
            JsonSerializer.Deserialize<DateTime>(JsonSerializer.Serialize("2017-12-30")).Is(new DateTime(2017, 12, 30));
            JsonSerializer.Deserialize<DateTimeOffset>(JsonSerializer.Serialize("2017")).Is(new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero));
            JsonSerializer.Deserialize<DateTimeOffset>(JsonSerializer.Serialize("2017-12")).Is(new DateTimeOffset(2017, 12, 1, 0, 0, 0, TimeSpan.Zero));
            JsonSerializer.Deserialize<DateTimeOffset>(JsonSerializer.Serialize("2017-12-30")).Is(new DateTimeOffset(2017, 12, 30, 0, 0, 0, TimeSpan.Zero));
        }

        [Fact]
        public void Offset()
        {
            DateTimeOffset now = new DateTime(DateTime.UtcNow.Ticks + TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time").BaseUtcOffset.Ticks, DateTimeKind.Local);
            var binary = "    " + JsonSerializer.ToJsonString(now);
            JsonSerializer.Deserialize<DateTimeOffset>(binary).Is(now);

            foreach (var item in new[] { TimeSpan.MaxValue, TimeSpan.MinValue, TimeSpan.MaxValue.Add(TimeSpan.FromTicks(-1)), TimeSpan.MinValue.Add(TimeSpan.FromTicks(1)), TimeSpan.Zero })
            {
                var ts = JsonSerializer.Deserialize<TimeSpan>("    " + JsonSerializer.ToJsonString(item));
                ts.Is(item);
            }

            foreach (var item in new[] { DateTime.MaxValue, DateTime.MaxValue.AddTicks(-1), DateTime.MinValue.AddTicks(1) })
            {
                var ts = JsonSerializer.Deserialize<DateTime>("    " + JsonSerializer.ToJsonString(item));
                ts.Is(item);
            }

            foreach (var item in new[] { DateTimeOffset.MinValue.ToUniversalTime(), DateTimeOffset.MaxValue.ToUniversalTime() })
            {
                var ts = JsonSerializer.Deserialize<DateTimeOffset>("    " + JsonSerializer.ToJsonString(item));
                ts.Is(item);
            }

            foreach (var item in new[] { new DateTimeOffset(DateTime.MinValue.Ticks, new TimeSpan(-1, 0, 0)) })
            {
                var ts = JsonSerializer.Deserialize<DateTimeOffset>("    " + JsonSerializer.ToJsonString(item));
                ts.Is(item);
            }
        }

        [Fact]
        public void Issue20()
        {
            var input = "{\"Flag\":false,\"StatusDate\":\"1970-01-01T09:15:10+00:00\"}";
            var deserialized = (Test)Utf8Json.JsonSerializer.NonGeneric.Deserialize(typeof(Test), input, StandardResolver.ExcludeNull);
            deserialized.Flag.IsFalse();
            deserialized.StatusDate.Is(new DateTimeOffset(1970, 1, 1, 9, 15, 10, TimeSpan.Zero));
        }

        public class Test
        {
            public bool Flag { get; set; }
            public DateTimeOffset? StatusDate { get; set; }
        }
    }

}
