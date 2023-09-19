namespace DesignPatterns
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; } // Виртуалните пропертита могат да се оверрайдват

        public Rectangle()
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        //public new int Width
        //{
        //    set { base.Width = base.Height = value; }
        //}
        // Ако са с new, няма да работи правилно

        public override int Width 
        {  
            set { base.Width = base.Height = value; }
        }

        //public new int Height
        //{
        //    set { base.Width = base.Height = value; }
        //}

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    public class Demo
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}