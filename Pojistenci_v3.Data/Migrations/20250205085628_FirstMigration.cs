using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pojistenci_v3.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_AspNetUsers_InsuredId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_AspNetUsers_InsurerId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "CustomerNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "InsurerId",
                table: "Insurances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Insureds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR CustomerNumberSequence")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insureds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insureds_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Insureds_InsuredId",
                table: "Insurances",
                column: "InsuredId",
                principalTable: "Insureds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Insurers_InsurerId",
                table: "Insurances",
                column: "InsurerId",
                principalTable: "Insurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Insureds_InsuredId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Insurers_InsurerId",
                table: "Insurances");

            migrationBuilder.DropTable(
                name: "Insureds");

            migrationBuilder.DropTable(
                name: "Insurers");

            migrationBuilder.AlterColumn<string>(
                name: "InsurerId",
                table: "Insurances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerNumber",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValueSql: "NEXT VALUE FOR CustomerNumberSequence");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_AspNetUsers_InsuredId",
                table: "Insurances",
                column: "InsuredId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_AspNetUsers_InsurerId",
                table: "Insurances",
                column: "InsurerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
