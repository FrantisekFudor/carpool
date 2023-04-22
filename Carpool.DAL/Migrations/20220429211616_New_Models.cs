using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carpool.DAL.Migrations
{
    public partial class New_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             /*migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_UserEntityId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_UserEntityId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Rides");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
             /*migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Rides",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_UserEntityId",
                table: "Rides",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_UserEntityId",
                table: "Rides",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");*/
        }
    }
}
