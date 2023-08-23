using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicAPI.Migrations
{
    /// <inheritdoc />
    public partial class DefaultVillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "City", "CreatedDate", "ImageUrl", "Name", "Rate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "New York", new DateTime(2023, 8, 23, 20, 24, 41, 262, DateTimeKind.Local).AddTicks(5672), "https://www.villaabiente.com/resources/abiente/headers/mobile/xVilla,P20Abiente,P20-,P20Magnificent,P20exterior,P20villa.jpg.pagespeed.ic.e6bNABZFo5.jpg", "Royal", 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Berlin", new DateTime(2023, 8, 23, 20, 24, 41, 262, DateTimeKind.Local).AddTicks(5703), "https://image.urlaubspiraten.de/1024/image/upload/v1669132243/Impressions%20and%20Other%20Assets/28ad22b7-e6e8-41dd-94f8-ad3490ac205c_ygxlws.webp", "Sun", 8.6999999999999993, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
