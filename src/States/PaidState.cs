using System;

namespace DesignPatternChallenge
{
    public class PaidState : IOrderState
    {
        public void ProcessPayment(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido já foi pago!");
        }

        public void Ship(NewOrder order, string trackingCode)
        {
            order.TrackingCode = trackingCode;
            order.ShippedDate = DateTime.Now;
            order.TransitionTo(new ShippedState());
            Console.WriteLine($"✅ Pedido enviado!");
            Console.WriteLine($"   Código de rastreamento: {order.TrackingCode}");
            Console.WriteLine($"   Status: Shipped");
        }

        public void Deliver(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido ainda não foi enviado!");
        }

        public void Cancel(NewOrder order)
        {
            order.TransitionTo(new CancelledState());
            Console.WriteLine($"✅ Pedido cancelado. Reembolso será processado.");
            Console.WriteLine($"   Valor: R$ {order.TotalAmount:N2}");
            Console.WriteLine($"   Status: Cancelled");
        }

        public void RequestReturn(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido ainda não foi entregue. Use cancelamento.");
        }
    }
}
