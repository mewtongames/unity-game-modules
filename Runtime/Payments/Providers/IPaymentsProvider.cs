using System;
using System.Collections.Generic;
using MewtonGames.Common;

namespace MewtonGames.Payments.Providers
{
    public interface IPaymentsProvider : IInitializable<List<Product>>
    {
        bool isPaymentsSupported { get; }
        
        void GetCatalog(Action<List<Product>> onComplete);
        void GetPurchases(Action<List<Product>> onComplete);

        void Purchase(string id, Action<bool> onComplete);
        void Consume(string id, Action<bool> onComplete);
    }
}