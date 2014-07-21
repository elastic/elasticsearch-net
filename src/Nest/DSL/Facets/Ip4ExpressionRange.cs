using Newtonsoft.Json;

namespace Nest
{
	public class Ip4ExpressionRange
	{
		[JsonProperty(PropertyName = "from")]
		internal string _From { get; set; }

		[JsonProperty(PropertyName = "to")]
		internal string _To { get; set; }

		[JsonProperty(PropertyName = "mask")]
		internal string _Mask { get; set; }

		public Ip4ExpressionRange From(string value)
		{
			this._From = value;
			return this;
		}
		public Ip4ExpressionRange To(string value)
		{
			this._To = value;
			return this;
		}

		public Ip4ExpressionRange Mask(string value)
		{
			this._Mask = value;
			return this;
		}
	}
}
