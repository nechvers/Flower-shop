using System.Data.Entity;

namespace PracticeAppWPF
{
    public static class Database
    {
        private static practiceEntities s_entities = new practiceEntities();
        

        public static DbSet<Staff> Staffs => s_entities.Staffs;
        public static DbSet<Post> Posts => s_entities.Posts;
        public static DbSet<Role> Roles => s_entities.Roles;
        public static DbSet<Division> Divisions=> s_entities.Divisions;
        public static DbSet<Trash> Trashes => s_entities.Trashes;
        public static DbSet<Flower> Flowers => s_entities.Flowers;

        public static void SaveChanges() => s_entities.SaveChanges();
    }
}
