using Newtonsoft.Json.Linq;

namespace Nest
{
	public class CompletionOption<T>
	{
		public CompletionOption(SuggestOption option)
		{
			Frequency = option.Frequency;
			Score = option.Score;
			Text = option.Text;
			Payload = DeserializePayload(option);
		}

		private T DeserializePayload(SuggestOption option)
		{
			var jObject = option.Payload as JObject;
			return jObject == null ? default(T) : jObject.ToObject<T>();
		}

		public int? Frequency { get; internal set; }
		public double Score { get; internal set; }
		public string Text { get; internal set; }
		public T Payload { get; internal set; }
	}
}