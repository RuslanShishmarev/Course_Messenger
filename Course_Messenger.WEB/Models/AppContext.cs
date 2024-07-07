using Microsoft.EntityFrameworkCore;

namespace Course_Messenger.WEB.Models
{
    public class AppContext : DbContext
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = "Course_Messenger";
            optionsBuilder.UseSqlServer($@"Server=(localdb)\mssqllocaldb;Database={dbName};Trusted_Connection=True;");
        }
    }
}
