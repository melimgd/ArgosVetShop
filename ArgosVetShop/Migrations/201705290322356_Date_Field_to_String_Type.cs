namespace ArgosVetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Date_Field_to_String_Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppointmentModels", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppointmentModels", "Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
