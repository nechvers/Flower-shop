using System.Data.Entity;

namespace PracticeAppWPF
{
    public static class Database
    {
        private static practice04Entities s_entities = new practice04Entities();
        public static Staff CurrentUser { get; set; }

        public static DbSet<Staff> Staffs => s_entities.Staffs;
        public static DbSet<Post> Posts => s_entities.Posts;
        public static DbSet<Role> Roles => s_entities.Roles;
        public static DbSet<Division> Divisions=> s_entities.Divisions;

        public static void SaveChanges() => s_entities.SaveChanges();
    }
}
