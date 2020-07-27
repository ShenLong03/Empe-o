namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChangeProfileUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perfils", "Perfil_PerfilId", c => c.Int());
            CreateIndex("dbo.Perfils", "Perfil_PerfilId");
            AddForeignKey("dbo.Perfils", "Perfil_PerfilId", "dbo.Perfils", "PerfilId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Perfils", "Perfil_PerfilId", "dbo.Perfils");
            DropIndex("dbo.Perfils", new[] { "Perfil_PerfilId" });
            DropColumn("dbo.Perfils", "Perfil_PerfilId");
        }
    }
}
