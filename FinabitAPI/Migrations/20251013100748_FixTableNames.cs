using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinabitAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the incorrectly created foreign key first
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_HGuest_Id",
                table: "Patients");

            // Drop the incorrectly created HGuest table (tblHGuest already exists)
            migrationBuilder.DropTable("HGuest");

            // Rename Patients table to follow clinic naming convention
            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "tblCLPatients");

            // Add foreign key constraint to existing tblHGuest table
            migrationBuilder.AddForeignKey(
                name: "FK_tblCLPatients_tblHGuest_Id",
                table: "tblCLPatients",
                column: "Id",
                principalTable: "tblHGuest",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_tblCLPatients_tblHGuest_Id",
                table: "tblCLPatients");

            // Rename table back to original name
            migrationBuilder.RenameTable(
                name: "tblCLPatients",
                newName: "Patients");

            // Recreate the temporary HGuest table (for rollback purposes)
            migrationBuilder.CreateTable(
                name: "HGuest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HGuest", x => x.ID);
                });

            // Add foreign key to temporary HGuest table
            migrationBuilder.AddForeignKey(
                name: "FK_Patients_HGuest_Id",
                table: "Patients",
                column: "Id",
                principalTable: "HGuest",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
