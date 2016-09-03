namespace InstaSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikesAndCommentModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 150),
                        Timestamp = c.DateTime(nullable: false),
                        Post_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Post_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Post_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Post_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropIndex("dbo.Likes", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
        }
    }
}
