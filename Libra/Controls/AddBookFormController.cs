using Libra.Views;

namespace Libra.Controls {
    public class AddBookFormController {

        public AddBookFormController() {
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public void ShowAddBookForm() {
            using (var addBookForm = new AddBookForm()) {
                addBookForm.ShowDialog();
            }
        }
    }
}
