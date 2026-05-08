using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Birtamod.Migrations
{
    /// <inheritdoc />
    public partial class AddedPopulationManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppEducationQualifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEducationQualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppEthnicities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEthnicities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppFamilyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFamilyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppReligions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppReligions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppWards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WardNumber = table.Column<int>(type: "integer", nullable: false),
                    WardName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppHouseholds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HouseNumber = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FamilyHeadName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    WardId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalMembers = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHouseholds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppHouseholds_AppWards_WardId",
                        column: x => x.WardId,
                        principalTable: "AppWards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCitizens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirthAd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfBirthBs = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    ReligionId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    EthnicityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationQualificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    FamilyTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisabilityStatus = table.Column<bool>(type: "boolean", nullable: false),
                    Occupation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CitizenshipNumber = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    WardId = table.Column<Guid>(type: "uuid", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsHouseOwner = table.Column<bool>(type: "boolean", nullable: false),
                    HasToilet = table.Column<bool>(type: "boolean", nullable: false),
                    Address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCitizens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCitizens_AppEducationQualifications_EducationQualificati~",
                        column: x => x.EducationQualificationId,
                        principalTable: "AppEducationQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCitizens_AppEthnicities_EthnicityId",
                        column: x => x.EthnicityId,
                        principalTable: "AppEthnicities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCitizens_AppFamilyTypes_FamilyTypeId",
                        column: x => x.FamilyTypeId,
                        principalTable: "AppFamilyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCitizens_AppHouseholds_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "AppHouseholds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCitizens_AppLanguages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "AppLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCitizens_AppReligions_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "AppReligions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCitizens_AppWards_WardId",
                        column: x => x.WardId,
                        principalTable: "AppWards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_CitizenshipNumber",
                table: "AppCitizens",
                column: "CitizenshipNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_EducationQualificationId",
                table: "AppCitizens",
                column: "EducationQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_EthnicityId",
                table: "AppCitizens",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_FamilyTypeId",
                table: "AppCitizens",
                column: "FamilyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_HouseholdId",
                table: "AppCitizens",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_LanguageId",
                table: "AppCitizens",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_ReligionId",
                table: "AppCitizens",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCitizens_WardId_Gender",
                table: "AppCitizens",
                columns: new[] { "WardId", "Gender" });

            migrationBuilder.CreateIndex(
                name: "IX_AppEducationQualifications_Name",
                table: "AppEducationQualifications",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppEthnicities_Name",
                table: "AppEthnicities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppFamilyTypes_Name",
                table: "AppFamilyTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppHouseholds_HouseNumber",
                table: "AppHouseholds",
                column: "HouseNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AppHouseholds_WardId",
                table: "AppHouseholds",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLanguages_Name",
                table: "AppLanguages",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppReligions_Name",
                table: "AppReligions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppWards_WardNumber",
                table: "AppWards",
                column: "WardNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCitizens");

            migrationBuilder.DropTable(
                name: "AppEducationQualifications");

            migrationBuilder.DropTable(
                name: "AppEthnicities");

            migrationBuilder.DropTable(
                name: "AppFamilyTypes");

            migrationBuilder.DropTable(
                name: "AppHouseholds");

            migrationBuilder.DropTable(
                name: "AppLanguages");

            migrationBuilder.DropTable(
                name: "AppReligions");

            migrationBuilder.DropTable(
                name: "AppWards");
        }
    }
}
