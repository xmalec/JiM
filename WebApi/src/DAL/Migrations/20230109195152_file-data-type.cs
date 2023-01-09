using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class filedatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Data",
                table: "Files",
                type: "MediumBlob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$f5RNttayR2Ec.pMwygHMj.4UtCiUPnM59AijOf5J7AtfusB.V0JBu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$HhSS8k29kc66alPqi7661.5W4weWF4tHWMJW25qrPvuzaelM/eWre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Data",
                table: "Files",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "MediumBlob");

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
    }
}
