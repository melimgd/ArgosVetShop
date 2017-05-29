namespace ArgosVetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_Fields_on_AppointmentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppointmentModels", "StartTime", c => c.String());
            AlterColumn("dbo.AppointmentModels", "Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppointmentModels", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.AppointmentModels", "StartTime");
        }
    }
}
