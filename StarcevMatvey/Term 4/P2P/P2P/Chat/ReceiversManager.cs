﻿using P2P.Net;
using P2P.MessengeTypes;
using P2P.Loggers;

namespace P2P.Chat
{
    public class ReceiversManager : IDisposable
    {
        private volatile bool _stop = false;
        private readonly object _lock;

        private readonly MessengeEncoder.MessengeEncoder _encoder;

        private List<Thread> _receiversThreads = new List<Thread> ();
        private List<Connect> _toClose = new List<Connect> (); 

        private bool _disposed = false;

        public Invokes Invoke
        {
            get => this.Invoke;
            set
            {
                if (_invokeUpdt)
                {
                    this.Invoke = value;
                    _invokeUpdt = false;
                }
            }
        }
        private bool _invokeUpdt;

        public ILogger Logger { get; }

        public ReceiversManager(object l, MessengeEncoder.MessengeEncoder encoder, ILogger logger)
        {
            _lock = l;
            _encoder = encoder;
            Logger = logger;
        }

        public ReceiversManager(object l, MessengeEncoder.MessengeEncoder encoder)
        {
            _lock = l;
            _encoder = encoder;
            Logger = new Logger();
        }

        public void Add(Connect con, ConnectionsManager manager)
        {
            _toClose.Add(con);

            var th = new Thread(() => Receive(con, manager));
            _receiversThreads.Add(th);
            th.Start();
        }

        private void Receive(Connect con, ConnectionsManager manager)
        {
            while (!_stop)
            {
                var mes = con.Receive();

                if (mes.Type == MessengeTypes.TypeOfData.Empty) continue;

                lock (_lock)
                {
                    WorkWithData(mes, manager);
                }
            }
        }

        private void WorkWithData(Messenge mes, ConnectionsManager manager)
        {
            switch (mes.Type)
            {
                case TypeOfData.Listeners:
                    manager.Merge(_encoder.GetConnections(mes));
                    break;
                case TypeOfData.RegularMessenge:
                    Console.WriteLine(mes.Data);
                    if (!_invokeUpdt) Invoke.Invoke(mes.Data);
                    break;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _stop = true;
            
            _toClose.ForEach(x => x.Dispose());
            _receiversThreads.ForEach(x => x.Join());

            _disposed = true;

            Logger.Log($"I disposed receivers manager");
        }
    }
}
