namespace SampleSignalR.Contracts
{
    //public class BroadcastMessage
    //{
    //    public string Name { get; set; } = default!;
    //    public string Message { get; set; } = default!;
    //}
    public record BroadcastMessage(string Name, string Message);
}