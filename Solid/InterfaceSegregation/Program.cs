namespace DesignPatterns
{
    public class Document
    {

    }

    public interface IPrinter
    {
        void Print(Document document);
    }

    public interface IScanner
    {
        void Scan(Document document);
    }

    public class PhotoCopier : IScanner, IPrinter
    {
        public void Scan(Document document)
        {
            //
        }

        public void Print(Document document)
        {
            //
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {

        }
    }
}