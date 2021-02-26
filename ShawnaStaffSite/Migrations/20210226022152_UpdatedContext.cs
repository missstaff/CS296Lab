using Microsoft.EntityFrameworkCore.Migrations;

namespace Shawna_Staff.Migrations
{
    public partial class UpdatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_CommenterId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ForumPosts_ForumPostsPostID",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ForumPostsPostID",
                table: "Comments",
                newName: "IX_Comments_ForumPostsPostID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CommenterId",
                table: "Comments",
                newName: "IX_Comments_CommenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CommenterId",
                table: "Comments",
                column: "CommenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ForumPosts_ForumPostsPostID",
                table: "Comments",
                column: "ForumPostsPostID",
                principalTable: "ForumPosts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CommenterId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ForumPosts_ForumPostsPostID",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ForumPostsPostID",
                table: "Comment",
                newName: "IX_Comment_ForumPostsPostID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommenterId",
                table: "Comment",
                newName: "IX_Comment_CommenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_CommenterId",
                table: "Comment",
                column: "CommenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ForumPosts_ForumPostsPostID",
                table: "Comment",
                column: "ForumPostsPostID",
                principalTable: "ForumPosts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
