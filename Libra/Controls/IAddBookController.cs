using Libra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Controls {
    public interface IAddBookController {
        Task<bool> SetAddBook(string vIsbn);
        Book GetAddBook();
    }
}
