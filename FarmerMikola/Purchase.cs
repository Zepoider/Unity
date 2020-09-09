using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchase : MonoBehaviour, IStoreListener
{

    private IStoreController controller;
    private IExtensionProvider extensions;

    public static string noAds = "noads";
    public static Purchase purchase;

    private void Start()
    {
        purchase = this;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(noAds, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProductID(string productId)
    {
        Product product = controller.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            controller.InitiatePurchase(product);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {

        //PlayerPrefs.SetInt("ads", 1);
        //GetComponent<ChangeScene>().options.GetComponent<Options>().ads.interactable = false;

        return PurchaseProcessingResult.Complete;
    }
}
