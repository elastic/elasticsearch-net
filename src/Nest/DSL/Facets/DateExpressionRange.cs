using System;
using Newtonsoft.Json;

namespace Nest
{
	public class DateExpressionRange 
	{
		[JsonProperty(PropertyName = "from")]
		internal string _From { get; set; }

		[JsonProperty(PropertyName = "to")]
		internal string _To { get; set; }

        [JsonProperty(PropertyName = "key")]
        internal string _Key { get; set; }

		public DateExpressionRange From(string value)
		{
			this._From = value;
			return this;
		}
		public DateExpressionRange To(string value)
		{
			this._To = value;
			return this;
		}
        public DateExpressionRange Key(string key)
        {
            this._Key = key;
            return this;
        }
    }
}