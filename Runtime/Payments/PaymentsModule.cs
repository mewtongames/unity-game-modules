using System;
using System.Collections.Generic;
using MewtonGames.Payments.Providers;

namespace MewtonGames.Payments
{
    public class PaymentsModule : IPaymentsProvider
    {
        public bool isInitialized => _provider.isInitialized;
        public bool isPaymentsSupported => _provider.isPaymentsSupported;
        
        private readonly IPaymentsProvider _provider;

        public PaymentsModule(IPaymentsProvider provider)
        {
            _provider = provider;
        }

        public void Initialize(List<Product> settings, Action onComplete = null)
        {
            _provider.Initialize(settings, onComplete);
        }
        
        public void GetCatalog(Action<List<Product>> onComplete)
        {
            _provider.GetCatalog(onComplete);
        }

        public void GetPurchases(Action<List<Product>> onComplete)
        {
            _provider.GetPurchases(onComplete);
        }

        public void Purchase(string id, Action<bool> onComplete)
        {
            _provider.Purchase(id, onComplete);
        }

        public void Consume(string id, Action<bool> onComplete)
        {
            _provider.Consume(id, onComplete);
        }
    }
}