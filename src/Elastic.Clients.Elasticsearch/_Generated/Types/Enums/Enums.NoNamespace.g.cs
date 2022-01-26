// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using Elastic.Transport;

#nullable restore
namespace Elastic.Clients.Elasticsearch
{
	[JsonConverter(typeof(BoundaryScannerConverter))]
	public enum BoundaryScanner
	{
		[EnumMember(Value = "word")]
		Word,
		[EnumMember(Value = "sentence")]
		Sentence,
		[EnumMember(Value = "chars")]
		Chars
	}

	internal sealed class BoundaryScannerConverter : JsonConverter<BoundaryScanner>
	{
		public override BoundaryScanner Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "word":
					return BoundaryScanner.Word;
				case "sentence":
					return BoundaryScanner.Sentence;
				case "chars":
					return BoundaryScanner.Chars;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, BoundaryScanner value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case BoundaryScanner.Word:
					writer.WriteStringValue("word");
					return;
				case BoundaryScanner.Sentence:
					writer.WriteStringValue("sentence");
					return;
				case BoundaryScanner.Chars:
					writer.WriteStringValue("chars");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(DistanceUnitConverter))]
	public enum DistanceUnit
	{
		[EnumMember(Value = "yd")]
		Yards,
		[EnumMember(Value = "nmi")]
		NauticMiles,
		[EnumMember(Value = "mm")]
		Millimeters,
		[EnumMember(Value = "mi")]
		Miles,
		[EnumMember(Value = "m")]
		Meters,
		[EnumMember(Value = "km")]
		Kilometers,
		[EnumMember(Value = "in")]
		Inches,
		[EnumMember(Value = "ft")]
		Feet,
		[EnumMember(Value = "cm")]
		Centimeters
	}

	internal sealed class DistanceUnitConverter : JsonConverter<DistanceUnit>
	{
		public override DistanceUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "yd":
					return DistanceUnit.Yards;
				case "nmi":
					return DistanceUnit.NauticMiles;
				case "mm":
					return DistanceUnit.Millimeters;
				case "mi":
					return DistanceUnit.Miles;
				case "m":
					return DistanceUnit.Meters;
				case "km":
					return DistanceUnit.Kilometers;
				case "in":
					return DistanceUnit.Inches;
				case "ft":
					return DistanceUnit.Feet;
				case "cm":
					return DistanceUnit.Centimeters;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, DistanceUnit value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case DistanceUnit.Yards:
					writer.WriteStringValue("yd");
					return;
				case DistanceUnit.NauticMiles:
					writer.WriteStringValue("nmi");
					return;
				case DistanceUnit.Millimeters:
					writer.WriteStringValue("mm");
					return;
				case DistanceUnit.Miles:
					writer.WriteStringValue("mi");
					return;
				case DistanceUnit.Meters:
					writer.WriteStringValue("m");
					return;
				case DistanceUnit.Kilometers:
					writer.WriteStringValue("km");
					return;
				case DistanceUnit.Inches:
					writer.WriteStringValue("in");
					return;
				case DistanceUnit.Feet:
					writer.WriteStringValue("ft");
					return;
				case DistanceUnit.Centimeters:
					writer.WriteStringValue("cm");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(ExpandWildcardConverter))]
	public enum ExpandWildcard
	{
		[EnumMember(Value = "open")]
		Open,
		[EnumMember(Value = "none")]
		None,
		[EnumMember(Value = "hidden")]
		Hidden,
		[EnumMember(Value = "closed")]
		Closed,
		[EnumMember(Value = "all")]
		All
	}

	internal sealed class ExpandWildcardConverter : JsonConverter<ExpandWildcard>
	{
		public override ExpandWildcard Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "open":
					return ExpandWildcard.Open;
				case "none":
					return ExpandWildcard.None;
				case "hidden":
					return ExpandWildcard.Hidden;
				case "closed":
					return ExpandWildcard.Closed;
				case "all":
					return ExpandWildcard.All;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, ExpandWildcard value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case ExpandWildcard.Open:
					writer.WriteStringValue("open");
					return;
				case ExpandWildcard.None:
					writer.WriteStringValue("none");
					return;
				case ExpandWildcard.Hidden:
					writer.WriteStringValue("hidden");
					return;
				case ExpandWildcard.Closed:
					writer.WriteStringValue("closed");
					return;
				case ExpandWildcard.All:
					writer.WriteStringValue("all");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(GeoDistanceTypeConverter))]
	public enum GeoDistanceType
	{
		[EnumMember(Value = "plane")]
		Plane,
		[EnumMember(Value = "arc")]
		Arc
	}

	internal sealed class GeoDistanceTypeConverter : JsonConverter<GeoDistanceType>
	{
		public override GeoDistanceType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "plane":
					return GeoDistanceType.Plane;
				case "arc":
					return GeoDistanceType.Arc;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, GeoDistanceType value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case GeoDistanceType.Plane:
					writer.WriteStringValue("plane");
					return;
				case GeoDistanceType.Arc:
					writer.WriteStringValue("arc");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(HealthStatusConverter))]
	public enum HealthStatus
	{
		[EnumMember(Value = "yellow")]
		Yellow,
		[EnumMember(Value = "red")]
		Red,
		[EnumMember(Value = "green")]
		Green
	}

	internal sealed class HealthStatusConverter : JsonConverter<HealthStatus>
	{
		public override HealthStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "yellow":
					return HealthStatus.Yellow;
				case "red":
					return HealthStatus.Red;
				case "green":
					return HealthStatus.Green;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, HealthStatus value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case HealthStatus.Yellow:
					writer.WriteStringValue("yellow");
					return;
				case HealthStatus.Red:
					writer.WriteStringValue("red");
					return;
				case HealthStatus.Green:
					writer.WriteStringValue("green");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(HighlighterEncoderConverter))]
	public enum HighlighterEncoder
	{
		[EnumMember(Value = "html")]
		Html,
		[EnumMember(Value = "default")]
		Default
	}

	internal sealed class HighlighterEncoderConverter : JsonConverter<HighlighterEncoder>
	{
		public override HighlighterEncoder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "html":
					return HighlighterEncoder.Html;
				case "default":
					return HighlighterEncoder.Default;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, HighlighterEncoder value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case HighlighterEncoder.Html:
					writer.WriteStringValue("html");
					return;
				case HighlighterEncoder.Default:
					writer.WriteStringValue("default");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(HighlighterFragmenterConverter))]
	public enum HighlighterFragmenter
	{
		[EnumMember(Value = "span")]
		Span,
		[EnumMember(Value = "simple")]
		Simple
	}

	internal sealed class HighlighterFragmenterConverter : JsonConverter<HighlighterFragmenter>
	{
		public override HighlighterFragmenter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "span":
					return HighlighterFragmenter.Span;
				case "simple":
					return HighlighterFragmenter.Simple;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, HighlighterFragmenter value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case HighlighterFragmenter.Span:
					writer.WriteStringValue("span");
					return;
				case HighlighterFragmenter.Simple:
					writer.WriteStringValue("simple");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(HighlighterOrderConverter))]
	public enum HighlighterOrder
	{
		[EnumMember(Value = "score")]
		Score
	}

	internal sealed class HighlighterOrderConverter : JsonConverter<HighlighterOrder>
	{
		public override HighlighterOrder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "score":
					return HighlighterOrder.Score;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, HighlighterOrder value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case HighlighterOrder.Score:
					writer.WriteStringValue("score");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(HighlighterTagsSchemaConverter))]
	public enum HighlighterTagsSchema
	{
		[EnumMember(Value = "styled")]
		Styled
	}

	internal sealed class HighlighterTagsSchemaConverter : JsonConverter<HighlighterTagsSchema>
	{
		public override HighlighterTagsSchema Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "styled":
					return HighlighterTagsSchema.Styled;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, HighlighterTagsSchema value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case HighlighterTagsSchema.Styled:
					writer.WriteStringValue("styled");
					return;
			}

			writer.WriteNullValue();
		}
	}

	public partial struct HighlighterType
	{
		public const string Unified = "unified";
		public const string Plain = "plain";
		public const string FastVector = "fvh";
	}

	[JsonConverter(typeof(LevelConverter))]
	public enum Level
	{
		[EnumMember(Value = "shards")]
		Shards,
		[EnumMember(Value = "indices")]
		Indices,
		[EnumMember(Value = "cluster")]
		Cluster
	}

	internal sealed class LevelConverter : JsonConverter<Level>
	{
		public override Level Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "shards":
					return Level.Shards;
				case "indices":
					return Level.Indices;
				case "cluster":
					return Level.Cluster;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, Level value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case Level.Shards:
					writer.WriteStringValue("shards");
					return;
				case Level.Indices:
					writer.WriteStringValue("indices");
					return;
				case Level.Cluster:
					writer.WriteStringValue("cluster");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(ResultConverter))]
	public enum Result
	{
		[EnumMember(Value = "updated")]
		Updated,
		[EnumMember(Value = "not_found")]
		NotFound,
		[EnumMember(Value = "noop")]
		NoOp,
		[EnumMember(Value = "deleted")]
		Deleted,
		[EnumMember(Value = "created")]
		Created
	}

	internal sealed class ResultConverter : JsonConverter<Result>
	{
		public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "updated":
					return Result.Updated;
				case "not_found":
					return Result.NotFound;
				case "noop":
					return Result.NoOp;
				case "deleted":
					return Result.Deleted;
				case "created":
					return Result.Created;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case Result.Updated:
					writer.WriteStringValue("updated");
					return;
				case Result.NotFound:
					writer.WriteStringValue("not_found");
					return;
				case Result.NoOp:
					writer.WriteStringValue("noop");
					return;
				case Result.Deleted:
					writer.WriteStringValue("deleted");
					return;
				case Result.Created:
					writer.WriteStringValue("created");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(ScoreModeConverter))]
	public enum ScoreMode
	{
		[EnumMember(Value = "total")]
		Total,
		[EnumMember(Value = "multiply")]
		Multiply,
		[EnumMember(Value = "min")]
		Min,
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "avg")]
		Avg
	}

	internal sealed class ScoreModeConverter : JsonConverter<ScoreMode>
	{
		public override ScoreMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "total":
					return ScoreMode.Total;
				case "multiply":
					return ScoreMode.Multiply;
				case "min":
					return ScoreMode.Min;
				case "max":
					return ScoreMode.Max;
				case "avg":
					return ScoreMode.Avg;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, ScoreMode value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case ScoreMode.Total:
					writer.WriteStringValue("total");
					return;
				case ScoreMode.Multiply:
					writer.WriteStringValue("multiply");
					return;
				case ScoreMode.Min:
					writer.WriteStringValue("min");
					return;
				case ScoreMode.Max:
					writer.WriteStringValue("max");
					return;
				case ScoreMode.Avg:
					writer.WriteStringValue("avg");
					return;
			}

			writer.WriteNullValue();
		}
	}

	public partial struct ScriptLanguage
	{
		public const string Painless = "painless";
		public const string Mustache = "mustache";
		public const string Java = "java";
		public const string Expression = "expression";
	}

	[JsonConverter(typeof(SearchTypeConverter))]
	public enum SearchType
	{
		[EnumMember(Value = "query_then_fetch")]
		QueryThenFetch,
		[EnumMember(Value = "dfs_query_then_fetch")]
		DfsQueryThenFetch
	}

	internal sealed class SearchTypeConverter : JsonConverter<SearchType>
	{
		public override SearchType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "query_then_fetch":
					return SearchType.QueryThenFetch;
				case "dfs_query_then_fetch":
					return SearchType.DfsQueryThenFetch;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, SearchType value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case SearchType.QueryThenFetch:
					writer.WriteStringValue("query_then_fetch");
					return;
				case SearchType.DfsQueryThenFetch:
					writer.WriteStringValue("dfs_query_then_fetch");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(SortModeConverter))]
	public enum SortMode
	{
		[EnumMember(Value = "sum")]
		Sum,
		[EnumMember(Value = "min")]
		Min,
		[EnumMember(Value = "median")]
		Median,
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "avg")]
		Avg
	}

	internal sealed class SortModeConverter : JsonConverter<SortMode>
	{
		public override SortMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "sum":
					return SortMode.Sum;
				case "min":
					return SortMode.Min;
				case "median":
					return SortMode.Median;
				case "max":
					return SortMode.Max;
				case "avg":
					return SortMode.Avg;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, SortMode value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case SortMode.Sum:
					writer.WriteStringValue("sum");
					return;
				case SortMode.Min:
					writer.WriteStringValue("min");
					return;
				case SortMode.Median:
					writer.WriteStringValue("median");
					return;
				case SortMode.Max:
					writer.WriteStringValue("max");
					return;
				case SortMode.Avg:
					writer.WriteStringValue("avg");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(SortOrderConverter))]
	public enum SortOrder
	{
		[EnumMember(Value = "desc")]
		Desc,
		[EnumMember(Value = "asc")]
		Asc
	}

	internal sealed class SortOrderConverter : JsonConverter<SortOrder>
	{
		public override SortOrder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "desc":
					return SortOrder.Desc;
				case "asc":
					return SortOrder.Asc;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, SortOrder value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case SortOrder.Desc:
					writer.WriteStringValue("desc");
					return;
				case SortOrder.Asc:
					writer.WriteStringValue("asc");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(SuggestModeConverter))]
	public enum SuggestMode
	{
		[EnumMember(Value = "popular")]
		Popular,
		[EnumMember(Value = "missing")]
		Missing,
		[EnumMember(Value = "always")]
		Always
	}

	internal sealed class SuggestModeConverter : JsonConverter<SuggestMode>
	{
		public override SuggestMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "popular":
					return SuggestMode.Popular;
				case "missing":
					return SuggestMode.Missing;
				case "always":
					return SuggestMode.Always;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, SuggestMode value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case SuggestMode.Popular:
					writer.WriteStringValue("popular");
					return;
				case SuggestMode.Missing:
					writer.WriteStringValue("missing");
					return;
				case SuggestMode.Always:
					writer.WriteStringValue("always");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(TimeUnitConverter))]
	public enum TimeUnit
	{
		[EnumMember(Value = "s")]
		Seconds,
		[EnumMember(Value = "nanos")]
		Nanoseconds,
		[EnumMember(Value = "ms")]
		Milliseconds,
		[EnumMember(Value = "micros")]
		Microseconds,
		[EnumMember(Value = "m")]
		Minutes,
		[EnumMember(Value = "h")]
		Hours,
		[EnumMember(Value = "d")]
		Days
	}

	internal sealed class TimeUnitConverter : JsonConverter<TimeUnit>
	{
		public override TimeUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "s":
					return TimeUnit.Seconds;
				case "nanos":
					return TimeUnit.Nanoseconds;
				case "ms":
					return TimeUnit.Milliseconds;
				case "micros":
					return TimeUnit.Microseconds;
				case "m":
					return TimeUnit.Minutes;
				case "h":
					return TimeUnit.Hours;
				case "d":
					return TimeUnit.Days;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, TimeUnit value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case TimeUnit.Seconds:
					writer.WriteStringValue("s");
					return;
				case TimeUnit.Nanoseconds:
					writer.WriteStringValue("nanos");
					return;
				case TimeUnit.Milliseconds:
					writer.WriteStringValue("ms");
					return;
				case TimeUnit.Microseconds:
					writer.WriteStringValue("micros");
					return;
				case TimeUnit.Minutes:
					writer.WriteStringValue("m");
					return;
				case TimeUnit.Hours:
					writer.WriteStringValue("h");
					return;
				case TimeUnit.Days:
					writer.WriteStringValue("d");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(TotalHitsRelationConverter))]
	public enum TotalHitsRelation
	{
		[EnumMember(Value = "gte")]
		Gte,
		[EnumMember(Value = "eq")]
		Eq
	}

	internal sealed class TotalHitsRelationConverter : JsonConverter<TotalHitsRelation>
	{
		public override TotalHitsRelation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "gte":
					return TotalHitsRelation.Gte;
				case "eq":
					return TotalHitsRelation.Eq;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, TotalHitsRelation value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case TotalHitsRelation.Gte:
					writer.WriteStringValue("gte");
					return;
				case TotalHitsRelation.Eq:
					writer.WriteStringValue("eq");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(VersionTypeConverter))]
	public enum VersionType
	{
		[EnumMember(Value = "internal")]
		Internal,
		[EnumMember(Value = "force")]
		Force,
		[EnumMember(Value = "external_gte")]
		ExternalGte,
		[EnumMember(Value = "external")]
		External
	}

	internal sealed class VersionTypeConverter : JsonConverter<VersionType>
	{
		public override VersionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "internal":
					return VersionType.Internal;
				case "force":
					return VersionType.Force;
				case "external_gte":
					return VersionType.ExternalGte;
				case "external":
					return VersionType.External;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, VersionType value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case VersionType.Internal:
					writer.WriteStringValue("internal");
					return;
				case VersionType.Force:
					writer.WriteStringValue("force");
					return;
				case VersionType.ExternalGte:
					writer.WriteStringValue("external_gte");
					return;
				case VersionType.External:
					writer.WriteStringValue("external");
					return;
			}

			writer.WriteNullValue();
		}
	}

	[JsonConverter(typeof(WaitForEventsConverter))]
	public enum WaitForEvents
	{
		[EnumMember(Value = "urgent")]
		Urgent,
		[EnumMember(Value = "normal")]
		Normal,
		[EnumMember(Value = "low")]
		Low,
		[EnumMember(Value = "languid")]
		Languid,
		[EnumMember(Value = "immediate")]
		Immediate,
		[EnumMember(Value = "high")]
		High
	}

	internal sealed class WaitForEventsConverter : JsonConverter<WaitForEvents>
	{
		public override WaitForEvents Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "urgent":
					return WaitForEvents.Urgent;
				case "normal":
					return WaitForEvents.Normal;
				case "low":
					return WaitForEvents.Low;
				case "languid":
					return WaitForEvents.Languid;
				case "immediate":
					return WaitForEvents.Immediate;
				case "high":
					return WaitForEvents.High;
			}

			ThrowHelper.ThrowJsonException();
			return default;
		}

		public override void Write(Utf8JsonWriter writer, WaitForEvents value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case WaitForEvents.Urgent:
					writer.WriteStringValue("urgent");
					return;
				case WaitForEvents.Normal:
					writer.WriteStringValue("normal");
					return;
				case WaitForEvents.Low:
					writer.WriteStringValue("low");
					return;
				case WaitForEvents.Languid:
					writer.WriteStringValue("languid");
					return;
				case WaitForEvents.Immediate:
					writer.WriteStringValue("immediate");
					return;
				case WaitForEvents.High:
					writer.WriteStringValue("high");
					return;
			}

			writer.WriteNullValue();
		}
	}
}