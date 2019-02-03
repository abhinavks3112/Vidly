namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("Insert Into Genres (Id, Name) Values (1, 'Comedy')");
            Sql("Insert Into Genres (Id, Name) Values (2, 'Drama')");
            Sql("Insert Into Genres (Id, Name) Values (3, 'Action')");
            Sql("Insert Into Genres (Id, Name) Values (4, 'Thriller')");
            Sql("Insert Into Genres (Id, Name) Values (5, 'Family')");
            Sql("Insert Into Genres (Id, Name) Values (6, 'Romance')");
            Sql("Insert Into Genres (Id, Name) Values (7, 'Horror')");
        }
        
        public override void Down()
        {
        }
    }
}
