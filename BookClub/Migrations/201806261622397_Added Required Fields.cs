namespace BookClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Genre", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Synopsis", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "ImageUrl");
            DropColumn("dbo.AspNetUsers", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
            AddColumn("dbo.AspNetUsers", "ImageUrl", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.Books", "ImageUrl", c => c.String());
            AlterColumn("dbo.Books", "Synopsis", c => c.String());
            AlterColumn("dbo.Books", "Genre", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
            DropColumn("dbo.AspNetUsers", "Avatar");
        }
    }
}
