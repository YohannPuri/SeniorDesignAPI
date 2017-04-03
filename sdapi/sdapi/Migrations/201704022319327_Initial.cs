namespace sdapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorImages",
                c => new
                    {
                        SensorImageId = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.SensorImageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SensorImages");
        }
    }
}
