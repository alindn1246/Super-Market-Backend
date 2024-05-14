﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HussieniSuperMarket.Migrations
{
    /// <inheritdoc />
    public partial class FourthChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Products");
        }
    }
}
