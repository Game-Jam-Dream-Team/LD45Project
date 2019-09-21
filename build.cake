#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#addin nuget:?package=Cake.FileHelpers&version=3.2.0

var target = Argument("target", string.Empty);
var buildDir = Directory("./Build");

Func<string, string, bool, string> Run = (fileName, cmd, ignoreExitCode) => {
	var settings = new ProcessSettings {
		RedirectStandardOutput = true,
		RedirectStandardError  = true,
		Arguments              = new ProcessArgumentBuilder().Append(cmd)
	};
	Information($"Run program '{fileName}' with command: '{cmd}'");
	using ( var proc = StartAndReturnProcess(fileName, settings) ) {
		proc.WaitForExit();
		var stdout = string.Join("\n", proc.GetStandardOutput());
		Information($"Stdout:\n{stdout}");
		Information($"Stderr:\n{string.Join("\n", proc.GetStandardError())}");
		var exitCode = proc.GetExitCode();
		Information($"Exit code is {exitCode}");
		if ( (exitCode != 0) && !ignoreExitCode ) {
			throw new Exception($"Run '{fileName}' failed.");
		}
		return stdout;
	}
};

Func<string, bool, string> RunUnity = (cmd, ignoreExitCode) => {
	var unityPath = $"/Applications/Unity/Hub/Editor/2019.2.6f1/Unity.app/Contents/MacOS/Unity";
	var fullCmd = cmd + $" -quit -batchmode -nographics -logFile -";
	return Run(unityPath, fullCmd, ignoreExitCode);
};

Func<string[], string, string> GetStrStartsWith = (lines, prefix) => {
	return lines
		.Select(l => l.Trim())
		.Where(l => l.StartsWith(prefix))
		.Select(l => l.Substring(prefix.Length))
		.First();
};

Func<string> GetLatestCommit = () => {
	return Run("git", "rev-parse --short HEAD", false);
};

Func<string> GetProjectVersion = () => {
	return GetStrStartsWith(
		FileReadLines("ProjectSettings/ProjectSettings.asset"),
		"bundleVersion: ");
};

Task("Clean")
	.Does(() =>
{
	CleanDirectory(buildDir);
});

Task("Build")
	.Does(() =>
{
    Run("git", "checkout ProjectSettings/ProjectSettings.asset", false);
	var latestCommit = GetLatestCommit();
	var version = GetProjectVersion() + "." + latestCommit;
	var buildTarget = "WebGL";
	RunUnity($"-executeMethod UnityCiPipeline.CustomBuildPipeline.RunBuildForVersion -projectPath . -version={version} -buildTarget={buildTarget}", false);
});

Task("Upload")
	.Does(() =>
{
	var version = GetProjectVersion();
	var target = "konh/ld45-project:html";
	Run("butler", $"push --userversion={version} --verbose Build {target}", false);
});

Task("Publish")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("Upload")
    .Does(() => {});

RunTarget(target);
