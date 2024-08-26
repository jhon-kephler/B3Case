namespace B3Case.Application.Services.RabbitServices.Interface
{
    public interface IBusService
    {
        void SendMessage(string message, string queue);
        T Consuming<T>(string queue);
    }
}