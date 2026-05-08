using Birtamod.Population;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Birtamod.EntityFrameworkCore;

public static class BirtamodPopulationDbContextModelCreatingExtensions
{
    public static void ConfigurePopulationManagement(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Religion>(b => ConfigureMasterData(b, "Religions"));
        builder.Entity<Language>(b => ConfigureMasterData(b, "Languages"));
        builder.Entity<Ethnicity>(b => ConfigureMasterData(b, "Ethnicities"));
        builder.Entity<EducationQualification>(b => ConfigureMasterData(b, "EducationQualifications"));
        builder.Entity<FamilyType>(b => ConfigureMasterData(b, "FamilyTypes"));

        builder.Entity<Ward>(b =>
        {
            b.ToTable(BirtamodConsts.DbTablePrefix + "Wards", BirtamodConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.WardName).IsRequired().HasMaxLength(128);
            b.HasIndex(x => x.WardNumber).IsUnique();
        });

        builder.Entity<Household>(b =>
        {
            b.ToTable(BirtamodConsts.DbTablePrefix + "Households", BirtamodConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.HouseNumber).IsRequired().HasMaxLength(64);
            b.Property(x => x.FamilyHeadName).IsRequired().HasMaxLength(256);
            b.Property(x => x.Address).IsRequired().HasMaxLength(512);
            b.Property(x => x.Latitude).HasPrecision(10, 7);
            b.Property(x => x.Longitude).HasPrecision(10, 7);
            b.HasIndex(x => x.HouseNumber);
            b.HasOne(x => x.Ward).WithMany().HasForeignKey(x => x.WardId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Citizen>(b =>
        {
            b.ToTable(BirtamodConsts.DbTablePrefix + "Citizens", BirtamodConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            b.Property(x => x.MiddleName).HasMaxLength(128);
            b.Property(x => x.LastName).IsRequired().HasMaxLength(128);
            b.Property(x => x.DateOfBirthBs).IsRequired().HasMaxLength(16);
            b.Property(x => x.Occupation).HasMaxLength(256);
            b.Property(x => x.CitizenshipNumber).HasMaxLength(64);
            b.Property(x => x.PhoneNumber).HasMaxLength(32);
            b.Property(x => x.Email).HasMaxLength(256);
            b.Property(x => x.Address).HasMaxLength(512);
            b.HasIndex(x => x.CitizenshipNumber);
            b.HasIndex(x => new { x.WardId, x.Gender });

            b.HasOne(x => x.Religion).WithMany().HasForeignKey(x => x.ReligionId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Language).WithMany().HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Ethnicity).WithMany().HasForeignKey(x => x.EthnicityId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.EducationQualification).WithMany().HasForeignKey(x => x.EducationQualificationId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.FamilyType).WithMany().HasForeignKey(x => x.FamilyTypeId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Ward).WithMany().HasForeignKey(x => x.WardId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Household).WithMany().HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureMasterData<TEntity>(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> b, string tableName)
        where TEntity : MasterDataEntity
    {
        b.ToTable(BirtamodConsts.DbTablePrefix + tableName, BirtamodConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        b.Property(x => x.Description).HasMaxLength(1024);
        b.HasIndex(x => x.Name);
    }
}
