using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Tests.Profiling.Framework
{
	internal abstract class ProfileFactory<TAttribute> : IProfileFactory where TAttribute : ProfilingAttribute
	{
		private IEnumerable<ProfiledClass> _profiledClasses;

		protected ProfileFactory(string sdkPath,
			string outputPath,
			ProfilingCluster cluster,
			Assembly assembly,
			IColoredWriter output)
		{
			this.Name = this.GetType().Name.Replace("ProfileFactory", string.Empty).ToLowerInvariant();
			this.SdkPath = sdkPath;
			this.OutputPath = Path.Combine(outputPath, this.Name);
			this.Cluster = cluster;
			this.Assembly = assembly;
			this.Output = output;
		}

		private string Name { get; }

		protected Assembly Assembly { get; }

		protected ProfilingCluster Cluster { get; }

		protected IColoredWriter Output { get; }

		protected string OutputPath { get; }

		protected IEnumerable<ProfiledClass> ProfiledClasses
		{
			get
			{
				if (_profiledClasses == null)
				{
					_profiledClasses = this.Assembly
						.GetTypes()
						.Where(t => t.GetTypeInfo().IsPublic && t.GetTypeInfo().IsClass)
						.Where(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public)
							.Any(m => m.GetCustomAttribute<TAttribute>() != null))
						.Select(t =>
						{
							var setup = t.GetMethods(BindingFlags.Instance | BindingFlags.Public)
								.Where(m => m.GetCustomAttribute<ProfilingSetupAttribute>() != null)
								.Select(m => new ProfiledMethod(m, m.GetCustomAttribute<ProfilingSetupAttribute>()))
								.FirstOrDefault();

							var methods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public)
								.Where(m => m.GetCustomAttribute<TAttribute>() != null)
								.Select(m => new ProfiledMethod(m, m.GetCustomAttribute<TAttribute>()));

							return new ProfiledClass(t, setup, methods);
						});
				}

				return _profiledClasses;
			}
		}

		protected string SdkPath { get; }

		public void Run(ProfileConfiguration configuration)
		{
			foreach (var profiledClass in this.ProfiledClasses
				.Where(c => !configuration.ClassNames.Any() || configuration.ClassNames.Contains(c.Name))
				.Where(c => c.Methods.Any(m => !m.IsAsync)))
			{
				var instance = profiledClass.CreateInstance(this.Cluster);

				if (profiledClass.SetupMethod != null)
				{
					var setup = profiledClass.SetupMethod.Compile(instance);
					this.Output.WriteLine(ConsoleColor.Green, $"Running setup for {profiledClass.Type.Name}");
					setup();
				}

				foreach (var profiledMethod in profiledClass.Methods.Where(m => !m.IsAsync))
				{
					var resultsDirectory = Path.Combine(this.OutputPath, profiledClass.Type.Name, profiledMethod.MethodInfo.Name);
					var action = profiledMethod.Compile(instance);

					this.Output.WriteLine(
						ConsoleColor.Green,
						$"{this.Name} profiling {profiledClass.Type.Name}.{profiledMethod.MethodInfo.Name}");

					using (this.BeginProfiling(resultsDirectory))
					{
						for (var i = 0; i < profiledMethod.Attribute.Iterations; i++)
						{
							action();
						}
					}
				}
			}
		}

		public async Task RunAsync(ProfileConfiguration configuration)
		{
			foreach (var profiledClass in this.ProfiledClasses
				.Where(c => !configuration.ClassNames.Any() || configuration.ClassNames.Contains(c.Name))
				.Where(c => c.Methods.Any(m => m.IsAsync)))
			{
				var instance = profiledClass.CreateInstance(this.Cluster);

				if (profiledClass.SetupMethod != null)
				{
					var setup = profiledClass.SetupMethod.Compile(instance);
					this.Output.WriteLine(ConsoleColor.Green, $"Running setup for {profiledClass.Type.Name}");
					setup();
				}

				foreach (var profiledMethod in profiledClass.Methods.Where(m => m.IsAsync))
				{
					var resultsDirectory = Path.Combine(this.OutputPath, profiledClass.Type.Name, profiledMethod.MethodInfo.Name);
					var thunk = profiledMethod.CompileAsync(instance);

					this.Output.WriteLine(
						ConsoleColor.Green,
						$"{this.Name} profiling {profiledClass.Type.Name}.{profiledMethod.MethodInfo.Name}");

					using (this.BeginProfiling(resultsDirectory))
					{
						for (var i = 0; i < profiledMethod.Attribute.Iterations; i++)
						{
							await thunk().ConfigureAwait(false);
						}
					}
				}
			}
		}

		protected abstract IDisposable BeginProfiling(string resultsDirectory);
	}
}
