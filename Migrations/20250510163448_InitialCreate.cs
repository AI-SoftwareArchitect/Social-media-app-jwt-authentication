using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthenticationApiProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    ProfileImage = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FriendId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "ProfileImage", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "a@mail.com", "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX12", null, "alice" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "b@mail.com", "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX34", null, "bob" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "c@mail.com", "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX56", null, "carol" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "d@mail.com", "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX78", null, "dan" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "e@mail.com", "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX90", null, "erin" }
                });

            migrationBuilder.InsertData(
                table: "Friends",
                columns: new[] { "Id", "FriendId", "UserId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
