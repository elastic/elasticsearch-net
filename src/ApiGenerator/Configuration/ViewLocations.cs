// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace ApiGenerator.Configuration 
{
	public static class ViewLocations
	{
		public static string Root { get; } = $@"{GeneratorLocations.Root}Views/";
		private static string HighLevelRoot { get; } = $@"{Root}/HighLevel/";
		public static string HighLevel(params string[] paths) => HighLevelRoot + string.Join("/", paths);
		
		private static string LowLevelRoot { get; } = $@"{Root}/LowLevel/";
		public static string LowLevel(params string[] paths) => LowLevelRoot + string.Join("/", paths);
	}
}