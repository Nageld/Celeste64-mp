﻿using System;
using System.Net.Http.Headers;

namespace Celeste64;

public class Message
{
    private string position;
    private String userID;
    private string extra;
    private Vec3 posVec;
    private string currentMap;
    private Int64 packetNum;

    public String Position
    {
        get => position;
        set => position = value;
    }
    
    public Vec3 PosVec
    {
        get => posVec;
        set => posVec = value;
    }
    
    public String CurrentMap
    {
        get => currentMap;
        set => currentMap = value;
    }

    public String UserID
    {
        get => userID;
        set => userID = value;
    }
    
    public Int64 PacketNum
    {
        get => packetNum;
        set => packetNum = value;
    }


    public string Extra
    {
        get => extra;
        set => extra = value;
    }


    public Message( String userID, String position, String currentMap, Int64 packet)
    {
        this.userID = userID;
        this.position = position;
        this.currentMap = currentMap;
        this.packetNum = packet;
    }


    public override string ToString()
    {
        return $@"{{ ""userID"": ""{userID}"",""position"": ""{position}"",""currentMap"": ""{currentMap}"",""packetNum"": ""{packetNum}"" }}";
    }

    public bool Equals(Message message)
    {
        return message.ToString() == this.ToString();
    }

    public void PositionVec()
    {
        var s = position.Substring(1, position.Length - 2);
        string[] parts = s.Split(new string[] { "," }, StringSplitOptions.None);
        var temp = new Vector3(
            float.Parse(parts[0]),
            float.Parse(parts[1]),
            float.Parse(parts[2]));
        this.PosVec = temp;
    }
}