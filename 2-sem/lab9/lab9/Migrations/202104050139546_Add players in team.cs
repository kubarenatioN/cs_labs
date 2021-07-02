namespace lab9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addplayersinteam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "Region", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "Region");
        }
    }
}
