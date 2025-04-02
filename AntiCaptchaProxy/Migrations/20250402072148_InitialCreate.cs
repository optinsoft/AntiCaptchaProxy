using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntiCaptchaProxy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntiCaptchaStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTaskCount = table.Column<int>(type: "integer", nullable: false),
                    CreateTaskSucceeded = table.Column<int>(type: "integer", nullable: false),
                    CreateTaskFailed = table.Column<int>(type: "integer", nullable: false),
                    CreateTaskErrors = table.Column<int>(type: "integer", nullable: false),
                    GetTaskResultCount = table.Column<int>(type: "integer", nullable: false),
                    GetTaskResultSucceeded = table.Column<int>(type: "integer", nullable: false),
                    GetTaskResultFailed = table.Column<int>(type: "integer", nullable: false),
                    GetTaskResultErrors = table.Column<int>(type: "integer", nullable: false),
                    LastBalance = table.Column<double>(type: "double precision", nullable: true),
                    LastBalanceTime = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntiCaptchaStats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntiCaptchaStats");
        }
    }
}
