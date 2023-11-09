using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingTest.Campaign.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddIdShoppingCampaign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignSet",
                table: "CampaignSet");

            migrationBuilder.RenameTable(
                name: "CampaignSet",
                newName: "Campaign");

            migrationBuilder.AddColumn<string>(
                name: "IdShopping",
                table: "Campaign",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "IdShopping",
                table: "Campaign");

            migrationBuilder.RenameTable(
                name: "Campaign",
                newName: "CampaignSet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignSet",
                table: "CampaignSet",
                column: "Id");
        }
    }
}
