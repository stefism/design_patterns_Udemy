namespace DesignPattern
{
    public class Person
    {
        public string Name, Position;
    }

    public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public PersonBuilder Called(string name) => Do(person => person.Name = name);

        public PersonBuilder Do(Action<Person> action) => AddAction(action);

        public Person Build() => actions.Aggregate(new Person(), (person, function) => function(person));

        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(person =>
            {
                action(person);
                return person;
            });

            return this;
        } 
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
            => builder.Do(person => person.Position = position);
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var person = new PersonBuilder().Called("Sarah").WorksAs("Developer").Build();
            ;
        }
    }
}