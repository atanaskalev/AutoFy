using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoFy.Data.Context;

public class AutoFyDbContextFactory : IDesignTimeDbContextFactory<AutoFyDbContext>
{
    public AutoFyDbContext CreateDbContext(string[] args)
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AutoFy",
            "autofy.db");

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        var optionsBuilder = new DbContextOptionsBuilder<AutoFyDbContext>();

        optionsBuilder.UseSqlite($"Filename={dbPath}");

        return new AutoFyDbContext(optionsBuilder.Options);
    }
}