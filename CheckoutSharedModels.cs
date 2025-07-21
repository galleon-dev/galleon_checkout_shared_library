using System;
using System.Collections;
using System.Collections.Generic;


/// The file "CheckoutSharedModules.cs" is shared between the server project (apt.net project) and the client (unity project)
/// it defines the namespace "Galleon.Checkout.Shared"
/// in it are many public serializable data classes that act the the contracts for requests and responses between server and client.
/// All contracts for this API must sit in this single file.
/// the syntax must be c#8 syntax, compaatible with unity 2021.3.
/// so for example - no "required" modifier.
/// 
/// example of a request and resposnse :
/// /// /authenticate example :
///    
///    [Serializable]
///    public class AuthenticateRequest
///    {
///        public string AppID  { get; set; }
///        public string ID     { get; set; }
///        public string Device { get; set; }
///    }
///
///    [Serializable]
///    public class AuthenticateResponse
///    {
///        public string accessToken { get; set; }
///        public string appID       { get; set; }
///        public string id          { get; set; }
///        public string externalId  { get; set; }
///    }

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
        public string AppID  { get; set; } = "";
        public string ID     { get; set; } = "";
        public string Device { get; set; } = "";
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
        public string accessToken { get; set; } = "";
        public string appID       { get; set; } = "";
        public string id          { get; set; } = "";
        public string externalId  { get; set; } = "";
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Initialization

    [Serializable]
    public class InitializationRequest
    {
        public string access_token { get; set; } = "";
    }
    
    [Serializable]
    public class InitializationResponse
    {
        public ConfigurationData configuration { get; set; } = new ConfigurationData();
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Config

    [Serializable]
    public class ConfigurationData
    {
        public Dictionary<string, object> values { get; set; } = new Dictionary<string, object>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Payment Method Definitions

    
    [Serializable]
    public class PaymentMethodDefinitionsRequest
    {
    }
    
    [Serializable]
    public class PaymentMethodDefinitionsResponse
    {
        public List<PaymentMethodDefinitionData> paymentMethods { get; set; } = new List<PaymentMethodDefinitionData>();
    }
    
    [Serializable]
    public partial class PaymentMethodDefinitionData
    {
        public string              id                 { get; set; } = "";
        public string              name               { get; set; } = "";
        public string              type               { get; set; } = "";
        public string              iconUrl            { get; set; } = "";
        public string              logoUrl            { get; set; } = "";
        public List<PaymentAction> vaultingActions    { get; set; } = new List<PaymentAction>();
        public List<PaymentAction> transactionActions { get; set; } = new List<PaymentAction>();
    }

    [Serializable]
    public class CreditCardPaymentMethodDefinitionData : PaymentMethodDefinitionData
    {
        public string[] supported_card_types { get; set; } = new string[0];
    }
    
    [Serializable]
    public class PaypalPaymentMethodDefinitionData : PaymentMethodDefinitionData
    {
        
    }
    
    [Serializable]
    public class GooglePayPaymentMethodDefinitionData : PaymentMethodDefinitionData
    {
        
    }
    
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Payment Methods
    
    
    [Serializable]
    public class UserPaymentMethodsRequest
    {
    }
    
    [Serializable]
    public class UserPaymentMethodsResponse
    {
        public UserPaymentMethodData[] payment_methods { get; set; } = new UserPaymentMethodData[0];
    }
    
    [Serializable]
    public partial class UserPaymentMethodData
    {
        public string type        { get; set; } = "";
        public string id          { get; set; } = "";
        public string displayName { get; set; } = "";
    }
    
    [Serializable]
    public class CreditCardUserPaymentMethodData : UserPaymentMethodData
    {
    }
    [Serializable]
    public class GooglePayUserPaymentMethodData : UserPaymentMethodData
    {
    }
    [Serializable]
    public class PaypalUserPaymentMethodData : UserPaymentMethodData
    {
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Payment Actions
    
    [Serializable]
    public class PaymentAction
    {
        public string                     action  { get; set; } = "";
        public Dictionary<string, object> @params { get; set; } = new Dictionary<string, object>();
    }
    
    [Serializable]
    public class GenericPaymentAction : PaymentAction
    {
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// vaulting
    
    [Serializable]
    public class AddCardRequest
    {
        public string credit_card_token { get; set; } = "";
    }
    
    [Serializable]
    public class AddCardResponse
    {   
    }
    
    [Serializable]
    public class RemoveCardRequest
    {
        public string credit_card_token { get; set; } = "";
    }
    
    [Serializable]
    public class RemoveCardResponse
    {
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Transactions
    
    [Serializable]
    public class StartTransactionRequest
    {
        public string selected_payment_method_type { get; set; } = "";
        public string selected_payment_method_id   { get; set; } = "";
    }
    
    [Serializable]
    public class StartTransactionResponse
    {
        public string transaction_id { get; set; } = "";
    }
    
    [Serializable]
    public class TransactionResultData
    {
        public string   transaction_id { get; set; } = "";
        public bool     isSuccess      { get; set; }
        public bool     isCanceled     { get; set; }
        public string[] errors         { get; set; } = new string[0];
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Credit Card Actions

    [Serializable]
    public class ChargeRequest
    {
        public string                     sku      { get; set; } = "";
        public int                        quantity { get; set; }
        public decimal                    amount   { get; set; }
        public string                     currency { get; set; } = "";
        public Card                       card     { get; set; } = new Card();
        public Dictionary<string, string> metadata { get; set; } = new Dictionary<string, string>();
    }

    [Serializable]
    public class ChargeResponse
    {
        public bool                  Status             { get; set; }
        public string                PaymentId          { get; set; } = "";
        public TransactionResultData transaction_result { get; set; } = new TransactionResultData();
        public PaymentAction[]       NextActions        { get; set; } = new PaymentAction[0];
    }
    
    //// Helper Types
    
    [Serializable]
    public class Card
    {
        public string number   { get; set; } = "";
        public string expMonth { get; set; } = "";
        public string expYear  { get; set; } = "";
        public string cvc      { get; set; } = "";
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Unified Payment Platform
    
    
    [Serializable]
    public class PaymentInitiationRequest
    {
        public PaymentMethodDetails       method    { get; set; }
        public OrderDetails               order     { get; set; }
        public DateTime                   expiresAt { get; set; }
        public Dictionary<string, string> metadata  { get; set; }
    }

    [Serializable]
    public class PaymentInitiationResponse
    {
        public string              sessionId      { get; set; } = "";
        public string              orderId        { get; set; } = "";
        public string              transactionId  { get; set; } = "";
        public List<PaymentAction> actions        { get; set; } = new List<PaymentAction>();
        public bool                success        { get; set; } = true;
        public bool                requiresAction { get; set; } = true;
        public string              errorMessage   { get; set; } = "";
    }

    [Serializable]
    public class ValidatePaymentRequest
    {
        public string   PaymentId { get; set; }
        public string?  Sku       { get; set; }
        public int?     Quantity  { get; set; }
        public decimal? Amount    { get; set; }
        public string?  Currency  { get; set; }
    }
    
    [Serializable]
    public class PaymentMethodDetails
    {
        public string                     id   { get; set; }
        public Dictionary<string, object> data { get; set; }
    }

    [Serializable]
    public class OrderDetails
    {
        public string  sku      { get; set; }
        public string  currency { get; set; }
        public decimal amount   { get; set; }
        public int     quantity { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Socket Actions
    
    [Serializable]
    public class ServerSocketRequest
    {
        public string action { get; set; } = "";
    }
    
    
    [Serializable]
    public class ServerSocketResponse
    {
        public string socket_ip   { get; set; } = "";
        public int    socket_port { get; set; }
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
        public string transaction_id { get; set; } = "";
        public string recipt_data    { get; set; } = "";
        public string date_time_utc  { get; set; } = "";
    }
    
    /// /validate_receipt
    /// {
    ///     result : "valid"
    /// }
    [Serializable]
    public class ValidateReceiptResponse
    {
        public string result { get; set; } = "";
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tax

    /// /tax request example :
    /// {
    ///     location : "usa"
    /// }
    [Serializable]
    public class TaxRequest
    {
        public string location { get; set; } = "";
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
        public Dictionary<string, float> taxes                              { get; set; } = new Dictionary<string, float>();
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Email
    
    [Serializable]
    public class UpdateEmailRequest
    {
        public string user_id { get; set; } = "";
        public string email   { get; set; } = "";
    }
    
    [Serializable]
    public class UpdateEmailResponse
    {
    }
}

