using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Models {

    /// <summary>
    /// 書籍情報クラス
    /// </summary>
    /// <remarks>
    /// このクラスを基にDBの書籍情報テーブルが作成されます。
    /// </remarks>
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
        [Required]
        public int IsDeleted { get; set; }
        public string UserName { get; set; }
        public string BorrowingDate { get; set; }
    }
}
