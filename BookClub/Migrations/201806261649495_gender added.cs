namespace BookClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genderadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}
