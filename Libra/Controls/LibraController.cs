namespace Libra.Controls {
    public class LibraController {

        public LibraController() {
        }

        public void OpenAddForm() {
            var addBookFormController = new AddBookFormController();
            addBookFormController.ShowAddBookForm();
        }
    }
}
