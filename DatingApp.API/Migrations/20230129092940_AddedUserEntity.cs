using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", true),
                    UserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
