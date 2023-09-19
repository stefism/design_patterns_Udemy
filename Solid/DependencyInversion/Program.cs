using System.Security.Cryptography.X509Certificates;

namespace DesignPatterns
{
    public enum Relationship
    {
        Parent, Child, Sibling
    }

    public class Person
    {
        public string Name;
    }

    public interface IRelationshipBrouser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // low-level
    public class Relationships : IRelationshipBrouser
    {
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)> ();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            foreach (var relation in relations.Where(r => r.Item1.Name == name && r.Item2 == Relationship.Parent))
            {
                //Console.WriteLine($"John has a child called {relation.Item3.Name}");
                yield return relation.Item3;
            }
        }

        //public List<(Person, Relationship, Person)> Relations => relations;
    }

    public class Research
    {
        //private Relationships relationships;
        //public Research(Relationships relationships)
        //{
        //    this.relationships = relationships;
        //}

        public Research(IRelationshipBrouser brouser)
        {
            foreach(var person in brouser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child called {person.Name}");
            }
        }

        public void ShowRelations()
        {
            //var relations = relationships.Relations;
            //foreach (var relation in relations.Where(r => r.Item1.Name == "John" && r.Item2 == Relationship.Parent))
            //{
            //    Console.WriteLine($"John has a child called {relation.Item3.Name}");
            //}
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            //var research = new Research(relationships);
            //research.ShowRelations();

            new Research(relationships);
        }
    }
}