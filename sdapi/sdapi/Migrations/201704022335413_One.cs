namespace sdapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class One : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SensorImages", "TimeStamp", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SensorImages", "TimeStamp", c => c.DateTime(nullable: false));
        }
    }
}
