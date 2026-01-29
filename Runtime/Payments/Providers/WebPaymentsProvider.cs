#if UNITY_WEBGL
using System;
using System.Collections.Generic;
using Playgama;

namespace MewtonGames.Payments.Providers
{
    public class WebPaymentsProvider : IPaymentsProvider
    {
        public bool isPaymentsSupported => Bridge.payments.isSupported;
        public bool isInitialized { get; private set; }
        
        public void Initialize(List<Product> settings, Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }

        public void GetCatalog(Action<List<Product>> onComplete)
        {
            if (!isPaymentsSupported) {
                onComplete?.Invoke(new List<Product>());
                return;
            }

            Bridge.payments.GetCatalog((success, catalog) =>
            {
                var products = new List<Product>();
                if (!success)
                {
                    onComplete?.Invoke(products);
                    return;
                }

                foreach (var productData in catalog)
                {
                    var product = new Product(productData["id"], productData["price"]);
                    products.Add(product);
                }
                
                onComplete?.Invoke(products);
            });
        }

        public void GetPurchases(Action<List<Product>> onComplete)
        {
            if (!isPaymentsSupported) {
                onComplete?.Invoke(new List<Product>());
                return;
            }

            Bridge.payments.GetPurchases((success, purchases) =>
            {
                var products = new List<Product>();
                if (!success)
                {
                    onComplete?.Invoke(products);
                    return;
                }

                foreach (var productData in purchases)
                {
                    var product = new Product(productData["id"], string.Empty);
                    products.Add(product);
                }
                
                onComplete?.Invoke(products);
            });
        }

        public void Purchase(string id, Action<bool> onComplete)
        {
            if (!isPaymentsSupported) {
                onComplete?.Invoke(false);
                return;
            }

            Bridge.payments.Purchase(id, (success, purchase) =>
            {
                onComplete?.Invoke(success);
            });
        }

        public void Consume(string id, Action<bool> onComplete)
        {
            if (!isPaymentsSupported) {
                onComplete?.Invoke(false);
                return;
            }

            Bridge.payments.ConsumePurchase(id, (success, purchase) =>
            {
                onComplete?.Invoke(success);
            });
        }
    }
}
#endif