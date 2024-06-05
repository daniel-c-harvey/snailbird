namespace snailbird_admin_scratch
{

    internal class Base<TSelf> where TSelf : Base<TSelf>
    {
        public TSelf GetMe()
        {
            return (TSelf)this;
        }
    }

    internal class Derived : Base<Derived>
    {

    }

    internal class Client<T> where T : Base<T>
    {
        public T? X { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Client<Derived> client = new();
            client.X = new Derived();
            client.X = client.X.GetMe();
        }
    }
}
