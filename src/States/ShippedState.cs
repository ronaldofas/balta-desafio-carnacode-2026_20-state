using System;

namespace DesignPatternChallenge
{
    public class ShippedState : IOrderState
    {
        public void ProcessPayment(NewOrder order)
        {
            Console.WriteLine($"❌ Não é possível processar pagamento. Pedido já está Shipped");
        }

        public void Ship(NewOrder order, string trackingCode)
        {
            Console.WriteLine($"❌ Pedido já foi enviado em {order.ShippedDate:dd/MM/yyyy}");
        }

        public void Deliver(NewOrder order)
        {
            order.DeliveredDate = DateTime.Now;
            order.TransitionTo(new DeliveredState());
            Console.WriteLine($"✅ Pedido entregue com sucesso!");
            Console.WriteLine($"   Data: {order.DeliveredDate:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"   Status: Delivered");
        }

        public void Cancel(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido já enviado. Use processo de devolução.");
        }

        public void RequestReturn(NewOrder order)
        {
            Console.WriteLine($"❌ Aguarde a entrega para solicitar devolução.");
        }
    }
}
