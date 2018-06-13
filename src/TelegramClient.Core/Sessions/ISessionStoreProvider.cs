﻿namespace TelegramClient.Core.Sessions
{
    using System;
    using System.Threading.Tasks;

    public interface ISessionStoreProvider : IDisposable
    {
        Task<byte[]> LoadSession();

        Task RemoveSession();

        Task SaveSession(byte[] session);
    }
}