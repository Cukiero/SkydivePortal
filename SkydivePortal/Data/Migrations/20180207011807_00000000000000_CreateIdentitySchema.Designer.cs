using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SkydivePortal.Data.Migrations
{
    public partial class _00000000000000_CreateIdentitySchemaDesigner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParachuteId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Img = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PagePosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagePosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parachutes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Manufacturer = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parachutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dropzones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dropzones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dropzones_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DropzoneId = table.Column<int>(nullable: true),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationRoles_Dropzones_DropzoneId",
                        column: x => x.DropzoneId,
                        principalTable: "Dropzones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dropzone_Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DropzoneId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dropzone_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dropzone_Events_Dropzones_DropzoneId",
                        column: x => x.DropzoneId,
                        principalTable: "Dropzones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dropzone_Events_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dropzone_Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    DropzoneId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dropzone_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dropzone_Posts_Dropzones_DropzoneId",
                        column: x => x.DropzoneId,
                        principalTable: "Dropzones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dropzone_User_Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DropzoneId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dropzone_User_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dropzone_User_Posts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dropzone_User_Posts_Dropzones_DropzoneId",
                        column: x => x.DropzoneId,
                        principalTable: "Dropzones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserJumps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DropzoneId = table.Column<int>(nullable: true),
                    Height = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Parachute = table.Column<string>(nullable: true),
                    Plane = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJumps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserJumps_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserJumps_Dropzones_DropzoneId",
                        column: x => x.DropzoneId,
                        principalTable: "Dropzones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRoles",
                columns: table => new
                {
                    ApplicationRoleId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRoles", x => new { x.ApplicationRoleId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dropzone_Post_Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dropzone_PostId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dropzone_Post_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dropzone_Post_Images_Dropzone_Posts_Dropzone_PostId",
                        column: x => x.Dropzone_PostId,
                        principalTable: "Dropzone_Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dropzone_Post_Images_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dropzone_User_Post_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Dropzone_User_PostId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dropzone_User_Post_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dropzone_User_Post_Comments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dropzone_User_Post_Comments_Dropzone_User_Posts_Dropzone_User_PostId",
                        column: x => x.Dropzone_User_PostId,
                        principalTable: "Dropzone_User_Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dropzone_User_Post_Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dropzone_User_PostId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dropzone_User_Post_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dropzone_User_Post_Images_Dropzone_User_Posts_Dropzone_User_PostId",
                        column: x => x.Dropzone_User_PostId,
                        principalTable: "Dropzone_User_Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dropzone_User_Post_Images_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParachuteId",
                table: "AspNetUsers",
                column: "ParachuteId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RegionId",
                table: "AspNetUsers",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoles_DropzoneId",
                table: "ApplicationRoles",
                column: "DropzoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ApplicationUserId",
                table: "ApplicationUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_Events_DropzoneId",
                table: "Dropzone_Events",
                column: "DropzoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_Events_ImageId",
                table: "Dropzone_Events",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_Post_Images_Dropzone_PostId",
                table: "Dropzone_Post_Images",
                column: "Dropzone_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_Post_Images_ImageId",
                table: "Dropzone_Post_Images",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_Posts_DropzoneId",
                table: "Dropzone_Posts",
                column: "DropzoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_User_Post_Comments_ApplicationUserId",
                table: "Dropzone_User_Post_Comments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_User_Post_Comments_Dropzone_User_PostId",
                table: "Dropzone_User_Post_Comments",
                column: "Dropzone_User_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_User_Post_Images_Dropzone_User_PostId",
                table: "Dropzone_User_Post_Images",
                column: "Dropzone_User_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_User_Post_Images_ImageId",
                table: "Dropzone_User_Post_Images",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_User_Posts_ApplicationUserId",
                table: "Dropzone_User_Posts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzone_User_Posts_DropzoneId",
                table: "Dropzone_User_Posts",
                column: "DropzoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Dropzones_RegionId",
                table: "Dropzones",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJumps_ApplicationUserId",
                table: "UserJumps",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJumps_DropzoneId",
                table: "UserJumps",
                column: "DropzoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Parachutes_ParachuteId",
                table: "AspNetUsers",
                column: "ParachuteId",
                principalTable: "Parachutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Regions_RegionId",
                table: "AspNetUsers",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parachutes_ParachuteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Regions_RegionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ApplicationUserRoles");

            migrationBuilder.DropTable(
                name: "Dropzone_Events");

            migrationBuilder.DropTable(
                name: "Dropzone_Post_Images");

            migrationBuilder.DropTable(
                name: "Dropzone_User_Post_Comments");

            migrationBuilder.DropTable(
                name: "Dropzone_User_Post_Images");

            migrationBuilder.DropTable(
                name: "PagePosts");

            migrationBuilder.DropTable(
                name: "Parachutes");

            migrationBuilder.DropTable(
                name: "UserJumps");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "Dropzone_Posts");

            migrationBuilder.DropTable(
                name: "Dropzone_User_Posts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Dropzones");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParachuteId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RegionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParachuteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
