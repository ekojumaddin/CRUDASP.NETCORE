using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core1.Data.Migrations
{
    public partial class AddingModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "Supplier",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeleteDate",
                table: "Supplier",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Supplier",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Supplier",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Supplier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Supplier",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeleteDate",
                table: "Supplier",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Supplier",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
