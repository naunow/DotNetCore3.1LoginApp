using LoginApp.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace LoginApp.Migrations
{
    public partial class _20210703_InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            // Seed Data
            migrationBuilder.InsertData(
                table: "Users",
                columns: new string[] { "Id", "Login_Id", "Password", "FirstName", "LastName", "Age" },
                values: new object[,]
                {
                    {"1","mikesmith","pass","Mike","Smith","20" },
                    {"2","lisajohnson","pass","Lisa","Johnson","21" },
                    {"3","bobwilliams","pass","Bob","Williams","22" },
                    {"4","alicegarcia","pass","Alice","Garcia","23" },
                    {"5","charlesmiller","pass","Charles","Miller","24" },
                    {"6","takeshikondo","pass","Takeshi","Kondo","25" },
                }
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
