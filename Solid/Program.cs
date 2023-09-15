using System.Diagnostics;

namespace DesignPatterns
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; //memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, journal.ToString());
            }
        }
    }
    public class Demo
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("Cool name 1");
            journal.AddEntry("Name cool 2");
            Console.WriteLine(journal);

            var persitence = new Persistence();
            var filename = @"C:\PROJECTS\design_patterns_Udemy\Solid\journal.txt";

            persitence.SaveToFile(journal, filename, true);
            //Process.Start(filename); //not work for me.
        }
    }
}