#r "Newtonsoft.Json"

using System;
using System.Net;
using Newtonsoft.Json;

public class Order
{
    public string OrderId {get;set;}
    public string ProductId {get;set;}
    public string Email {get;set;}
    public string Price {get;set;}
}

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log, IAsyncCollector<Order> outputQueueItem)
{
    log.Info($"Order received!");
    string jsonContent = await req.Content.ReadAsStringAsync();
    var order = JsonConvert.DeserializeObject<Order>(jsonContent);
    if(order.ProductId == null){
        return req.CreateResponse(HttpStatusCode.PreconditionFailed, new {
            message = $"Invalid order!"
        });
    } else {
        log.Info($"Order {order.OrderId} received from {order.Email} for product {order.ProductId}");
        await outputQueueItem.AddAsync(order);
        return req.CreateResponse(HttpStatusCode.OK, new {
            message = $"Thank you for your order!"
        });
    }
    

}
