using System;
using System.Collections;
using System.Collections.Generic;

namespace Galleon.Checkout.Shared
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Authentication
    
    /// /authenticate example :
    /// {
    ///     AppID  : "test.app"
    ///     ID     : "test_ID"
    ///     Device : "test_device"
    /// }
    [Serializable]
    public class AuthenticateRequest
    {
        public string AppID  { get; set; }
        public string ID     { get; set; }
        public string Device { get; set; }
    }
    
    /// /authenticate Response example :
    /// {
    ///     "accessToken" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.........xdg3d7xMIVAhDm_9JpSl5MENnmWAaZDHRjApWUMj-t8",
    ///     "appId"       : "test.app",
    ///     "id"          : 1,
    ///     "externalId"  : "user id 1"
    /// }
    [Serializable]
    public class AuthenticateResponse
    {
        public string accessToken { get; set; }
        public string appID       { get; set; }
        public string id          { get; set; }
        public string externalId  { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Initialization

    [Serializable]
    public class InitializationRequest
    {
        public string access_token { get; set; }
    }
    
    public class InitializationResponse
    {
         public ConfigurationData configuration { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Config
    
    [Serializable]
    public class ConfigurationData
    {
        public Dictionary<string, string> values { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Payment Methods
    
    [Serializable]
    public class PaymentMethodsResponse
    {
        public PaymentMethodData[] payment_methods { get; set; }    
    }
    
    [Serializable]
    public class PaymentMethodData
    {
        public string          type                   { get; set; }
        public PaymentAction[] initialization_actions { get; set; }    
        public PaymentAction[] vaulting_actions       { get; set; }    
        public PaymentAction[] transaction_actions    { get; set; }    
    }
    
    [Serializable]
    public class CreditCardPaymentMethodData : PaymentMethodData
    {
        public string[] supported_card_types { get; set; }
    }
    [Serializable]
    public class GooglePayPaymentMethodData : PaymentMethodData
    {
        public string google_pay_token { get; set; }
    }
    [Serializable]
    public class PaypalPaymentMethodData : PaymentMethodData
    {
        public string paypal_token { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Payment Actions
    
    [Serializable]
    public class PaymentAction
    {
        public string action { get; set; }
    }
    
    [Serializable]
    public class GenericPaymentAction : PaymentAction
    {
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// vaulting
    
    [Serializable]
    public class AddCardRequest
    {
        public string credit_card_token { get; set; }
    }
    
    [Serializable]
    public class AddCardResponse
    {   
    }
    
    [Serializable]
    public class RemoveCardRequest
    {
        public string credit_card_token { get; set; }
    }
    
    [Serializable]
    public class RemoveCardResponse
    {
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Transactions
    
    [Serializable]
    public class StartTransactionRequest
    {
        public string selected_payment_method_type { get; set; }
        public string selected_payment_method_id   { get; set; }
    }
    
    [Serializable]
    public class StartTransactionResponse
    {
        public string transaction_id { get; set; }
    }
    
    [Serializable]
    public class TransactionResultData
    {
        public string   transaction_id { get; set; }
        public bool     isSuccess      { get; set; }
        public string[] errors         { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Recipt Validation
    
    /// /validate_receipt
    /// {
    ///     transaction_id : "12345"
    ///     recipt_data    : "12345"
    ///     date_time_utc  : "2025-01-01T12:34"
    /// }
    [Serializable]
    public class ValidateReceiptRequest
    {
        public string transaction_id { get; set; }
        public string recipt_data    { get; set; }
        public string date_time_utc  { get; set; }
    }
    
    /// /validate_receipt
    /// {
    ///     result : "valid"
    /// }
    [Serializable]
    public class ValidateReceiptResponse
    {
        public string result { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tax

    /// /tax request example :
    /// {
    ///     location : "usa"
    /// }
    [Serializable]
    public class TaxRequest
    {
        public string location { get; set; }
    }

    /// /tax response example :
    /// {
    ///     should_display_price_including_tax : false,
    ///     taxes                              :
    ///                                        {
    ///                                            "general tax" : 18,
    ///                                            "IRS"         : 2,         
    ///                                        }
    /// }
    [Serializable]
    public class TaxData
    {
        public bool                      should_display_price_including_tax { get; set; }
        public Dictionary<string, float> taxes                              { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Email
    
    [Serializable]
    public class UpdateEmailRequest
    {
        public string user_id { get; set; }
        public string email   { get; set; }
    }
    
    [Serializable]
    public class UpdateEmailResponse
    {
    }
}
