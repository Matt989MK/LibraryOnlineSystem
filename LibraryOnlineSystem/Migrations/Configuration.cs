﻿namespace LibraryOnlineSystem.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryOnlineSystem.Models.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LibraryOnlineSystem.Models.LibraryContext";
        }

        protected override void Seed(LibraryOnlineSystem.Models.LibraryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
