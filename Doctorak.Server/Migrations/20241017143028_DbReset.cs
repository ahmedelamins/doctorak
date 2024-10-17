using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctorak.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlot_Doctors_DoctorId",
                table: "AvailabilitySlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilitySlot",
                table: "AvailabilitySlot");

            migrationBuilder.RenameTable(
                name: "AvailabilitySlot",
                newName: "AvailabilitySlots");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilitySlot_DoctorId",
                table: "AvailabilitySlots",
                newName: "IX_AvailabilitySlots_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilitySlots",
                table: "AvailabilitySlots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlots_Doctors_DoctorId",
                table: "AvailabilitySlots",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlots_Doctors_DoctorId",
                table: "AvailabilitySlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilitySlots",
                table: "AvailabilitySlots");

            migrationBuilder.RenameTable(
                name: "AvailabilitySlots",
                newName: "AvailabilitySlot");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilitySlots_DoctorId",
                table: "AvailabilitySlot",
                newName: "IX_AvailabilitySlot_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilitySlot",
                table: "AvailabilitySlot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlot_Doctors_DoctorId",
                table: "AvailabilitySlot",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
