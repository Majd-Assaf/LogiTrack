namespace Logistics.Messaging.Configuration;

public static class ExchangeNames
{
    public static string Commands(string env) => $"{env}.logistics.commands";
    public static string Events(string env) => $"{env}.logistics.events";
}
