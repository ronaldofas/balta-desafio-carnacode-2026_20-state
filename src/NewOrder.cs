using System;

namespace DesignPatternChallenge
{
    // Contexto: A classe principal que mantém uma referência para a interface de estado.
    // Ela delega as requisições para o objeto de estado atual.
    public class NewOrder
    {
        public string OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string TrackingCode { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        // Mantém a referência para o estado atual
        private IOrderState _currentState;

        public NewOrder(string orderId, decimal totalAmount)
        {
            OrderId = orderId;
            TotalAmount = totalAmount;
            
            // O estado inicial do pedido é Pendente
            // Nós repassaremos a dependência de PendingState depois de criá-lo.
            // Por enquanto, faremos o setup inicial no próprio Program.cs
        }
        
        // Método para permitir a transição de estado promovida pelos próprios estados
        public void TransitionTo(IOrderState state)
        {
            Console.WriteLine($"[Sistema] Transição de estado: Mudando para {state.GetType().Name}");
            _currentState = state;
        }

        // Operações delegadas ao estado atual
        public void ProcessPayment()
        {
            Console.WriteLine($"\n[{OrderId}] Processando pagamento...");
            _currentState.ProcessPayment(this);
        }

        public void Ship(string trackingCode)
        {
            Console.WriteLine($"\n[{OrderId}] Tentando enviar pedido...");
            _currentState.Ship(this, trackingCode);
        }

        public void Deliver()
        {
            Console.WriteLine($"\n[{OrderId}] Registrando entrega...");
            _currentState.Deliver(this);
        }

        public void Cancel()
        {
            Console.WriteLine($"\n[{OrderId}] Tentando cancelar pedido...");
            _currentState.Cancel(this);
        }

        public void RequestReturn()
        {
            Console.WriteLine($"\n[{OrderId}] Solicitando devolução...");
            _currentState.RequestReturn(this);
        }
    }
}
