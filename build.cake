#load nuget:?package=Cake.Recipe&version=1.0.0

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            solutionFilePath: "./sourcelyzer.sln",
                            title: "sourcelyzer",
                            repositoryOwner: "vhatsura",
                            repositoryName: "sourcelyzer",
                            shouldRunGitVersion: true,
                            shouldRunDotNetCorePack: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

// BuildParameters.Tasks.DefaultTask.IsDependentOn(BuildParameters.Tasks.PublishMyGetPackagesTask.Task.Name);

Build.RunDotNetCore();