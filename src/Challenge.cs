// DESAFIO: Sistema de Pedido de E-commerce com Estados
// PROBLEMA: Um pedido passa por múltiplos estados (Pendente, Pago, Enviado, Entregue, Cancelado)
// e cada estado permite operações diferentes. O código atual usa condicionais gigantes que
// verificam o estado atual antes de cada operação, tornando difícil adicionar novos estados

using System;

namespace DesignPatternChallenge
{
    // Contexto: Sistema de e-commerce onde pedidos passam por ciclo de vida complexo
    // Cada estado tem regras específicas sobre quais operações são permitidas
    
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled,
        Returned
    }

    public class Order
    {
        public string OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string TrackingCode { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        public Order(string orderId, decimal totalAmount)
        {
            OrderId = orderId;
            TotalAmount = totalAmount;
            Status = OrderStatus.Pending;
        }

        // Problema: Cada método tem switch/if gigante verificando estado atual
        public void ProcessPayment()
        {
            Console.WriteLine($"\n[{OrderId}] Processando pagamento...");

            // Problema: Lógica condicional baseada em estado
            switch (Status)
            {
                case OrderStatus.Pending:
                    Status = OrderStatus.Paid;
                    Console.WriteLine($"✅ Pagamento confirmado! Total: R$ {TotalAmount:N2}");
                    Console.WriteLine($"   Status: {Status}");
                    break;

                case OrderStatus.Paid:
                    Console.WriteLine($"❌ Pedido já foi pago!");
                    break;

                case OrderStatus.Shipped:
                case OrderStatus.Delivered:
                    Console.WriteLine($"❌ Não é possível processar pagamento. Pedido já está {Status}");
                    break;

                case OrderStatus.Cancelled:
                    Console.WriteLine($"❌ Pedido foi cancelado. Crie novo pedido.");
                    break;

                default:
                    Console.WriteLine($"❌ Operação inválida para estado {Status}");
                    break;
            }
        }

        public void Ship(string trackingCode)
        {
            Console.WriteLine($"\n[{OrderId}] Tentando enviar pedido...");

            // Problema: Mesmo padrão de switch se repete
            switch (Status)
            {
                case OrderStatus.Pending:
                    Console.WriteLine($"❌ Pedido ainda não foi pago!");
                    break;

                case OrderStatus.Paid:
                    Status = OrderStatus.Shipped;
                    TrackingCode = trackingCode;
                    ShippedDate = DateTime.Now;
                    Console.WriteLine($"✅ Pedido enviado!");
                    Console.WriteLine($"   Código de rastreamento: {TrackingCode}");
                    Console.WriteLine($"   Status: {Status}");
                    break;

                case OrderStatus.Shipped:
                    Console.WriteLine($"❌ Pedido já foi enviado em {ShippedDate:dd/MM/yyyy}");
                    break;

                case OrderStatus.Delivered:
                    Console.WriteLine($"❌ Pedido já foi entregue!");
                    break;

                case OrderStatus.Cancelled:
                    Console.WriteLine($"❌ Não é possível enviar pedido cancelado");
                    break;

                default:
                    Console.WriteLine($"❌ Operação inválida para estado {Status}");
                    break;
            }
        }

        public void Deliver()
        {
            Console.WriteLine($"\n[{OrderId}] Registrando entrega...");

            switch (Status)
            {
                case OrderStatus.Pending:
                case OrderStatus.Paid:
                    Console.WriteLine($"❌ Pedido ainda não foi enviado!");
                    break;

                case OrderStatus.Shipped:
                    Status = OrderStatus.Delivered;
                    DeliveredDate = DateTime.Now;
                    Console.WriteLine($"✅ Pedido entregue com sucesso!");
                    Console.WriteLine($"   Data: {DeliveredDate:dd/MM/yyyy HH:mm}");
                    Console.WriteLine($"   Status: {Status}");
                    break;

                case OrderStatus.Delivered:
                    Console.WriteLine($"❌ Pedido já foi entregue em {DeliveredDate:dd/MM/yyyy}");
                    break;

                case OrderStatus.Cancelled:
                    Console.WriteLine($"❌ Pedido cancelado não pode ser entregue");
                    break;

                default:
                    Console.WriteLine($"❌ Operação inválida para estado {Status}");
                    break;
            }
        }

        public void Cancel()
        {
            Console.WriteLine($"\n[{OrderId}] Tentando cancelar pedido...");

            // Problema: Regras de cancelamento diferentes por estado
            switch (Status)
            {
                case OrderStatus.Pending:
                    Status = OrderStatus.Cancelled;
                    Console.WriteLine($"✅ Pedido cancelado. Nenhuma cobrança realizada.");
                    Console.WriteLine($"   Status: {Status}");
                    break;

                case OrderStatus.Paid:
                    Status = OrderStatus.Cancelled;
                    Console.WriteLine($"✅ Pedido cancelado. Reembolso será processado.");
                    Console.WriteLine($"   Valor: R$ {TotalAmount:N2}");
                    Console.WriteLine($"   Status: {Status}");
                    break;

                case OrderStatus.Shipped:
                    Console.WriteLine($"❌ Pedido já enviado. Use processo de devolução.");
                    break;

                case OrderStatus.Delivered:
                    Console.WriteLine($"❌ Pedido já entregue. Solicite devolução se necessário.");
                    break;

                case OrderStatus.Cancelled:
                    Console.WriteLine($"❌ Pedido já está cancelado!");
                    break;

                default:
                    Console.WriteLine($"❌ Operação inválida para estado {Status}");
                    break;
            }
        }

        public void RequestReturn()
        {
            Console.WriteLine($"\n[{OrderId}] Solicitando devolução...");

            switch (Status)
            {
                case OrderStatus.Delivered:
                    var daysSinceDelivery = (DateTime.Now - DeliveredDate.Value).Days;
                    if (daysSinceDelivery <= 7)
                    {
                        Status = OrderStatus.Returned;
                        Console.WriteLine($"✅ Devolução aprovada! Prazo dentro de 7 dias.");
                        Console.WriteLine($"   Reembolso: R$ {TotalAmount:N2}");
                        Console.WriteLine($"   Status: {Status}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Prazo de devolução expirado ({daysSinceDelivery} dias)");
                    }
                    break;

                case OrderStatus.Pending:
                case OrderStatus.Paid:
                    Console.WriteLine($"❌ Pedido ainda não foi entregue. Use cancelamento.");
                    break;

                case OrderStatus.Shipped:
                    Console.WriteLine($"❌ Aguarde a entrega para solicitar devolução.");
                    break;

                case OrderStatus.Cancelled:
                    Console.WriteLine($"❌ Pedido cancelado não pode ser devolvido.");
                    break;

                case OrderStatus.Returned:
                    Console.WriteLine($"❌ Devolução já processada!");
                    break;

                default:
                    Console.WriteLine($"❌ Operação inválida para estado {Status}");
                    break;
            }
        }

        // Problema: Adicionar novo estado = modificar TODOS os métodos acima
        // Problema: Adicionar nova operação = novo método com switch gigante
    }

    public class LegacyProgram
    {
        public static void ExecuteLegacy()
        {
            Console.WriteLine("=== Sistema de Gerenciamento de Pedidos ===");

            var order1 = new Order("ORD-001", 250.00m);
            Console.WriteLine($"\n=== Pedido {order1.OrderId} criado ===");
            Console.WriteLine($"Status inicial: {order1.Status}");

            // Fluxo normal
            order1.ProcessPayment();
            order1.Ship("BR123456789");
            order1.Deliver();
            order1.RequestReturn();

            Console.WriteLine("\n" + new string('=', 60));

            var order2 = new Order("ORD-002", 150.00m);
            Console.WriteLine($"\n=== Pedido {order2.OrderId} criado ===");

            // Tentativas inválidas
            order2.Ship("BR987654321");  // Não pago ainda
            order2.ProcessPayment();
            order2.ProcessPayment();     // Já pago
            order2.Cancel();             // Cancelar após pagamento

            Console.WriteLine("\n=== PROBLEMAS ===");
            Console.WriteLine("✗ Switch/case gigante repetido em cada método");
            Console.WriteLine("✗ Lógica de estado espalhada por toda a classe");
            Console.WriteLine("✗ Adicionar novo estado = modificar todos os métodos");
            Console.WriteLine("✗ Adicionar nova operação = novo método com switch completo");
            Console.WriteLine("✗ Difícil manter consistência das transições de estado");
            Console.WriteLine("✗ Código difícil de testar (muitos caminhos possíveis)");
            Console.WriteLine("✗ Viola Open/Closed Principle");

            Console.WriteLine("\n=== Complexidade ===");
            Console.WriteLine("• 6 estados × 5 operações = 30 casos para gerenciar");
            Console.WriteLine("• Adicionar 1 estado = modificar 5 métodos");
            Console.WriteLine("• Adicionar 1 operação = criar switch com 6 cases");
            Console.WriteLine("• Difícil visualizar máquina de estados");
            Console.WriteLine("• Transições implícitas (não há diagrama claro)");

            // Perguntas para reflexão:
            // - Como encapsular comportamento específico de cada estado?
            // - Como facilitar adição de novos estados?
            // - Como tornar transições de estado explícitas?
            // - Como eliminar condicionais baseadas em estado?
        }
    }
}
