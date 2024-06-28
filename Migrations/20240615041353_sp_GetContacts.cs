using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagementV2.Migrations
{
    /// <inheritdoc />
    public partial class sp_GetContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE sp_GetConatcts
                    AS
                    BEGIN
                        SELECT * FROM Contact
                    END
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE sp_GetContacts");
        }
    }
}
