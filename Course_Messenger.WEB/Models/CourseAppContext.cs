using Microsoft.EntityFrameworkCore;

namespace Course_Messenger.WEB.Models
{
    public class CourseAppContext : DbContext
    {
        /// <summary>
        /// Таблица пользователей
        /// </summary>
        public DbSet<UserModel> Users { get; set; } = null!;

        /// <summary>
        /// Таблица сообщений
        /// </summary>
        public DbSet<MessageModel> Messages { get; set; } = null!;

        /// <summary>
        /// Таблица чатов
        /// </summary>
        public DbSet<ChatModel> Chats { get; set; } = null!;

        //public AppContext()
        //{
        //    // Database.EnsureDeleted(); - не обязательно. Удалит все, вместе с данными!
        //    //Database.EnsureCreated();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = "CourseMessenger";
            optionsBuilder.UseSqlServer($@"Server=(localdb)\mssqllocaldb;Database={dbName};Trusted_Connection=True;");
        }
    }
}
