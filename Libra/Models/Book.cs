using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libra.Models {

    /// <summary>
    /// 書籍クラス
    /// </summary>
    /// <remarks>
    /// DBの書籍情報テーブルを定義しています。
    /// </remarks>
    [Table("Book")]
    public class Book {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(13)]
        [MinLength(13)]
        public string Barcode { get; set; }
        public int IsDeleted { get; set; }
        public string UserName { get; set; }
        public string BorrowingDate { get; set; }
    }
}