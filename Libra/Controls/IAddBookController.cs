using Libra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Controls {
    /// <summary>
    /// 書籍追加用コントローラーのインターフェースです。
    /// </summary>
    public interface IAddBookController {
        Task SetAddBook(string vIsbn);
        Book GetAddBook();
    }
}
