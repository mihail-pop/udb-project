﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectOBS.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postalcode = table.Column<int>(type: "int", nullable: true),
                    Block = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    Apartment = table.Column<int>(type: "int", nullable: true),
                    Staircase = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CVV = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Card = table.Column<int>(type: "int", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    IBAN = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<int>(type: "int", nullable: true),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ClientId1 = table.Column<int>(type: "int", nullable: false),
                    IBAN = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    WithdrawalId = table.Column<int>(type: "int", nullable: false),
                    DepositId = table.Column<int>(type: "int", nullable: false),
                    TransferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Deposit_DepositId",
                        column: x => x.DepositId,
                        principalTable: "Deposit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Transfer_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Withdrawal_WithdrawalId",
                        column: x => x.WithdrawalId,
                        principalTable: "Withdrawal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_AddressId",
                table: "Client",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_CardId",
                table: "Client",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ClientId",
                table: "Transactions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DepositId",
                table: "Transactions",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransferId",
                table: "Transactions",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WithdrawalId",
                table: "Transactions",
                column: "WithdrawalId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_ClientId",
                table: "Transfer",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "Withdrawal");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Card");
        }
    }
}
