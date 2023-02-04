using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Email.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Users_SenderId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMail_Emails_ReceivedMailsId",
                table: "UserMail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emails",
                table: "Emails");

            migrationBuilder.RenameTable(
                name: "Emails",
                newName: "Mails");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_SenderId",
                table: "Mails",
                newName: "IX_Mails_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mails",
                table: "Mails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Probas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Probas", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Users_SenderId",
                table: "Mails",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMail_Mails_ReceivedMailsId",
                table: "UserMail",
                column: "ReceivedMailsId",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Users_SenderId",
                table: "Mails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMail_Mails_ReceivedMailsId",
                table: "UserMail");

            migrationBuilder.DropTable(
                name: "Probas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mails",
                table: "Mails");

            migrationBuilder.RenameTable(
                name: "Mails",
                newName: "Emails");

            migrationBuilder.RenameIndex(
                name: "IX_Mails_SenderId",
                table: "Emails",
                newName: "IX_Emails_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emails",
                table: "Emails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Users_SenderId",
                table: "Emails",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMail_Emails_ReceivedMailsId",
                table: "UserMail",
                column: "ReceivedMailsId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
