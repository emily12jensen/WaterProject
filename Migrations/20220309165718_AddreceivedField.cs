﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterProject.Migrations
{
    public partial class AddreceivedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DonationReceived",
                table: "Donations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonationReceived",
                table: "Donations");
        }
    }
}
