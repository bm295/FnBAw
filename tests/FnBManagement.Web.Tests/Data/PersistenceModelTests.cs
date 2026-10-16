using FnBManagement.Web.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FnBManagement.Web.Tests.Data;

public class PersistenceModelTests
{
    [Fact]
    public void RelocatedEntities_DoNotRequireSchemaChanges()
    {
        var options = new DbContextOptionsBuilder<FnBManagementDbContext>()
            .UseNpgsql("Host=localhost;Database=model_check;Username=model_check;Password=unused").Options;
        using var context = new FnBManagementDbContext(options);
        var migrations = context.GetService<IMigrationsAssembly>();
        Assert.Contains("20260623000000_InitialCreate", migrations.Migrations.Keys);
        var snapshot = migrations.ModelSnapshot;
        Assert.NotNull(snapshot);
        var snapshotModel = context.GetService<IModelRuntimeInitializer>()
            .Initialize(snapshot.Model, designTime: true);
        var currentModel = context.GetService<IDesignTimeModel>().Model;

        Assert.False(context.GetService<IMigrationsModelDiffer>().HasDifferences(
            snapshotModel.GetRelationalModel(), currentModel.GetRelationalModel()));
    }
}
