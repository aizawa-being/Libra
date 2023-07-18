using static Libra.BooksDataSet;

namespace Libra {
    /// <summary>
    /// 書籍テーブル
    /// </summary>
    public class BooksTable {
        public BooksDataTable Books { get; set; } = new BooksDataTable();
    }
}