﻿namespace Spreedly.Net.Client
{
    using System;
    using RestSharp;

    public class SpreedlyResponse
    {
        public SpreedlyStatus Status { get; set; }
        public Exception Error { get; set; }
    }

    public class SpreedlyResponse<TEntity>: SpreedlyResponse
    {
        public TEntity Entity { get; set; }
    }
}