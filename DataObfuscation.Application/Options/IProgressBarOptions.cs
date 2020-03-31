using ShellProgressBar;

namespace DataObfuscation.Application.Options
{
	public interface IProgressBarOptions
	{
		ProgressBarOptions GetMainOptions();
		ProgressBarOptions GetChildOptions();
	}
}
