namespace MusicPlayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAudioUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "AudioUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "AudioUrl");
        }
    }
}
