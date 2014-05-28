using System;
using Ninject;
using Ninject.Modules;

namespace NinjectIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new MyModule());

            var shopper = kernel.Get<Shopper>();

            shopper.Charge();

            Console.Read();
        }
    }

    public class MyModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ICreditCard>().To<MasterCard>();
        }
    }

    public class Visa : ICreditCard
    {
        public string Charge()
        {
            return "Charging with the Visa!";
        }
    }

    public class MasterCard : ICreditCard
    {
        public string Charge()
        {
            return "Swiping the MasterCard!";
        }
    }

    public class Shopper
    {
        private readonly ICreditCard creditCard;

        public Shopper(ICreditCard creditCard)
        {
            this.creditCard = creditCard;
        }

        public void Charge()
        {
            var chargeMessage = creditCard.Charge();
            Console.WriteLine(chargeMessage);
        }
    }

    public interface ICreditCard
    {
        string Charge();
    }
}
