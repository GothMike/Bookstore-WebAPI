﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PublishingHouses_BookId",
                table: "PublishingHouses");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "PublishingHouses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PublishingHouses_BookId",
                table: "PublishingHouses",
                column: "BookId",
                unique: true,
                filter: "[BookId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PublishingHouses_BookId",
                table: "PublishingHouses");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "PublishingHouses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublishingHouses_BookId",
                table: "PublishingHouses",
                column: "BookId",
                unique: true);
        }
    }
}
