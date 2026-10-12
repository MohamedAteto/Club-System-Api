using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Club_Management_System_Api.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FacilityCapacity = table.Column<int>(type: "int", nullable: false),
                    FacilityHourlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MemberEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BoookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    GroupSize = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    FacilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BoookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberShips",
                columns: table => new
                {
                    MemberShipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberShipType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberShipStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberShipEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberShipPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberShipStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShips", x => x.MemberShipId);
                    table.ForeignKey(
                        name: "FK_MemberShips_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberShipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_MemberShips_MemberShipId",
                        column: x => x.MemberShipId,
                        principalTable: "MemberShips",
                        principalColumn: "MemberShipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "FacilityId", "FacilityCapacity", "FacilityHourlyPrice", "FacilityName" },
                values: new object[,]
                {
                    { 1, 30, 250m, "Swimming Pool" },
                    { 2, 40, 150m, "Gym Hall" },
                    { 3, 10, 200m, "Tennis Court" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "MemberBirthDate", "MemberEmail", "MemberFullName", "MemberPhone" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed@example.com", "Ahmed Mohamed", "01011112222" },
                    { 2, new DateTime(1998, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "mona@example.com", "Mona Ali", "01022223333" },
                    { 3, new DateTime(2002, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar@example.com", "Omar Hassan", "01033334444" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BoookingId", "BookingDate", "EndTime", "FacilityId", "GroupSize", "MemberId", "StartTime", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), 1, 10, 1, new TimeSpan(0, 10, 0, 0, 0), "Confirmed" },
                    { 2, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0), 2, 8, 2, new TimeSpan(0, 14, 0, 0, 0), "Confirmed" },
                    { 3, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 0, 0, 0), 3, 4, 3, new TimeSpan(0, 16, 0, 0, 0), "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "MemberShips",
                columns: new[] { "MemberShipId", "MemberId", "MemberShipEndDate", "MemberShipPrice", "MemberShipStartDate", "MemberShipStatus", "MemberShipType" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Gold" },
                    { 2, 2, new DateTime(2027, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Silver" },
                    { 3, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expired", "Basic" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "MemberShipId", "PaymentAmount", "PaymentDate", "PaymentMethod" },
                values: new object[,]
                {
                    { 1, 1, 5000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Card" },
                    { 2, 2, 3000m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash" },
                    { 3, 3, 1500m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallet" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FacilityId",
                table: "Bookings",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MemberId",
                table: "Bookings",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityName",
                table: "Facilities",
                column: "FacilityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_MemberId",
                table: "MemberShips",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MemberShipId",
                table: "Payments",
                column: "MemberShipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "MemberShips");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
