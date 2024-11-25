using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingService.Booking.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCatalogRequestIdNotKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Guid",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "CatalogRequestId",
                table: "Bookings",
                newName: "catalog_request_id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "catalog_request_id",
                table: "Bookings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_bookings",
                table: "Bookings",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_bookings",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "catalog_request_id",
                table: "Bookings",
                newName: "CatalogRequestId");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "CatalogRequestId",
                table: "Bookings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "Guid",
                table: "Bookings",
                column: "CatalogRequestId");
        }
    }
}
