using System;

namespace DesignPatternChallenge
{
    public class DeliveredState : IOrderState
    {
        public void ProcessPayment(NewOrder order)
        {
            Console.WriteLine($"❌ Não é possível processar pagamento. Pedido já está Delivered");
        }

        public void Ship(NewOrder order, string trackingCode)
        {
            Console.WriteLine($"❌ Pedido já foi entregue!");
        }

        public void Deliver(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido já foi entregue em {order.DeliveredDate:dd/MM/yyyy}");
        }

        public void Cancel(NewOrder order)
        {
            Console.WriteLine($"❌ Pedido já entregue. Solicite devolução se necessário.");
        }

        public void RequestReturn(NewOrder order)
        {
            var daysSinceDelivery = (DateTime.Now - order.DeliveredDate.Value).Days;
            
            if (daysSinceDelivery <= 7)
            {
                order.TransitionTo(new ReturnedState());
                Console.WriteLine($"✅ Devolução aprovada! Prazo dentro de 7 dias.");
                Console.WriteLine($"   Reembolso: R$ {order.TotalAmount:N2}");
                Console.WriteLine($"   Status: Returned");
            }
            else
            {
                Console.WriteLine($"❌ Prazo de devolução expirado ({daysSinceDelivery} dias)");
            }
        }
    }
}
