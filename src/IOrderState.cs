namespace DesignPatternChallenge
{
    // Interface que define os comportamentos esperados para cada estado do pedido.
    // Todos os estados concretos devem implementar esta interface.
    public interface IOrderState
    {
        void ProcessPayment(NewOrder order);
        void Ship(NewOrder order, string trackingCode);
        void Deliver(NewOrder order);
        void Cancel(NewOrder order);
        void RequestReturn(NewOrder order);
    }
}
