using System.Buffers;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RequestConverter.Console;

using System;
using System.Buffers.Text;

// TODO: A shortcut property basically defines a union. We can re-use the existing union (de-)serialization strategies.

internal class Program
{
	private static readonly FrozenSet<string> Blacklist = new HashSet<string>
	{
		// Missing 'ObjectProperty.synthetic_source_keep' property.
		"8b8b6aac2111b2d8b93758ac737e6543",

		// Missing support for non-exhaustive internal tagged variants.
		"5d428ea66252fd252b6a8d6f47605c86",
		"464dffb6a6e24a860223d1c32b232f95",

		// Missing support for non-exhaustive containers.
		//"5e21dbac92f34d236a8f0cc0d3a39cdd",
		//"8f6f7ea5abf56152b4a5639ddf40848f",
		//"fe54f3e53dbe7dee40ec3108a461d19a",
		//"7885ca9d7c61050095288eef6bc6cca9",
		//"862907653d1c18d2e80eff7f421200e2",
		//"aac5996a8398cc8f7701a063df0b2346",
		//"10de9fd4a38755020a07c4ec964d44c9",
		//"f3ab820e1f2f54ea718017aeae865742",
		//"e60b7f75ca806f2c74927c3d9409a986",
		//"5ad365ed9e1a3c26093a0f09666c133a",
		//"b80e1f5b26bae4f3c2f8a604b7caaf17",
		//"0d94d76b7f00d0459d1f8c962c144dcd",
		//"50dc35d3d8705bd62aed20a15209476c",

		// Missing shortcut property for 'DataStreamLifecycleDownsampling' to 'rounds'.
		"fe208d94ec93eabf3bd06139fa70701e",
		"e2a753029b450942a3228e3003a55a7d",

		// 'PutAutoFollowPatternRequest.leader_index_exclusion_patterns' should be "single or many" (based on this example).
		"46a0eaaf5c881f1ba716d1812b36c724",

		// 'DataframeAnalysisFeatureProcessorTargetMeanEncoding.default_value' should be 'FieldValue'.
		// 'DataframeAnalysisFeatureProcessorTargetMeanEncoding.target_map' should be 'Dictionary<Field, FieldValue>'
		"dac8ec8547bc446637fd97d9fa872f4f",

		// We need to handle custom DateTime deserialization in query parameters
		"12d5ff4b8d3d832b32a7e7e2a520d0bb",

		// 'UpdateJobRequest.detectors' should be "single or many".
		"421e68e2b9789f0e8c08760d9e685d1c",

		// 'GeoDistanceSort' additional property is 'GeoLocation | GeoLocation[]' (single or many), but 'GeoLocation'
		// itself can be an array 'double[]' (besides other variants).
		// => We can not disambiguate between these cases.
		// => We could remove the SingleOrManyConverter and generate case-by-case code for all places in our custom
		//    converters. This would allow us to do a 1-level lookahead for the next token to disambiguate.
		// => This might be a good idea anyway as things get even more complicated with "nested" single or many types
		//    like e.g. "single or many" in a dictionary
		"fbb38243221c8fb311660616e3add9ce",
		"aee4734ee63dbbbd12a21ee886f7a829",

		// No 'string' factory is registered for 'GeoLocation/GeoBoundsConverter'. Check why this is the case.
		"ed5c3b45e8de912faba44507d827eb93",
		"9beb260834f8cfb240f6308950dbb9c2",
		"2051ffe025550ab6645bfd525eaed3c4",
		"dead0682932ea6ec33c1197017bcb209",
		"eae8931d01b3b878dd0c45214121e662",
		"45c6e54a9c9e08623af96752b4bde346",
		"fbdad6620eb645f5f1f02e3673604d01",
		"ab29bfbd35ee482cf54052b03d62cd31",
		"bc4d308069af23929a49d856f6bc3008",
		"65c671fbecdb5b0d75c13d63f87e36f0",
		"ed01d27b8f80bb4ea54bf4e32b8d6258",
		"3eca58ef7592b3a857ea3a9898de5997",
		"a1879930c1dac36a57d7f094a680420b",
		"4ca5bc2c2b2f64d15b9c16370ae97a39",
		"a412fe22a74900c72434391ed75139dc",
		"a9d44463dcea3cb0ea4c8f8460cea524",
		"4659f639d71a54df571260ee5798dbb3",
		"641009f2147e1ca56215c701f45c970b",

		// No "array type" is defined for 'GeoLocation/GeoBoundsConverter'
		"0f2e5e006b663a88ee99b130ab1b4844",
		"1f507659757e2844cefced25848540a0",
		"c2d7c36daac8608d2515c549b2c82436",
		"ce247fc08371e1b30cb52195e521c076",

		// 'FunctionScoreQuery.functions' shortcut property should be "single or many" (FunctionScoreContainer | FunctionScoreContainer[]).
		// We have to check, if deserialization works after this change as we are now dealing with either an array, or an object. This
		// most certainly requires a change in the converter generation!
		"9334ccd09548b585cd637d7c66c5ae65",
		"eff2fc92d46eb3c8f4d424eed18f54a2",
		"6c00dae1a456ae5e854e98e895dca2ab",
		"996f320a0f537c24b9cd0d71b5f7c1f8",
		"fad524db23eb5718ff310956e590b00d",
		"1153bd92ca18356db927054958cd95c6",
		"4f3366fc26e7ea4de446dfa5cdec9683",
		"807c0c9763f8c1114b3c8278c2a0cb56",
		"35260b615d0b5628c95d7cc814c39bd3",
		"234cec3ead32d7ed71afbe1edfea23df",
		"6326f5c6fd2a6e6b1aff9a643b94f455",
		"07ba3eaa931f2cf110052e3544db51f8",

		// We don't capture the alternative array representation for 'Highlight.fields' in the spec.
		// See:
		// https://www.elastic.co/guide/en/elasticsearch/reference/current/highlighting.html#explicit-field-order
		"e6faae2e272ee57727f38e55a3de5bb2",

		// We don't capture the object representation for 'Script.source' in the spec.
		"f8833488041f3d318435b60917fa877c",
		"634ecacf14b83c5f0bb8b6273cf6418e",
		"41fd33a293a575bd71a1fac7bcc8b47c",

		// We don't capture the object representation for 'StoredScript.source' in the spec.
		"52bc577a0d0cd42b46f33e0ef5124df8",
		"bd2a387e8c21bf01a1039e81d7602921",
		"98b403c356a9b14544e9b9f646845e9f",

		// We don't capture the object representation for 'RenderSearchTemplate.source' in the spec.
		"30bd3c0785f3df4795684754adeb5ecb",
		"9e962baf1fb407c21d6c47dcd37cec29",
		"1eb9c6ecb827ca69f7b17f7d2a26eae9",
		"ff05842419968a2141bde0371ac2f6f4",
		"e2b4867a9f72bda87ebaa3608d3fba4c",
		"0c7c40cd17985c3dd32aeaadbafc4fce",
		"13917f7cfb6a382c293275ff71134ec4",

		// 'QueryVector' must be handcrafted as it accepts a HEX-string representation.
		"856c10ad554c26b70f1121454caff40a",

		// The 'dictionary' property is missing for 'Script' (might be a bug in the documentation example).
		"32b8a5152b47930f2e16c40c8615c7bb",
		"6b6fd0a5942dfb9762ad2790cf421a80",
		"16634cfa7916cf4e8048a1d70e6240f2",

		// ESQL named query parameters are currently not captured in the spec.
		"7fde3ff91c4a2e7080444af37d5cd287",

		// No simplified "single or many" as a dictionary value.
		"c4272ad0309ffbcbe9ce96bf9fb4352a",

		// Shortcut property converter generation does not work correctly. Shortcut property of type 'CompletionContext'
		// is of type 'Context' (string | GeoLocation) => ultimately allows 'object' BUT not only object.
		// The converter incorrectly checks for TokenType == Object.
		"25ae1a698f867ba5139605cc952436c0",
		"6521c3578dc4ad4a6db697700986e78e",

		// SQL positional query parameters are currently not captured in the spec.
		"d9e0cba8e150681d861f5fd1545514e2",

		// Missing variant 'text_similarity' for 'InferenceConfigUpdateContainer'.
		"095e3f21941a9cc75f398389a075152d",

		// 'PhraseSuggestCollateQuery.source' can be 'string' or 'object'.
		"89a6b24618cafd60de1702a5b9f28a8d",

		// Param: 'NodeStatsRequest.groups' has incorrect type 'bool', while it should be a
		// "Comma-separated list of search groups to include in the search statistics."
		// and apparently accepts the "_all" syntax.
		"bd68666ca2e0be12f7624016317a62bc",

		// We incorrectly simplify the 'Tokenizer', 'CharFilter' and 'TokenFilter' unions.
		"f7ec9062b3a7578fed55f119d7c22b74",
		"c6d39d22188dc7bbfdad811a94cbcc2b",
		"a512e4dd8880ce0395937db1bab1d205",
		"09a44b619a99f6bf3f01bd5e258fd22d",
		"c95d5317525c2ff625e6971c277247af",
		"76448aaaaa2c352bb6e09d2f83a3fbb3",
		"a99bc141066ef673e35f306157750ec9",
		"39963032d423e2f20f53c4621b6ca3c6",
		"dc4dcfeae8a5f248639335c2c9809549",
		"1a6dbe5df488c4a16e2f1101ba8a25d9",
		"88a08d0b15ef41324f5c23db533d47d1",
		"a1e5f3956f9a697e79478fc9a6e30e1f",
		"d12df43ffcdcd937bae9b26fb475e239",
		"7b9dfe5857bde1bd8483ea3241656714",
		"3343a4cf559060c422d86c786a95e535",
		"00d65f7b9daa1c6b18eedd8ace206bae",
		"b8c03bbd917d0cf5474a3e46ebdd7aad",
		"76b279835936ee4b546a171c671c3cd7",
		"c8bbf362f06a0d8dab33ec0d99743343",
		"2fd0b3c132b46aa34cc9d92dd2d4bc85",
		"09944369863fd8666d5301d717317276",
		"a21319c9eff1ac47d7fe7490f1ef2efa",
		"7dc82f7d36686fd57a47e34cbda39a4e",
		"3fecd5c6d0c172566da4a54320e1cff3",
		"6dbfe5565a95508e65d304131847f9fc",
		"446e8fc8ccfb13bb5ec64e32a5676d18",
		"df82a9cb21a7557f3ddba2509f76f608",
		"2c27a8eb6528126f37a843d434cd88b6",
		"ef10e8d07d9fae945e035d5dee1e9754",
		"62f1ec1bb5cc5a9c2efd536a7474f549",
		"f34c02351662481dd61a5c2a3e206c60",
		"83cd4eb89818b4c32f654d370eafa920",
		"d94f666616dea141dcb7aaf08a35bc10",
		"9a036a792be1d39af9fd0d1adb5f3402",
		"26f237f9bf14e8b972cc33ff6aebefa2",
		"5302f4f2bcc0f400ff71c791e6f68d7b",
		"059e04aaf093379401f665c33ac796dc",
		"a037beb3d02296e1d36dd43ef5c935dd",
		"8cbf9b46ce3ccc966c4902d2e0c56317",
		"29783e5de3a5f3c985cbf11094cf49a0",
		"68a891f609ca3a379d2d64e4914f3067",
		"1659420311d907d9fc024b96f4150216",
		"5a3855f1b3e37d89ab7cbcc4f7ae1dd3",
		"aa3284717241ed79d3d1d3bdbbdce598",
		"f65abb38dd0cfedeb06e0cef206fbdab",
		"2ec8d757188349a4630e120ba2c98c3b",
		"0d54ddad2bf6f76aa5c35f53ba77748a",
		"a159143bb578403bb9c7ff37d635d7ad",
		"15d948d593d2624ac5e2b155052048f0",
		"bab4c3b22c1768fcc7153345e4096dfb",
		"e09d30195108bd6a1f6857394a6123ea",
		"c065a200c00e2005d88ec2f0c10c908a",
		"ac366b9dda7040e743dee85335354094",
		"56fa6c9e08258157d445e2f92274962b",
		"12ec704d62ffedcb03787e6aba69d382",
		"a4e510aa9145ccedae151c4a6634f0a4",
		"e9738fe09a99080506a07945795e8eda",
		"c318fde926842722825a51e5c9c326a9",
		"a3a14f7f0e80725f695a901a7e1d579d",
		"ee2d97090d617ed8aa2a87ea33556dd7",
		"50d5c5b7e8ed9a95b8d9a25a32a77425",
		"9f7671119236423e0e40801ef6485af1",
		"c42bc6e74afc3d43cd032ec2bfd77385",
		"ffcf80e1094aa2d774f56f6b0bc54827",
		"affc7ff234dc3acccb2bf7dc51f54813",
		"02853293a5b7cd9cc7a886eb413bbeb6",
		"6a3f06962cceb3dfd3cd4fb5c679fa75",
		"6edfc35a66afd9b884431fccf48fdbf5",
		"ef33b3b373f7040b874146599db5d557",
		"dc8c94c9bef1f879282caea5c406f36e",
		"22dde5fe7ac5d85d52115641a68b3c55",
		"15a34bfe0ef8ef6333c8c7b55c011e5d",
		"89f8eac24f3ec6a7668d580aaf0eeefa",

		// We do not allow to set the 'keyed' property for aggregations.
		"ecc57597f6b791d1151ad79d9f4ce67b",
		"b7ad394975863a8f5ee29627c3ab738b",

		// Invalid example
		"d0c03847106d23ad632ceb624d647c37",
		"16a9ebe102b53495de9d2231f5ae7158",
		"48b21c5aaf16b87f1a9b1a18a5d27cbd",
		"a0bcad37014cb534a720722c3cb3fefd",
		"e9ae959608d128202921b174f4faa7a8",
		"7c862a20772467e0f5beebbd1b80c4cb",
		"2d633b7f346b828d01f923ce9dbf6ad5", // Invalid JSON
		"59726e3c90e1218487a781508788c243", // Invalid 'DateTime' value
		"316cd43feb3b86396483903af1a048b1", // Invalid 'DateTime' value
		"f5815d573cee0447910c9668003887b8", // Invalid 'CalendarInterval' value (intentional)
		"f43d551aaaad73d979adf1b86533e6a3", // Invalid 'Duration' value (intentional)

		"b0fe9a7c8e519995258786be4bef36c4", // Invalid '<task_id>'
	}.ToFrozenSet();

	private class Test
	{
		public int? A { get; set; }
		public int B { get; set; }
	}

	public static void Main(string[] args)
	{
		var converter = new RequestConverter();

		//var b = new ArrayBufferWriter<byte>();
		//var writer = new Utf8JsonWriter(b, new JsonWriterOptions { SkipValidation = false });
		//writer.
		//writer.WriteStartObject();
		//writer.WriteEndObject();
		//writer.WriteRawValue("\n"u8, true);
		//writer.WriteStartObject();
		//writer.WriteEndObject();
		//writer.WriteRawValue("\n"u8, true);
		//writer.Flush();
		//var s = b.ToString();

		using var file = File.OpenRead("D:\\elastic\\elasticsearch-net\\alternatives_report.json");

		var total = 0;
		var valid = 0;

		var examples = JsonSerializer.Deserialize<ExampleModel[]>(file, JsonSerializerOptions.Default)!;
		foreach (var example in examples)
		{
			if (example.Lang != "console")
			{
				continue;
			}

			++total;

			if (Blacklist.Contains(example.Digest))
			{
				continue;
			}

			if (example.ParsedSource is null or [])
			{
				continue;
			}

			if (example.ParsedSource.Any(x => x.Api is "bulk" or "msearch" or "msearch_template"))
			{
				// We currently don't support IStreamSerializable.
				continue;
			}

			Console.WriteLine(example.Digest);

			foreach (var source in example.ParsedSource)
			{
				Console.WriteLine(source.Api);

				var body = source.Body?.ToString();
				if (source.Api is "bulk" or "msearch" or "msearch_template")
				{
					body = source.Body!.Value.EnumerateArray().Aggregate("", (current, element) => current + (JsonSerializer.Serialize(element, JsonSerializerOptions.Default) + "\n"));
				}

				try
				{
					var code = converter.Convert(
						RequestConverter.DefaultSerializer,
						source.Api,
						source.PathParameters,
						source.QueryParameters,
						body
					);

					Console.WriteLine(code);
				}
				catch (NotSupportedException)
				{
					Console.WriteLine("not supported");
				}
			}

			++valid;
		}

		Console.WriteLine($"Total: {total}");
		Console.WriteLine($"Valid: {valid}");
		Console.WriteLine($"Ratio: {(float)valid / total * 100}");
	}
}
