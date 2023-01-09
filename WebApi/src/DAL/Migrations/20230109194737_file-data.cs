using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class filedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Files",
                type: "longblob",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$B2wfb3ZRMIXBKcwiMxbM6.jiaf3voo4teuOHBkQgiFjhgj8jgdKX6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$W8e5QG3hNCaMEhWo59s78e8CvSDaNjU5mypYWgpBu80J/qUzdpArm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Files");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8tsZm9xd2TQPGudEwM5RLeJPc72fkAtW3iFQ9Wc.jHMpOCvNbTKbO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$hc5WuD4WMTWJGtivl.uCBOHPqPcKW2PwEe0jz/hy3.FXG5f8wYJEW");
        }
    }
}
