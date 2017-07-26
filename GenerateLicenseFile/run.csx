using System;

public class Order
{
    public string OrderId {get;set;}
    public string ProductId {get;set;}
    public string Email {get;set;}
    public string Price {get;set;}
}

public static void Run(Order myQueueItem, TraceWriter log, TextWriter outputBlob)
{
    log.Info($"Received and order: Order {myQueueItem.OrderId}, Product {myQueueItem.ProductId}, Email {myQueueItem.Email}, Price {myQueueItem.Price}");
    outputBlob.WriteLine($"OrderId: {myQueueItem.OrderId}");
    outputBlob.WriteLine($"Email: {myQueueItem.Email}");
    outputBlob.WriteLine($"ProductId: {myQueueItem.ProductId}");
    outputBlob.WriteLine($"PurchaseDate: {DateTime.UtcNow}");

    var md5 = System.Security.Cryptography.MD5.Create();
    var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(myQueueItem.Email + "secret"));
    outputBlob.WriteLine($"SecretCode: {BitConverter.ToString(hash).Replace("-","")}");
}
