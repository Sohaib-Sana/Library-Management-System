using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
            //Application.Run(new Form1());
            //Application.Run(new Admin());
            //Application.Run(new Librarian());
            //Application.Run(new ViewLibrarian());
            //Application.Run(new UpdateLibrarian());
            //Application.Run(new DeleteLibrarian());
            //Application.Run(new Author());
            //Application.Run(new AddAuthor());
            //Application.Run(new DeleteAuthor());
            //Application.Run(new UpdateAuthor());
            //Application.Run(new Books());
            //Application.Run(new AddBook());
            //Application.Run(new DeleteBooks());
            //Application.Run(new UpdateBook());
            //Application.Run(new Publishers());
            //Application.Run(new AddPublisher());
            //Application.Run(new DeletePublisher());
            //Application.Run(new UpdatePublisher());
            //Application.Run(new Users());
            //Application.Run(new AddUser());
            //Application.Run(new DeleteUser());
            //Application.Run(new UpdateUser());
            //Application.Run(new IssueBooks());
        }
    }
}
