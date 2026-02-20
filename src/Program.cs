using System;

namespace DesignPatternChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("     SISTEMA ANTIGO (SEM DESIGN PATTERN)      ");
            Console.WriteLine("==============================================\n");
            
            // Executando fluxo legado para demonstração na mesma compilação
            LegacyProgram.ExecuteLegacy();

            Console.WriteLine("\n\n\n==============================================");
            Console.WriteLine("          NOVO SISTEMA (STATE PATTERN)        ");
            Console.WriteLine("==============================================\n");

            var orderState = new NewOrder("NOVO-001", 300.00m);
            // Definir o estado inicial como pendente (contexto inicializado dependendo do pattern)
            orderState.TransitionTo(new PendingState());
            
            Console.WriteLine($"\n=== Pedido {orderState.OrderId} criado ===");

            // Fluxo normal - Sucesso
            orderState.ProcessPayment(); // Muda para Paid
            orderState.Ship("BR-NEW-778899"); // Muda para Shipped
            orderState.Deliver(); // Muda para Delivered
            orderState.RequestReturn(); // Muda para Returned

            Console.WriteLine("\n" + new string('=', 60));

            var orderState2 = new NewOrder("NOVO-002", 500.00m);
            orderState2.TransitionTo(new PendingState());
            Console.WriteLine($"\n=== Pedido {orderState2.OrderId} criado ===");

            // Fluxo com cancelamento e operações inválidas
            orderState2.Ship("XYZ-123"); // Não permite envio de pedido pendente sem pagamento
            orderState2.ProcessPayment(); // Muda para Paid
            orderState2.Cancel(); // Muda para Cancelado
            orderState2.RequestReturn(); // Pedido cancelado, então não pode ser devolvido

            Console.WriteLine("\n=== IMPACTO DA REFATORAÇÃO ===");
            Console.WriteLine("✅ Cada estado encapsula suas próprias lógicas, sem switch cases gigantes");
            Console.WriteLine("✅ Adicionar um novo status requer apenas criar uma nova classe `IOrderState`");
            Console.WriteLine("✅ A classe Order (contexto) obedece o Single Responsibility Principle (SRP)");
            Console.WriteLine("✅ Open/Closed Principle (OCP) garantido na inclusão de regras complexas futuras\n");
        }
    }
}
