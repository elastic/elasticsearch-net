namespace Nest
{
	// ReSharper disable InconsistentNaming
	//these const reflect their output on purpose
	public static class DateFormat
	{
		///<summary>A formatter for the number of milliseconds since the epoch. Note, that this timestamp is subject to the limits of a Java Long.MIN_VALUE and Long.MAX_VALUE.</summary>
		public const string epoch_millis = "epoch_millis";
		///<summary>A formatter for the number of seconds since the epoch. Note, that this timestamp is subject to the limits of a Java Long.MIN_VALUE and Long. MAX_VALUE divided by 1000 (the number of milliseconds in a second).</summary>
		public const string epoch_second = "epoch_second";
		///<summary>A basic formatter for a full date as four digit year, two digit month of year, and two digit day of month: yyyyMMdd.</summary>
		public const string date_optional_time = "date_optional_time";
		///<summary>A basic formatter that combines a basic date and time, separated by a T: yyyyMMdd'T'HHmmss.SSSZ.</summary>
		public const string basic_date = "basic_date";
		///<summary>A basic formatter that combines a basic date and time, separated by a T: yyyyMMdd'T'HHmmss.SSSZ.</summary>
		public const string basic_date_time = "basic_date_time";
		///<summary>A basic formatter that combines a basic date and time without millis, separated by a T: yyyyMMdd'T'HHmmssZ.</summary>
		public const string basic_date_time_no_millis = "basic_date_time_no_millis";
		///<summary>A formatter for a full ordinal date, using a four digit year and three digit dayOfYear: yyyyDDD.</summary>
		public const string basic_ordinal_date = "basic_ordinal_date";
		///<summary>A formatter for a full ordinal date and time, using a four digit year and three digit dayOfYear: yyyyDDD'T'HHmmss.SSSZ.</summary>
		public const string basic_ordinal_date_time = "basic_ordinal_date_time";
		///<summary>A formatter for a full ordinal date and time without millis, using a four digit year and three digit dayOfYear: yyyyDDD'T'HHmmssZ.</summary>
		public const string basic_ordinal_date_time_no_millis = "basic_ordinal_date_time_no_millis";
		///<summary>A basic formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, three digit millis, and time zone offset: HHmmss.SSSZ.</summary>
		public const string basic_time = "basic_time";
		///<summary>A basic formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and time zone offset: HHmmssZ.</summary>
		public const string basic_time_no_millis = "basic_time_no_millis";
		///<summary>A basic formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, three digit millis, and time zone off set prefixed by T: 'T'HHmmss.SSSZ.</summary>
		public const string basic_t_time = "basic_t_time";
		///<summary>A basic formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and time zone offset prefixed by T: 'T'HHmmssZ.</summary>
		public const string basic_t_time_no_millis = "basic_t_time_no_millis";
		///<summary>A basic formatter for a full date as four digit weekyear, two digit week of weekyear, and one digit day of week: xxxx'W'wwe.</summary>
		public const string basic_week_date = "basic_week_date";
		///<summary>A basic formatter for a full date as four digit weekyear, two digit week of weekyear, and one digit day of week: xxxx'W'wwe.</summary>
		public const string strict_basic_week_date = "strict_basic_week_date";
		///<summary>A basic formatter that combines a basic weekyear date and time, separated by a T: xxxx'W'wwe'T'HHmmss.SSSZ.</summary>
		public const string basic_week_date_time = "basic_week_date_time";
		///<summary>A basic formatter that combines a basic weekyear date and time, separated by a T: xxxx'W'wwe'T'HHmmss.SSSZ.</summary>
		public const string strict_basic_week_date_time = "strict_basic_week_date_time";
		///<summary>A basic formatter that combines a basic weekyear date and time without millis, separated by a T: xxxx'W'wwe'T'HHmmssZ.</summary>
		public const string basic_week_date_time_no_millis = "basic_week_date_time_no_millis";
		///<summary>A basic formatter that combines a basic weekyear date and time without millis, separated by a T: xxxx'W'wwe'T'HHmmssZ.</summary>
		public const string strict_basic_week_date_time_no_millis = "strict_basic_week_date_time_no_millis";
		///<summary>A formatter for a full date as four digit year, two digit month of year, and two digit day of month: yyyy-MM-dd.</summary>
		public const string date = "date";
		///<summary>A formatter for a full date as four digit year, two digit month of year, and two digit day of month: yyyy-MM-dd.</summary>
		public const string strict_date = "strict_date";
		///<summary>A formatter that combines a full date and two digit hour of day: yyyy-MM-dd'T'HH.</summary>
		public const string date_hour = "date_hour";
		///<summary>A formatter that combines a full date and two digit hour of day: yyyy-MM-dd'T'HH.</summary>
		public const string strict_date_hour = "strict_date_hour";
		///<summary>A formatter that combines a full date, two digit hour of day, and two digit minute of hour: yyyy-MM-dd'T'HH:mm.</summary>
		public const string date_hour_minute = "date_hour_minute";
		///<summary>A formatter that combines a full date, two digit hour of day, and two digit minute of hour: yyyy-MM-dd'T'HH:mm.</summary>
		public const string strict_date_hour_minute = "strict_date_hour_minute";
		///<summary>A formatter that combines a full date, two digit hour of day, two digit minute of hour, and two digit second of minute: yyyy-MM-dd'T'HH:mm:ss.</summary>
		public const string date_hour_minute_second = "date_hour_minute_second";
		///<summary>A formatter that combines a full date, two digit hour of day, two digit minute of hour, and two digit second of minute: yyyy-MM-dd'T'HH:mm:ss.</summary>
		public const string strict_date_hour_minute_second = "strict_date_hour_minute_second";
		///<summary>A formatter that combines a full date, two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: yyyy-MM-dd'T'HH:mm:ss.SSS.</summary>
		public const string date_hour_minute_second_fraction = "date_hour_minute_second_fraction";
		///<summary>A formatter that combines a full date, two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: yyyy-MM-dd'T'HH:mm:ss.SSS.</summary>
		public const string strict_date_hour_minute_second_fraction = "strict_date_hour_minute_second_fraction";
		///<summary>A formatter that combines a full date, two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: yyyy-MM-dd'T'HH:mm:ss.SSS.</summary>
		public const string date_hour_minute_second_millis = "date_hour_minute_second_millis";
		///<summary>A formatter that combines a full date, two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: yyyy-MM-dd'T'HH:mm:ss.SSS.</summary>
		public const string strict_date_hour_minute_second_millis = "strict_date_hour_minute_second_millis";
		///<summary>A formatter that combines a full date and time, separated by a T: yyyy-MM-dd'T'HH:mm:ss.SSSZZ.</summary>
		public const string date_time = "date_time";
		///<summary>A formatter that combines a full date and time, separated by a T: yyyy-MM-dd'T'HH:mm:ss.SSSZZ.</summary>
		public const string strict_date_time = "strict_date_time";
		///<summary>A formatter that combines a full date and time without millis, separated by a T: yyyy-MM-dd'T'HH:mm:ssZZ.</summary>
		public const string date_time_no_millis = "date_time_no_millis";
		///<summary>A formatter that combines a full date and time without millis, separated by a T: yyyy-MM-dd'T'HH:mm:ssZZ.</summary>
		public const string strict_date_time_no_millis = "strict_date_time_no_millis";
		///<summary>A formatter for a two digit hour of day: HH</summary>
		public const string hour = "hour";
		///<summary>A formatter for a two digit hour of day: HH</summary>
		public const string strict_hour = "strict_hour";
		///<summary>A formatter for a two digit hour of day and two digit minute of hour: HH:mm.</summary>
		public const string hour_minute = "hour_minute";
		///<summary>A formatter for a two digit hour of day and two digit minute of hour: HH:mm.</summary>
		public const string strict_hour_minute = "strict_hour_minute";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, and two digit second of minute: HH:mm:ss.</summary>
		public const string hour_minute_second = "hour_minute_second";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, and two digit second of minute: HH:mm:ss.</summary>
		public const string strict_hour_minute_second = "strict_hour_minute_second";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: HH:mm:ss.SSS.</summary>
		public const string hour_minute_second_fraction = "hour_minute_second_fraction";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: HH:mm:ss.SSS.</summary>
		public const string strict_hour_minute_second_fraction = "strict_hour_minute_second_fraction";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: HH:mm:ss.SSS.</summary>
		public const string hour_minute_second_millis = "hour_minute_second_millis";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and three digit fraction of second: HH:mm:ss.SSS.</summary>
		public const string strict_hour_minute_second_millis = "strict_hour_minute_second_millis";
		///<summary>A formatter for a full ordinal date, using a four digit year and three digit dayOfYear: yyyy-DDD.</summary>
		public const string ordinal_date = "ordinal_date";
		///<summary>A formatter for a full ordinal date, using a four digit year and three digit dayOfYear: yyyy-DDD.</summary>
		public const string strict_ordinal_date = "strict_ordinal_date";
		///<summary>A formatter for a full ordinal date and time, using a four digit year and three digit dayOfYear: yyyy-DDD'T'HH:mm:ss.SSSZZ.</summary>
		public const string ordinal_date_time = "ordinal_date_time";
		///<summary>A formatter for a full ordinal date and time, using a four digit year and three digit dayOfYear: yyyy-DDD'T'HH:mm:ss.SSSZZ.</summary>
		public const string strict_ordinal_date_time = "strict_ordinal_date_time";
		///<summary>A formatter for a full ordinal date and time without millis, using a four digit year and three digit dayOfYear: yyyy-DDD'T'HH:mm:ssZZ.</summary>
		public const string ordinal_date_time_no_millis = "ordinal_date_time_no_millis";
		///<summary>A formatter for a full ordinal date and time without millis, using a four digit year and three digit dayOfYear: yyyy-DDD'T'HH:mm:ssZZ.</summary>
		public const string strict_ordinal_date_time_no_millis = "strict_ordinal_date_time_no_millis";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, three digit fraction of second, and time zone offset: HH:mm:ss.SSSZZ.</summary>
		public const string time = "time";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, three digit fraction of second, and time zone offset: HH:mm:ss.SSSZZ.</summary>
		public const string strict_time = "strict_time";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and time zone offset: HH:mm:ssZZ.</summary>
		public const string time_no_millis = "time_no_millis";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and time zone offset: HH:mm:ssZZ.</summary>
		public const string strict_time_no_millis = "strict_time_no_millis";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, three digit fraction of second, and time zone offset prefixed by T: 'T'HH:mm:ss.SSSZZ.</summary>
		public const string t_time = "t_time";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, three digit fraction of second, and time zone offset prefixed by T: 'T'HH:mm:ss.SSSZZ.</summary>
		public const string strict_t_time = "strict_t_time";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and time zone offset prefixed by T: 'T'HH:mm:ssZZ.</summary>
		public const string t_time_no_millis = "t_time_no_millis";
		///<summary>A formatter for a two digit hour of day, two digit minute of hour, two digit second of minute, and time zone offset prefixed by T: 'T'HH:mm:ssZZ.</summary>
		public const string strict_t_time_no_millis = "strict_t_time_no_millis";
		///<summary>A formatter for a full date as four digit weekyear, two digit week of weekyear, and one digit day of week: xxxx-'W'ww-e.</summary>
		public const string week_date = "week_date";
		///<summary>A formatter for a full date as four digit weekyear, two digit week of weekyear, and one digit day of week: xxxx-'W'ww-e.</summary>
		public const string strict_week_date = "strict_week_date";
		///<summary>A formatter that combines a full weekyear date and time, separated by a T: xxxx-'W'ww-e'T'HH:mm:ss.SSSZZ.</summary>
		public const string week_date_time = "week_date_time";
		///<summary>A formatter that combines a full weekyear date and time, separated by a T: xxxx-'W'ww-e'T'HH:mm:ss.SSSZZ.</summary>
		public const string strict_week_date_time = "strict_week_date_time";
		///<summary>A formatter that combines a full weekyear date and time without millis, separated by a T: xxxx-'W'ww-e'T'HH:mm:ssZZ.</summary>
		public const string week_date_time_no_millis = "week_date_time_no_millis";
		///<summary>A formatter that combines a full weekyear date and time without millis, separated by a T: xxxx-'W'ww-e'T'HH:mm:ssZZ.</summary>
        public const string strict_week_date_time_no_millis = "strict_week_date_time_no_millis";
		///<summary>A formatter for a four digit weekyear: xxxx.</summary>
		public const string weekyear = "weekyear";
		///<summary>A formatter for a four digit weekyear: xxxx.</summary>
        public const string strict_weekyear = "strict_weekyear";
		///<summary>A formatter for a four digit weekyear and two digit week of weekyear: xxxx-'W'ww.</summary>
		public const string weekyear_week = "weekyear_week";
		///<summary>A formatter for a four digit weekyear and two digit week of weekyear: xxxx-'W'ww.</summary>
        public const string strict_weekyear_week = "strict_weekyear_week";
		///<summary>A formatter for a four digit weekyear, two digit week of weekyear, and one digit day of week: xxxx-'W'ww-e.</summary>
		public const string weekyear_week_day = "weekyear_week_day";
		///<summary>A formatter for a four digit weekyear, two digit week of weekyear, and one digit day of week: xxxx-'W'ww-e.</summary>
        public const string strict_weekyear_week_day = "strict_weekyear_week_day";
		///<summary>A formatter for a four digit year: yyyy.</summary>
		public const string year = "year";
		///<summary>A formatter for a four digit year: yyyy.</summary>
        public const string strict_year = "strict_year";
		///<summary>A formatter for a four digit year and two digit month of year: yyyy-MM.</summary>
		public const string year_month = "year_month";
		///<summary>A formatter for a four digit year and two digit month of year: yyyy-MM.</summary>
        public const string strict_year_month = "strict_year_month";
		///<summary>A formatter for a four digit year, two digit month of year, and two digit day of month: yyyy-MM-dd.</summary>
		public const string year_month_day = "year_month_day";
		///<summary>A formatter for a four digit year, two digit month of year, and two digit day of month: yyyy-MM-dd.</summary>
        public const string strict_year_month_day = "strict_year_month_day";
	}
}
