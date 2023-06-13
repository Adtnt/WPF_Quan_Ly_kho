namespace WPF_Quan_Ly_kho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        MoreInfo = c.String(),
                        ContractDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Outputs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateOutput = c.DateTime(nullable: false),
                        Customer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.Inputs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateInput = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Supliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        MoreInfo = c.String(),
                        ContractDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
               "dbo.Materials",
               c => new
               {
                   ID = c.Int(nullable: false, identity: true),
                   DisplayName = c.String(),
                   IDUnit = c.Int(nullable: false),
                   IDSuplier = c.Int(nullable: false),
                   QRCode = c.String(),
                   BarCode = c.String(),
                   InputInfo_ID = c.Int(),
                   OutputInfo_ID = c.Int(),
                   Suplier_ID = c.Int(),
                   Unit_ID = c.Int(),
               })
               .PrimaryKey(t => t.ID)
               .ForeignKey("dbo.Supliers", t => t.Suplier_ID)
               .ForeignKey("dbo.Units", t => t.Unit_ID)
               .Index(t => t.Suplier_ID)
               .Index(t => t.Unit_ID);

            CreateTable(
               "dbo.OutputInfoes",
               c => new
               {
                   ID = c.Int(nullable: false, identity: true),
                   IDMaterial = c.Int(nullable: false),
                   IDOutput = c.Int(nullable: false),
                   IDCustomer = c.Int(nullable: false),
                   Count = c.Int(nullable: false),
                   OutputPrice = c.Int(nullable: false),
                   Customer_ID = c.Int(),
                   Output_ID = c.Int(),
               })
               .PrimaryKey(t => t.ID)
               .ForeignKey("dbo.Customers", t => t.Customer_ID)
               .ForeignKey("dbo.Outputs", t => t.Output_ID)
               .ForeignKey("dbo.Materials", t => t.IDMaterial)
               .Index(t => t.Customer_ID)
               .Index(t => t.Output_ID)
               .Index(t => t.IDMaterial);

            CreateTable(
                "dbo.InputInfoes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    IDMaterial = c.Int(nullable: false),
                    IDInput = c.Int(nullable: false),
                    Count = c.Int(nullable: false),
                    InputPrice = c.Int(nullable: false),
                    Input_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inputs", t => t.Input_ID)
                .ForeignKey("dbo.Materials", t => t.IDMaterial)
                .Index(t => t.Input_ID)
                .Index(t => t.IDMaterial);
            

            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        IDRole = c.Int(nullable: false),
                        UserRole_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_ID)
                .Index(t => t.UserRole_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRole_ID", "dbo.UserRoles");
            DropForeignKey("dbo.Outputs", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.OutputInfoes", "Output_ID", "dbo.Outputs");
            DropForeignKey("dbo.Materials", "Unit_ID", "dbo.Units");
            DropForeignKey("dbo.Materials", "Suplier_ID", "dbo.Supliers");
            DropForeignKey("dbo.InputInfos", "IDMaterial", "dbo.Materials");
            DropForeignKey("dbo.OutputInfos", "IDMaterial", "dbo.Materials");
            DropForeignKey("dbo.InputInfoes", "Input_ID", "dbo.Inputs");
            DropForeignKey("dbo.OutputInfoes", "Customer_ID", "dbo.Customers");
            DropIndex("dbo.Users", new[] { "UserRole_ID" });
            DropIndex("dbo.InputInfoes", new[] { "Input_ID" });
            DropIndex("dbo.Materials", new[] { "Unit_ID" });
            DropIndex("dbo.Materials", new[] { "Suplier_ID" });
            DropIndex("dbo.Materials", new[] { "OutputInfo_ID" });
            DropIndex("dbo.Materials", new[] { "InputInfo_ID" });
            DropIndex("dbo.OutputInfoes", new[] { "Output_ID" });
            DropIndex("dbo.OutputInfoes", new[] { "Customer_ID" });
            DropIndex("dbo.Outputs", new[] { "Customer_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Units");
            DropTable("dbo.Supliers");
            DropTable("dbo.Inputs");
            DropTable("dbo.InputInfoes");
            DropTable("dbo.Materials");
            DropTable("dbo.OutputInfoes");
            DropTable("dbo.Outputs");
            DropTable("dbo.Customers");
        }
    }
}
