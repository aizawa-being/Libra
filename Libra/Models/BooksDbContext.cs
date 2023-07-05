namespace Libra.Models {
    using SQLite.CodeFirst;
    using System.Data.Common;
    using System.Data.Entity;

    public class BooksDbContext : DbContext {

        public BooksDbContext()
            : base("name=BooksDbContext") {
        }

        public BooksDbContext(DbConnection vConnection, bool vContextOwnsConnection)
            : base(vConnection, vContextOwnsConnection) {
        }
        
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// CodeFirstでデータベースを作成します。
        /// </summary>
        /// <param name="vModelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder vModelBuilder) {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<BooksDbContext>(vModelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

    }
}