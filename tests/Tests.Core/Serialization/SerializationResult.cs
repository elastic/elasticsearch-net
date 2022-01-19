using System;

namespace Tests.Core.Serialization
{
	public class SerializationResult
	{
		public string DiffFromExpected { get; set; }
		public string Serialized { get; set; }
		public bool Success { get; set; }

		private string DiffFromExpectedExcerpt =>
			string.IsNullOrEmpty(DiffFromExpected)
				? string.Empty
				: DiffFromExpected?.Substring(0, DiffFromExpected.Length > 4896 ? 4896 : DiffFromExpected.Length);

		public override string ToString()
		{
			var message = $"{GetType().Name} success: {Success}";
			if (Success)
				return message;

			message += Environment.NewLine;
			message += DiffFromExpectedExcerpt;
			return message;
		}
	}
}
