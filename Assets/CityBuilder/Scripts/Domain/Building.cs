namespace CityBuilder.Scripts.Domain
{
    public class Building
    {
        public int Id { get; private set; }

        public int Price { get; private set; }

        public string Name { get; private set; }

        public Building(int id, int price, string name)
        {
            Id = id;
            Price = price;
            Name = name;
        }
    }
}