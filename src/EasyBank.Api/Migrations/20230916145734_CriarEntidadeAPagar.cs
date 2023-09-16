﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyBank.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadeAPagar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apagar",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    ValorOriginal = table.Column<double>(type: "double precision", nullable: false),
                    ValorPago = table.Column<double>(type: "double precision", nullable: false),
                    Observacao = table.Column<string>(type: "VARCHAR", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataReferencia = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    IdNaturezaDeLancamento = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_apagar_naturezadelancamento_IdNaturezaDeLancamento",
                        column: x => x.IdNaturezaDeLancamento,
                        principalTable: "naturezadelancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_apagar_usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apagar_IdNaturezaDeLancamento",
                table: "apagar",
                column: "IdNaturezaDeLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_apagar_IdUsuario",
                table: "apagar",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apagar");
        }
    }
}
