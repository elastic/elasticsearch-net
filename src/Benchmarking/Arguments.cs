using System;
using System.CodeDom.Compiler;
using NDesk.Options;
using static System.Boolean;

namespace Benchmarking
{
	public class Arguments
	{
		private OptionSet _options;
		private bool _showHelp;

		public bool Interactive { get; private set; }

		public int Times { get; private set; }

		public Arguments()
		{
			Interactive = true;
			Times = 1;
		}

		private OptionSet OptionSet
		{
			get
			{
				return _options ?? (_options = new OptionSet
				{
					{
						"t|times=", "number of {times} to run the benchmark",
						t =>
						{
							int times;

							if (string.IsNullOrEmpty(t))
								throw new OptionException("must be specified", nameof(times));

							if (!int.TryParse(t, out times))
								throw new OptionException("must be an integer", nameof(times));

							if (times < 1)
								throw new OptionException("must be greater than 0", nameof(times));

							Times = times;
						}
					},
					{
						"i|interactive=", "runs the benchmark application in interactive mode",
						i =>
						{
							bool interactive;

							if (string.IsNullOrEmpty(i))
								throw new OptionException("must be specified", nameof(interactive));

							if (!bool.TryParse(i, out interactive))
								throw new OptionException("must be true or false", nameof(interactive));

							Interactive = interactive;
						}
					},
					{
						"?|h|help", "show this message and exit",
						v => _showHelp = v != null
					}
				});
			}
		}

		public bool Parse(string[] args)
		{
			try
			{
				OptionSet.Parse(args);

				if (_showHelp)
				{
					OptionSet.WriteOptionDescriptions(Console.Out);
					return false;
				}

				return true;
			}
			catch (OptionException e)
			{
				Console.Error.WriteLine($"Problem parsing argument for {e.OptionName}. Message: {e.Message}");
				return false;
			}
		}
	}
}