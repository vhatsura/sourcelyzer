#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

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