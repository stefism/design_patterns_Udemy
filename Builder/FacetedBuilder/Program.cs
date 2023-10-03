namespace DesignPatterns
{
    public class Person
    {
        // address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{StreetAddress}, {Postcode}, {City}";
        }
    }

    public class PersonBuilder // facade
    {
        // reference
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
    }

    public class PersonAddressBuilder : PersonBuilder
    { 

    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }


    public class Demo
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Works.At("Money").AsA("Engineer").Earning(123000);
        }
    }
}