using System;

namespace DesignPatternChallenge
{
    public class PendingState : IOrderState
    {
        public void ProcessPayment(NewOrder order)
        {
            // Transição para PaidState
            order.TransitionTo(new PaidState());
            Console.WriteLine($"✅ Pagamento confirmado! Total: R$ {order.TotalAmount:N2}");
            // Para mantermos a coesão com a saída visual original: 
            Console.WriteLine($"   Status: Paid");
        }

        public void Ship(NewOrder order, string trackingCode)
        {
            Console.WriteLine($"❌ Pedido ainda não foi pago!");
        }

        public void Deliver(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido ainda não foi enviado!");
        }

        public void Cancel(NewOrder order)
        {
            order.TransitionTo(new CancelledState());
            Console.WriteLine($"✅ Pedido cancelado. Nenhuma cobrança realizada.");
            Console.WriteLine($"   Status: Cancelled");
        }

        public void RequestReturn(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido ainda não foi entregue. Use cancelamento.");
        }
    }
}
