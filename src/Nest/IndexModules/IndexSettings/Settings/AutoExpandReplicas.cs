using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(AutoExpandReplicasJsonConverter))]
	public class AutoExpandReplicas
	{
		private const string AllMaxReplicas = "all";
		private Union<int?, string> _maxReplicas;
		private int? _minReplicas;

		/// <summary>
		/// Whether auto expand replicas is enabled
		/// </summary>
		public bool Enabled { get; private set; }

		/// <summary>
		/// The lower bound of replicas
		/// </summary>
		public int? MinReplicas
		{
			get => _minReplicas;
			private set
			{
				if (value == null && _maxReplicas == null) Enabled = false;
				else Enabled = true;
				_minReplicas = value;
			}
		}

		/// <summary>
		/// The upper bound of replicas. Can be an integer value or a string value of "all"
		/// </summary>
		public Union<int?, string> MaxReplicas
		{
			get => _maxReplicas;
			private set
			{
				if (value == null && _minReplicas == null) Enabled = false;
				else Enabled = true;
				_maxReplicas = value;
			}
		}

		public static AutoExpandReplicas Disabled { get; } = new AutoExpandReplicas();

		/// <summary>
		/// Creates an <see cref="AutoExpandReplicas"/> with the specified lower and upper bounds of replicas
		/// </summary>
		public static AutoExpandReplicas Create(int minReplicas, int maxReplicas)
		{
			if (minReplicas < 0)
				throw new ArgumentException("minReplicas must be greater than or equal to 0", nameof(minReplicas));

			if (maxReplicas < 0)
				throw new ArgumentException("maxReplicas must be greater than or equal to 0", nameof(minReplicas));

			if (minReplicas > maxReplicas)
				throw new ArgumentException("minReplicas must be less than or equal to maxReplicas", nameof(minReplicas));

			return new AutoExpandReplicas
			{
				Enabled = true,
				MinReplicas = minReplicas,
				MaxReplicas = maxReplicas
			};
		}

		/// <summary>
		/// Creates an <see cref="AutoExpandReplicas"/> with the specified lower bound of replicas and an
		/// "all" upper bound of replicas
		/// </summary>
		public static AutoExpandReplicas Create(int minReplicas)
		{
			if (minReplicas < 0)
				throw new ArgumentException("minReplicas must be greater than or equal to 0", nameof(minReplicas));

			return new AutoExpandReplicas
			{
				Enabled = true,
				MinReplicas = minReplicas,
				MaxReplicas = AllMaxReplicas
			};
		}

		/// <summary>
		/// Creates an <see cref="AutoExpandReplicas"/> with the specified lower and upper bounds of replicas
		/// </summary>
		/// <example>0-5</example>
		/// <example>0-all</example>
		public static AutoExpandReplicas Create(string value)
		{
			if (value.IsNullOrEmpty())
				throw new ArgumentException("cannot be null or empty", nameof(value));

			var expandReplicaParts = value.Split('-');
			if (expandReplicaParts.Length != 2)
				throw new ArgumentException("must contain a 'from' and 'to' value", nameof(value));

			if (!int.TryParse(expandReplicaParts[0], out var minReplicas))
				throw new FormatException("minReplicas must be an integer");

			var maxReplicas = 0;
			var parsedMaxReplicas = false;
			var allMaxReplicas = expandReplicaParts[1] == AllMaxReplicas;

			if (!allMaxReplicas)
				parsedMaxReplicas = int.TryParse(expandReplicaParts[1], out maxReplicas);

			if (!parsedMaxReplicas && !allMaxReplicas)
				throw new FormatException("minReplicas must be an integer or 'all'");

			return parsedMaxReplicas
				? Create(minReplicas, maxReplicas)
				: Create(minReplicas);
		}

		public static implicit operator AutoExpandReplicas(string value) =>
			value.IsNullOrEmpty() ? null : Create(value);

		public override string ToString()
		{
			if (!Enabled) return "false";
			var maxReplicas = MaxReplicas.Match(i => i.ToString(), s => s);
			return string.Join("-", MinReplicas, maxReplicas);
		}
	}

	internal class AutoExpandReplicasJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var autoExpandReplicas = (AutoExpandReplicas)value;

			if (autoExpandReplicas == null || !autoExpandReplicas.Enabled)
			{
				writer.WriteValue(false);
				return;
			}

			writer.WriteValue(autoExpandReplicas.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Boolean)
				return AutoExpandReplicas.Disabled;
			if (reader.TokenType == JsonToken.String)
				return AutoExpandReplicas.Create((string)reader.Value);

			throw new JsonSerializationException($"Cannot deserialize {typeof(AutoExpandReplicas)} from {reader.TokenType}");
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
