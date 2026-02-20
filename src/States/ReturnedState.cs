using System;

namespace DesignPatternChallenge
{
    public class ReturnedState : IOrderState
    {
        public void ProcessPayment(NewOrder order)
        {
            Console.WriteLine($"❌ Operação inválida para estado Returned");
        }

        public void Ship(NewOrder order, string trackingCode)
        {
            Console.WriteLine($"❌ Operação inválida para estado Returned");
        }

        public void Deliver(NewOrder order)
        {
            Console.WriteLine($"❌ Operação inválida para estado Returned");
        }

        public void Cancel(NewOrder order)
        {
            Console.WriteLine($"❌ Operação inválida para estado Returned");
        }

        public void RequestReturn(NewOrder order)
        {
            Console.WriteLine($"❌ Devolução já processada!");
        }
    }
}
