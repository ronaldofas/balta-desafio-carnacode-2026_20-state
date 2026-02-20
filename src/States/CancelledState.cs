using System;

namespace DesignPatternChallenge
{
    public class CancelledState : IOrderState
    {
        public void ProcessPayment(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido foi cancelado. Crie novo pedido.");
        }

        public void Ship(NewOrder order, string trackingCode)
        {
            Console.WriteLine($"❌ Não é possível enviar pedido cancelado");
        }

        public void Deliver(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido cancelado não pode ser entregue");
        }

        public void Cancel(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido já está cancelado!");
        }

        public void RequestReturn(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido cancelado não pode ser devolvido.");
        }
    }
}
