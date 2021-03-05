using Microsoft.EntityFrameworkCore.Migrations;

namespace shopAZ_API.Migrations
{
    public partial class UserRoleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRolePivot_Roles_RoleId",
                table: "UserRolePivot");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRolePivot_Users_UserId",
                table: "UserRolePivot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRolePivot",
                table: "UserRolePivot");

            migrationBuilder.RenameTable(
                name: "UserRolePivot",
                newName: "UserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRolePivot_UserId",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRolePivot_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRolePivot");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRolePivot",
                newName: "IX_UserRolePivot_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRolePivot",
                newName: "IX_UserRolePivot_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRolePivot",
                table: "UserRolePivot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRolePivot_Roles_RoleId",
                table: "UserRolePivot",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRolePivot_Users_UserId",
                table: "UserRolePivot",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
