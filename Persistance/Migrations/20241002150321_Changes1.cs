using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Changes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_positions_PositionId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_positionHistories_positions_PositionId",
                table: "positionHistories");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "positionHistories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "employees",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_positions_PositionId",
                table: "employees",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_positionHistories_positions_PositionId",
                table: "positionHistories",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_positions_PositionId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_positionHistories_positions_PositionId",
                table: "positionHistories");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "positionHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "employees",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_positions_PositionId",
                table: "employees",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_positionHistories_positions_PositionId",
                table: "positionHistories",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
