using ShellProgressBar;
using System;

namespace DataObfuscation.Application.Options
{
	public class ObfuscationProgressBarOptions : IProgressBarOptions
	{
		public ProgressBarOptions GetMainOptions()
		{
			return new ProgressBarOptions
			{
				ForegroundColor = ConsoleColor.Yellow,
				BackgroundColor = ConsoleColor.DarkGray,
				ProgressCharacter = '─',
				ProgressBarOnBottom = true
			};
		}

		public ProgressBarOptions GetChildOptions()
		{
			return new ProgressBarOptions
			{
				ForegroundColor = ConsoleColor.Green,
				BackgroundColor = ConsoleColor.DarkGray,
				ProgressCharacter = '─'
			};
		}
	}
}
