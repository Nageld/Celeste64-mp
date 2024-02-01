using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Celeste64;

public static class ClientHandler 
{
    public static UdpClient udpClient;

    public static void ClientStart(String ip, int port, ref Stack changes)
    {
        Console.WriteLine($"Connecting to {ip} on port {port}");

        udpClient = new UdpClient();
        try
        {
            Stack changed = changes;

            udpClient.Connect(ip, port);
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Thread thread = new Thread(() => AwaitMessages(ref udpClient, remoteIpEndPoint, ref changed ));
            thread.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.WriteLine($"FAILED to connect to {ip} on port {port}");
        }
    }


    private static void AwaitMessages(ref UdpClient udpClient, IPEndPoint remoteIpEndPoint, ref Stack changes)
    {
        Console.WriteLine($"AWAITING");

        while (true)
        {

            Byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);

            string returnData = Encoding.ASCII.GetString(receiveBytes);
            if (returnData.StartsWith("b'") && returnData.EndsWith("'"))
            {
                returnData = returnData.Substring(2, returnData.Length - 3);
            }
            try
            {
                Console.WriteLine(returnData);
                var result = JsonConvert.DeserializeObject<Message>(returnData);
                result.PositionVec();
                changes.Push(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public static void SendToServer(Message message)
    {

        udpClient.Send(Encoding.ASCII.GetBytes(message.ToString()), Encoding.ASCII.GetBytes(message.ToString()).Length);
    }

}