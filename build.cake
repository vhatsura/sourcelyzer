#load nuget:?package=Cake.Recipe&version=1.0.0

Environment.SetVariableNames();

var pushNuGetPackages = BuildSystem.IsRunningOnAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Branch == "develop";

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            solutionFilePath: "./sourcelyzer.sln",
                            title: "sourcelyzer",
                            repositoryOwner: "vhatsura",
                            repositoryName: "sourcelyzer",
                            shouldRunGitVersion: true,
                            shouldRunDotNetCorePack: true,
                            shouldPublishMyGet: pushNuGetPackages,
                            shouldPublishNuGet: pushNuGetPackages);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

// BuildParameters.Tasks.DefaultTask.IsDependentOn(BuildParameters.Tasks.PublishMyGetPackagesTask.Task.Name);

Build.RunDotNetCore();