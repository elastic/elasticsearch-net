using System;
using System.Collections.Generic;
using System.Reflection;
using Bogus;
using Newtonsoft.Json;
using Tests.Configuration;
using Tests.Domain.Helpers;

namespace Tests.Domain
{
	public class CommitActivity
	{
		public static IList<CommitActivity> CommitActivities { get; } =
			Generator.Clone().Generate(1000);

		public Developer Committer { get; set; }
		public double ConfidenceFactor { get; set; }
		public TimeSpan? Duration { get; set; }

		public static Faker<CommitActivity> Generator { get; } =
			new Faker<CommitActivity>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Id, p => Guid.NewGuid().ToString("N").Substring(0, 8))
				.RuleFor(p => p.ProjectName, p => Project.Projects[Gimme.Random.Number(0, Project.Projects.Count - 1)].Name)
				.RuleFor(p => p.Committer, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count - 1)])
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
				}));

		public string Id { get; set; }
		public string Message { get; set; }
		public string ProjectName { get; set; }
		public long SizeInBytes { get; set; }

		[JsonConverter(typeof(StringTimeSpanConverter))]
		public TimeSpan? StringDuration
		{
			get => Duration;
			set => Duration = value;
		}
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
				if (!objectType.GetTypeInfo().IsGenericType || objectType.GetGenericTypeDefinition() != typeof(Nullable<>))
					throw new JsonSerializationException($"Cannot convert null value to {objectType}.");

				return null;
			}
			if (reader.TokenType == JsonToken.String) return TimeSpan.Parse((string)reader.Value);

			throw new JsonSerializationException($"Cannot convert token of type {reader.TokenType} to {objectType}.");
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
	}
}
