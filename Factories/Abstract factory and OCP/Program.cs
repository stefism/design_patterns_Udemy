namespace DesignPatterns
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Tea tea tea");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Coffee coffee coffee");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Prepare {amount} Tea");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Prepare {amount} Coffee");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>> ();
        public HotDrinkMachine()
        {
            foreach(var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if(typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    var typeName = t.Name.Replace("Factory", string.Empty);
                    var factoryName = (IHotDrinkFactory)Activator.CreateInstance(t);
                    var tuple = Tuple.Create(typeName, factoryName);

                    factories.Add(tuple);
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks:");

            for (int i = 0; i < factories.Count; i++)
            {
                var tuple = factories[i];
                Console.WriteLine($"{i}: {tuple.Item1}");
            }
        }
    }

    public class Demo
    {
        public static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();
        }
    }
}