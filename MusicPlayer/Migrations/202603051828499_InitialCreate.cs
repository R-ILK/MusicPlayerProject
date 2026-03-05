namespace MusicPlayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackID = c.Int(nullable: false, identity: true),
                        Cover = c.String(),
                        Title = c.String(),
                        Artist = c.String(),
                        Length = c.String(),
                    })
                .PrimaryKey(t => t.TrackID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tracks");
        }
    }
}
