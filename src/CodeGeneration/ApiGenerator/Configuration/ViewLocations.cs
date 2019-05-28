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