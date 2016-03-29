using System;
using Bogus;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tests.Framework.MockData
{
	public class CommitActivity
	{
		public string Id { get; set; }
		public string ProjectName { get; set; }
		public string Message { get; set; }
		public long SizeInBytes { get; set; }
		public double ConfidenceFactor { get; set; }
		public Developer Committer { get; set; }
		public TimeSpan? Duration { get; set; }

		[Text]
		[JsonConverter(typeof(StringTimeSpanConverter))]
		public TimeSpan? StringDuration
		{
			get { return Duration; }
			set { Duration = value; }
		}

		public static Faker<CommitActivity> Generator { get; } =
			new Faker<CommitActivity>()
				.RuleFor(p => p.Id, p => Guid.NewGuid().ToString("N").Substring(0, 8))
				.RuleFor(p => p.ProjectName, p => Project.Projects[Gimme.Random.Number(0, Project.Projects.Count -1)].Name)
				.RuleFor(p => p.Committer, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Message, p => p.Lorem.Paragraph(Gimme.Random.Number(1, 3)))
				.RuleFor(p => p.SizeInBytes, p => p.Random.Number(0, 100000))
				.RuleFor(p => p.ConfidenceFactor, p => p.Random.Double())
				.RuleFor(p => p.Duration, p => p.Random.ArrayElement(new TimeSpan?[]
				{
					TimeSpan.MinValue,
					TimeSpan.MaxValue,
					TimeSpan.FromMinutes(7.5),
					TimeSpan.Zero,
					null,
					TimeSpan.FromHours(4.23),
					TimeSpan.FromDays(5),
				}))
			;
	}

	internal class StringTimeSpanConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteNull();
			else
			{
				var timeSpan = (TimeSpan)value;
				writer.WriteValue(timeSpan.ToString());
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				if (!objectType.IsGeneric() || objectType.GetGenericTypeDefinition() != typeof(Nullable<>))
					throw new JsonSerializationException($"Cannot convert null value to {objectType}.");

				return null;
			}
			if (reader.TokenType == JsonToken.String)
			{
				return TimeSpan.Parse((string)reader.Value);
			}

			throw new JsonSerializationException($"Cannot convert token of type {reader.TokenType} to {objectType}.");
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
	}
}
