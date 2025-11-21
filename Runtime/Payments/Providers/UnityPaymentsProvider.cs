#if (UNITY_ANDROID || UNITY_IOS) && UNITY_PAYMENTS
using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace MewtonGames.Payments.Providers
{
    public class UnityPaymentsProvider : IPaymentsProvider, IDetailedStoreListener
    {
        public bool isPaymentsSupported => true;
        public bool isInitialized { get; private set; }
        
        private List<Product> _products;
        private IStoreController _storeController;
        
        private Action _initializeCallback;
        private Action<bool> _purchaseCallback;
        
        
        public void Initialize(List<Product> products, Action onComplete = null)
        {
            _products = products;
            _initializeCallback = onComplete;
            
            var module = StandardPurchasingModule.Instance();
            var builder = ConfigurationBuilder.Instance(module);

            foreach (var product in _products)
            {
                builder.AddProduct(
                    product.id, 
                    ProductType.Consumable, 
                    new IDs()
                    {
                        { product.id, GooglePlay.Name },
                        { product.id, AppleAppStore.Name },
                    }
                );
            }

            UnityPurchasing.Initialize(this, builder);
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
            if (_storeController != null)
            {
                _purchaseCallback = onComplete;
                _storeController.InitiatePurchase(id);
                return;
            }
            
            onComplete?.Invoke(false);
        }

        public void Consume(string id, Action<bool> onComplete)
        {
            onComplete?.Invoke(true);
        }

        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            isInitialized = true;
            _storeController = controller;
            _initializeCallback?.Invoke();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            isInitialized = true;
            _initializeCallback?.Invoke();
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            isInitialized = true;
            _initializeCallback?.Invoke();
        }
        
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            _purchaseCallback?.Invoke(true);
            _purchaseCallback = null;
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureDescription failureDescription)
        {
            _purchaseCallback?.Invoke(false);
            _purchaseCallback = null;
        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
        {
            _purchaseCallback?.Invoke(false);
            _purchaseCallback = null;
        }
    }
}
#endif