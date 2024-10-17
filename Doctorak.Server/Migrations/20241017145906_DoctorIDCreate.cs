using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctorak.Server.Migrations
{
    /// <inheritdoc />
    public partial class DoctorIDCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlots_Doctors_UserId",
                table: "AvailabilitySlots");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AvailabilitySlots",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilitySlots_UserId",
                table: "AvailabilitySlots",
                newName: "IX_AvailabilitySlots_DoctorId");

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

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "AvailabilitySlots",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilitySlots_DoctorId",
                table: "AvailabilitySlots",
                newName: "IX_AvailabilitySlots_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlots_Doctors_UserId",
                table: "AvailabilitySlots",
                column: "UserId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
