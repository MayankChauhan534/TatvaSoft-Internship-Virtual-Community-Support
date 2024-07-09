using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data_Logic_Layer.Migrations
{
    /// <inheritdoc />
    public partial class MissionMissionTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedData",
                table: "UserDetail",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreateData",
                table: "UserDetail",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedData",
                table: "User",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreateData",
                table: "User",
                newName: "CreatedDate");

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MissionTitle = table.Column<string>(type: "text", nullable: true),
                    MissionDescription = table.Column<string>(type: "text", nullable: true),
                    MissionOrganisationName = table.Column<string>(type: "text", nullable: true),
                    MissionOrganisationDetail = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    MissionType = table.Column<string>(type: "text", nullable: true),
                    TotalSheets = table.Column<int>(type: "integer", nullable: true),
                    RegistrationDeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    MissionThemeId = table.Column<string>(type: "text", nullable: true),
                    MissionSkillId = table.Column<string>(type: "text", nullable: true),
                    MissionImages = table.Column<string>(type: "text", nullable: true),
                    MissionDocuments = table.Column<string>(type: "text", nullable: true),
                    MissionAvilability = table.Column<string>(type: "text", nullable: true),
                    MissionVideoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionTheme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ThemeName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionTheme", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "MissionTheme");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "UserDetail",
                newName: "ModifiedData");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "UserDetail",
                newName: "CreateData");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "User",
                newName: "ModifiedData");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "User",
                newName: "CreateData");
        }
    }
}
