using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.ProgramLogic
{
    public class OutputTitle
    {
        public static string ReadTitle()
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Info");
            string path = Path.Combine(folder, "title.txt");

            string text = File.ReadAllText(path);
            return text;
        }
    }
}
