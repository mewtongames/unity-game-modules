using System;
using System.Collections.Generic;

namespace MewtonGames.Payments.Providers
{
    public class MockPaymentsProvider : IPaymentsProvider
    {
        public bool isPaymentsSupported => true;
        public bool isInitialized { get; private set; }

        private List<Product> _products;
        
        public void Initialize(List<Product> products, Action onComplete = null)
        {
            _products = products;
            isInitialized = true;
        }

        public void GetCatalog(Action<List<Product>> onComplete)
        {
            onComplete?.Invoke(_products);
        }

        public void GetPurchases(Action<List<Product>> onComplete)
        {
            onComplete?.Invoke(new List<Product>());
        }

        public void Purchase(string id, Action<bool> onComplete)
        {
            onComplete?.Invoke(true);
        }

        public void Consume(string id, Action<bool> onComplete)
        {
            onComplete?.Invoke(true);
        }
    }
}