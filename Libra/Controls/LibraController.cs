namespace Libra.Controls {
    public class LibraController {

        public LibraController() {
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public void OpenAddForm() {
            var addBookFormController = new AddBookFormController();
            addBookFormController.ShowAddBookForm();
        }
    }
}
