﻿using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using NowMine.Helpers;
using NowMine.Models;
using NowMineCommon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NowMine
{
    class ServerUDP
    {
        private UdpClient UDPClient;
        private IPEndPoint BradcastUDPIP = new IPEndPoint(IPAddress.Broadcast, 1234);

        public delegate void NewUserEventHandler(object s, GenericEventArgs<IPAddress> e);
        public event NewUserEventHandler NewUser;
        protected virtual void OnNewUser(IPAddress ip)
        {
            NewUser?.Invoke(this, new GenericEventArgs<IPAddress>(ip));
        }

        public void udpListener()
        {
            int PORT_NUMBER = 1234;

            Console.WriteLine("UDP/ Starting UDP Listener");
            if (UDPClient == null)
                UDPClient = new UdpClient(PORT_NUMBER);
            IAsyncResult ar_ = UDPClient.BeginReceive(Receive, new object());
        }

        private void Receive(IAsyncResult ar)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 1234);
            byte[] bytes = UDPClient.EndReceive(ar, ref ip);
            string message = Encoding.ASCII.GetString(bytes);
            Regex.Replace(message, @"[^\u0020-\u007E]", string.Empty);
            Console.WriteLine("UDP/ From {0} received: {1} ", ip.Address.ToString(), message);
            if (message.Equals("NowMine!"))
            {
                Console.WriteLine("UDP/ Firing Event OnNewUser");
                OnNewUser(ip.Address);
            }
            udpListener();
        }

        public void UDPSend(string message)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, 1234);
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            UDPClient.Send(bytes, bytes.Length, ip);
            Console.WriteLine("UDP Sending string: {0} ", message);
        }

        public void UDPSend(byte[] message)
        {
            UDPClient.Send(message, message.Length, BradcastUDPIP);
            Console.WriteLine("UDP Sent: {0} ", Convert.ToBase64String(message));
        }


        public void SendQueuedPiece(ClipQueued clip, uint eventID)
        {
            var message = BytesMessegeBuilder.GetQueuePieceBytes(clip, eventID);
            UDPSend(message);
        }

        public void sendData(byte[] message)
        {
            UDPSend(message);
        }

        internal void playedNow(int qPos, uint eventID)
        {
            var message = BytesMessegeBuilder.GetPlayedNowBytes(qPos, eventID);
            UDPSend(message);
        }

        internal void SendShutdown()
        {
            //var message = BytesMessegeBuilder.GetShutdownBytes();
            //UDPSend(message);
        }
    }
}