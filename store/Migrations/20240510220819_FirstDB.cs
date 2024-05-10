using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace store.Migrations
{
    public partial class FirstDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointsFidelite = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QteStock = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCommande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commands_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "paniers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paniers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paniers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "att_produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valeur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_att_produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_att_produits_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Favorits",
                columns: table => new
                {
                    IdFavoris = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorits", x => x.IdFavoris);
                    table.ForeignKey(
                        name: "FK_Favorits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Favorits_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "photoProduits",
                columns: table => new
                {
                    PhotoProduitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photoProduits", x => x.PhotoProduitId);
                    table.ForeignKey(
                        name: "FK_photoProduits_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Variante",
                columns: table => new
                {
                    IdVariante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QteStock = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    ProduitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variante", x => x.IdVariante);
                    table.ForeignKey(
                        name: "FK_Variante_Products_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Paiements",
                columns: table => new
                {
                    IdPaiement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePaimenet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Montant = table.Column<double>(type: "float", nullable: false),
                    modePaiement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiements", x => x.IdPaiement);
                    table.ForeignKey(
                        name: "FK_Paiements_Commands_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Reclamations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reclamations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reclamations_Commands_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "att_variantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valeur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VarianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_att_variantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_att_variantes_Variante_VarianteId",
                        column: x => x.VarianteId,
                        principalTable: "Variante",
                        principalColumn: "IdVariante",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ligneCommandes",
                columns: table => new
                {
                    IdLigneCommande = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    ProduitUnitaire = table.Column<double>(type: "float", nullable: false),
                    VarianteId = table.Column<int>(type: "int", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ligneCommandes", x => x.IdLigneCommande);
                    table.ForeignKey(
                        name: "FK_ligneCommandes_Commands_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ligneCommandes_Variante_VarianteId",
                        column: x => x.VarianteId,
                        principalTable: "Variante",
                        principalColumn: "IdVariante",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LignePanier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PanierId = table.Column<int>(type: "int", nullable: false),
                    VarianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignePanier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LignePanier_paniers_PanierId",
                        column: x => x.PanierId,
                        principalTable: "paniers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LignePanier_Variante_VarianteId",
                        column: x => x.VarianteId,
                        principalTable: "Variante",
                        principalColumn: "IdVariante",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "photoVariantes",
                columns: table => new
                {
                    IdPhoto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VarianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photoVariantes", x => x.IdPhoto);
                    table.ForeignKey(
                        name: "FK_photoVariantes_Variante_VarianteId",
                        column: x => x.VarianteId,
                        principalTable: "Variante",
                        principalColumn: "IdVariante",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    VarianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reviews_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_reviews_Variante_VarianteId",
                        column: x => x.VarianteId,
                        principalTable: "Variante",
                        principalColumn: "IdVariante",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Retours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRetour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeRetour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LigneCommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retours_ligneCommandes_LigneCommandeId",
                        column: x => x.LigneCommandeId,
                        principalTable: "ligneCommandes",
                        principalColumn: "IdLigneCommande",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_att_produits_ProductId",
                table: "att_produits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_att_variantes_VarianteId",
                table: "att_variantes",
                column: "VarianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ClientId",
                table: "Commands",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorits_ClientId",
                table: "Favorits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorits_ProductId",
                table: "Favorits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ligneCommandes_CommandeId",
                table: "ligneCommandes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_ligneCommandes_VarianteId",
                table: "ligneCommandes",
                column: "VarianteId");

            migrationBuilder.CreateIndex(
                name: "IX_LignePanier_PanierId",
                table: "LignePanier",
                column: "PanierId");

            migrationBuilder.CreateIndex(
                name: "IX_LignePanier_VarianteId",
                table: "LignePanier",
                column: "VarianteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_CommandeId",
                table: "Paiements",
                column: "CommandeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paniers_ClientId",
                table: "paniers",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_photoProduits_ProductId",
                table: "photoProduits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_photoVariantes_VarianteId",
                table: "photoVariantes",
                column: "VarianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_CommandeId",
                table: "Reclamations",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Retours_LigneCommandeId",
                table: "Retours",
                column: "LigneCommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ClientId",
                table: "reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_VarianteId",
                table: "reviews",
                column: "VarianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Variante_ProduitId",
                table: "Variante",
                column: "ProduitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "att_produits");

            migrationBuilder.DropTable(
                name: "att_variantes");

            migrationBuilder.DropTable(
                name: "Favorits");

            migrationBuilder.DropTable(
                name: "LignePanier");

            migrationBuilder.DropTable(
                name: "Paiements");

            migrationBuilder.DropTable(
                name: "photoProduits");

            migrationBuilder.DropTable(
                name: "photoVariantes");

            migrationBuilder.DropTable(
                name: "Reclamations");

            migrationBuilder.DropTable(
                name: "Retours");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "paniers");

            migrationBuilder.DropTable(
                name: "ligneCommandes");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Variante");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
