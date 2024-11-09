using System.Diagnostics;
using System.IO;

namespace VsOpenFolder.Commands;

[Command(PackageIds.OpenFolderCommand)]
internal sealed class VsOpenFolderCommand : BaseCommand<VsOpenFolderCommand>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        Project project = await VS.Solutions.GetActiveProjectAsync();
        if (project == null)
            return;

        string dir = Path.GetDirectoryName(project.FullPath);
        string buildDir = await project.GetAttributeAsync("OutputPath");

        if (string.IsNullOrEmpty(buildDir) == false)
            dir = Path.Combine(dir, buildDir);

        Process.Start(dir);
    }
}
