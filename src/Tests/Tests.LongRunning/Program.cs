using System;
using System.Diagnostics;
using System.Globalization;
using CommandLine;
using Tests.LongRunning.CommandLineArgs;

namespace Tests.LongRunning
{
	internal class Program
	{
		private static void Main(string[] args) =>
			Parser.Default.ParseArguments<IndexPostsOptions, IndexUsersOptions, UpdateAnswersWithTagsOptions>(args)
				.MapResult(
					(IndexPostsOptions opts) =>
						ProfileWrapper(opts, () => RunIndexPostsAndReturnExitCode(opts)),
					(IndexUsersOptions opts) =>
						ProfileWrapper(opts, () => RunIndexUsersAndReturnExitCode(opts)),
					(UpdateAnswersWithTagsOptions opts) =>
						ProfileWrapper(opts, () => UpdateAnswersWithQuestionTagsAndReturnExitCode(opts)),
					errs => 1);

		private static int ProfileWrapper(LongRunningAppArgumentsBase opts, Func<int> run)
		{
			WaitOnProfileConfirmation(opts);
			var exitCode = 0;
			do
			{
				exitCode = run();
			} while (RunAgain(opts));
			return exitCode;
		}

		private static void WaitOnProfileConfirmation(LongRunningAppArgumentsBase opts)
		{
			if (!opts.ProfileApplication) return;

			Log.WriteLine($"Current Process Id: {Process.GetCurrentProcess().Id}");
			Log.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private static bool RunAgain(LongRunningAppArgumentsBase opts)
		{
			if (!opts.ProfileApplication) return false;

			Log.WriteLine($"Current Process Id: {Process.GetCurrentProcess().Id}");
			Log.WriteLine("Run again [Y/N]:");
			var c = Console.ReadKey();
			return c.KeyChar.ToString().Equals("Y", StringComparison.OrdinalIgnoreCase);
		}

		private static int RunIndexUsersAndReturnExitCode(IndexUsersOptions opts)
		{
			try
			{
				var indexer = new BulkIndexer(opts);
				indexer.IndexUsers(opts.UsersPath, opts.BadgesPath);
				return 0;
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				return 1;
			}
		}

		private static int RunIndexPostsAndReturnExitCode(IndexPostsOptions opts)
		{
			try
			{
				var indexer = new BulkIndexer(opts);
				indexer.IndexPosts(opts.PostsPath);
				return 0;
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				return 1;
			}
		}

		private static int UpdateAnswersWithQuestionTagsAndReturnExitCode(UpdateAnswersWithTagsOptions opts)
		{
			try
			{
				var indexer = new BulkIndexer(opts);
				indexer.UpdateAnswersWithQuestionTags(opts.PostsPath, opts.Size);
				return 0;
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				return 1;
			}
		}
	}
}
