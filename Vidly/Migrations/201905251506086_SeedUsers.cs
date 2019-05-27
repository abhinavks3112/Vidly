namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4ac85dfa-7d61-4c25-a752-9e8b1832b36f', N'guest@vidly.com', 0, N'AFnAMhEmcpXUsbchayMkIaM6l4Le71rUFRhvpQUYxX4bP86mwU7l+hObC6ps61/Ikw==', N'651f2dd7-5b2e-446e-a8a6-c04de00c5eab', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7a130aca-0b8e-4753-995e-82244ebb480f', N'admin@vidly.com', 0, N'AHtGj8QhIOoAaCcAANAvjNPSHtsHX10c0Utss2seNNqaUBAzrXoMa6ddrRcja+Yk2g==', N'b26a7834-6fc8-48f5-9018-0a8337249ab0', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4db28e50-049f-4de4-8722-1246fc3b859c', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7a130aca-0b8e-4753-995e-82244ebb480f', N'4db28e50-049f-4de4-8722-1246fc3b859c')

");
        }
        
        public override void Down()
        {
        }
    }
}
