using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra.Models {
    public class MessageBoxService : IMessageBoxService {
        public DialogResult Show(string message) {
            return MessageBox.Show(message);
        }

        public DialogResult Show(string message, string caption) {
            return MessageBox.Show(message, caption);
        }

        public DialogResult Show(string message, string caption, MessageBoxButtons button) {
            return MessageBox.Show(message, caption, button);
        }
    }
}
