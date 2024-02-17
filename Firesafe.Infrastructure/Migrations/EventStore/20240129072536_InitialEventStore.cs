using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.EventStore;

/// <inheritdoc />
public partial class InitialEventStore : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "ExceptionEvents",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Message = table.Column<string>("character varying(10000)", maxLength: 10000, nullable: false),
                StackTrace = table.Column<string>("character varying(100000)", maxLength: 100000, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ExceptionEvents", x => x.Id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ExceptionEvents");
    }
}