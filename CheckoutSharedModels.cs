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
    
    /// Response Example :
    /// {
    ///     "definitions" :
    ///     [
    ///         {
    ///             "type"                   : "credit_card",
    /// 
    ///             "supported_card_types"   : [ "visa", "master_card", "..." ],
    /// 
    ///             "icon_url"               : "http://blablabla.png"
    ///             "logo_url"               : "http://blablabla.png"
    /// 
    ///             "initialization_actions" : null,
    /// 
    ///             "vaulting_actions"       :
    ///                                      [
    ///                                           {
    ///                                               "action"     : "get_tokenizer",
    ///                                               "parameters" : null
    ///                                           },
    ///                                           {
    ///                                               "action"     : "tokenize",
    ///                                               "parameters" : null
    ///                                           },
    ///                                      ],
    /// 
    ///             "charge_actions"         :
    ///                                      [
    ///                                          { "action" : "charge" }
    ///                                      ],
    ///         },
    ///         {
    ///             "type"                   : "google_pay",
    /// 
    ///             "icon_url"               : "http://blablabla.png"
    ///             "logo_url"               : "http://blablabla.png"
    /// 
    ///             "initialization_actions" : [ { "action" : "check_google_pay_availability" } ],
    /// 
    ///             "vaulting_actions"       : null,
    /// 
    ///             "charge_actions"         :
    ///                                      [
    ///                                          {
    ///                                              "action"     : "open_url"
    ///                                              "parameters" :
    ///                                                           {
    ///                                                               "url"            : "https://blablabla.com"
    ///                                                               "deep_link_path" : "checkout/blabla"
    ///                                                               "socket_address" : "https://socket_address"
    ///                                                           }
    ///                                          }
    ///                                      ],
    ///         },
    ///         
    ///     ]
    /// }
    /// 
    [Serializable]
    public class PaymentMethodDefinitionsResponse
    {
        public List<PaymentMethodDefinitionData> definitions { get; set; } = new List<PaymentMethodDefinitionData>();
    }
    
    [Serializable]
    public partial class PaymentMethodDefinitionData
    {
        public string              type                     { get; set; } = "";
        
        public string              icon_url                 { get; set; } = "";
        public string              logo_url                 { get; set; } = "";
        
        public List<PaymentAction> initialization_actions   { get; set; } = new List<PaymentAction>();
        public List<PaymentAction> vaulting_actions         { get; set; } = new List<PaymentAction>();
        public List<PaymentAction> charge_actions           { get; set; } = new List<PaymentAction>();
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
    
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tokenization
    
    [Serializable]
    public class GetTokenizerRequest
    {
    }
    
    /// Response Example :
    /// {
    ///   "Timestamp" : 1742385314,
    ///   "Payload"   :
    ///               {
    ///                   "ServiceUrl"    : "https://api.basistheory.com/tokens",
    ///                   "RequestFormat" : "{ type: "card", data: { "number": "<CC_NUMBER>", "expiration_month": <CC_MONTH>, "expiration_year": <CC_YEAR>, "cvc": "<CC_CVC>"  } }",
    ///                   "Headers"       :
    ///                                   {
    ///                                       "Content-Type" : "application/json",
    ///                                       "BT-API-KEY"   : "key_test_us_pvt_Vii1FVRcm9BVtiUjZQBsX.2cb2d1e874906289f09adb4640bdf3d9"
    ///                                   }
    ///               }
    /// }
    [Serializable]
    public class GetTokenizerResponse
    {
        public TokenizerData tokenizer_data { get; set; }
    }
    
    public class TokenizerData
    {
        public long             Timestamp = 0L;
        public TokenizerPayload Payload   = new();
        
        public class TokenizerPayload
        {
            public string                     ServiceUrl    = "";
            public string                     RequestFormat = "";
            public Dictionary<string, string> Headers       = new();
        }
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
    
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Payment Methods
    
    
    [Serializable]
    public class UserPaymentMethodsRequest
    {
    }
    
    /// Response Example:
    /// {
    ///     "payment_methods" :
    ///                       [
    ///                           {
    ///                               "type"             : "credit_card",
    ///                               "id"               : "4",
    ///                               "display_name"     : "Master Card - **** - 1234",
    ///
    ///                               "credit_card_type" : "master_card"
    ///                           },
    ///                           {
    ///                               "type"             : "google_pay",
    ///                               "id"               : "5",
    ///                               "display_name"     : "GPay - jhon.doe@gmail.com"
    ///                           },
    ///                       ]
    /// }
    [Serializable]
    public class UserPaymentMethodsResponse
    {
        public UserPaymentMethodData[] payment_methods { get; set; } = new UserPaymentMethodData[0];
    }
    
    [Serializable]
    public partial class UserPaymentMethodData
    {
        public string type         { get; set; } = "";
        public string id           { get; set; } = "";
        public string display_name { get; set; } = "";
    }
    
    [Serializable]
    public class CreditCardUserPaymentMethodData : UserPaymentMethodData
    {
        public string credit_card_type;
    }
    [Serializable]
    public class GooglePayUserPaymentMethodData : UserPaymentMethodData
    {
    }
    [Serializable]
    public class PaypalUserPaymentMethodData : UserPaymentMethodData
    {
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Action Helper Class
    
    [Serializable]
    public class PaymentAction
    {
        public string                     action     { get; set; } = "";
        public Dictionary<string, object> parameters { get; set; } = new Dictionary<string, object>();
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Session
    
    /// Request Example
    /// {
    ///     "order"      :
    ///                  {
    ///                      "sku"      : "1234",
    ///                      "currency" : "USD",
    ///                      "amount"   : "4.99",
    ///                      "quantity" : "1"
    ///                  },
    ///     "expires_at" : "2025/12/31 23:59"
    /// }
    [Serializable]
    public class CreateCheckoutSessionRequest
    {
        public OrderDetails order      { get; set; }
        public DateTime     expires_at { get; set; }
    }

    /// Response Example :
    /// {
    ///     "session_id" : "1234",
    ///     "price_data" :
    ///                  {
    ///                      "total_price"     : 9.99,
    ///                      "sub_total_price" : 5.99,
    ///                      "tax"             :
    ///                                        {
    ///                                            should_display_price_including_tax : false,
    ///                                            taxes                              :
    ///                                                                               {
    ///                                                                                   "Tax" : { 4.99, true  },
    ///                                                                                   "IRS" : { 2,    false }         
    ///                                                                               }
    ///                                        }
    ///                  }
    /// }
    [Serializable]
    public class CreateCheckoutSessionResponse
    {
        public string    session_id    { get; set; } = "";
        public PriceData price_data    { get; set; } = new PriceData();
    }
    
    
    [Serializable]
    public class OrderDetails
    {
        public string  sku      { get; set; }
        public string  currency { get; set; }
        public decimal amount   { get; set; }
        public int     quantity { get; set; }
    }
    
    
    [Serializable]
    public class PriceData
    {
        public decimal total_price    { get; set; }
        public decimal subtotal_price { get; set; }
        public TaxData tax            { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tax Helper Classes

    [Serializable]
    public class TaxData
    {
        public bool                        should_display_price_including_tax { get; set; }
        public Dictionary<string, TaxItem> taxes                              { get; set; } = new();
    }
    
    [Serializable]
    public class TaxItem
    {
        public decimal tax_amount { get; set; }
        public bool    inclusive  { get; set; }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Charge
    
    /// /////////// NEW-CREDIT-CARD Request Example :
    /// {
    ///     "session_id"            : "1234",
    ///     "save_payment_method"   : "true",
    ///     "is_new_payment_method" : "true",
    ///     "payment_method"        :
    ///                             {
    ///                                 "data" :
    ///                                        {
    ///                                            "type"  : "credit_card" // type of the payment method definition
    ///                                            "token" : "1234"
    ///                                        }
    ///                             }
    /// }
    /// /////////// EXISTING-PAYMENT-METHOD Request Example :
    /// {
    ///     "session_id"            : "1234",
    ///     "save_payment_method"   : "false",
    ///     "is_new_payment_method" : "false",
    ///     "payment_method"        :
    ///                             {
    ///                                 "id" : "4"
    ///                             }
    /// }
    [Serializable]
    public class ChargeRequest
    {
        public string               session_id            { get; set; } = "";
        public bool                 save_payment_method   { get; set; }
        public bool                 is_new_payment_method { get; set; }
        public PaymentMethodDetails payment_method        { get; set; } = new PaymentMethodDetails();
    }

    /// Response Example :
    /// {
    ///     "result"       :
    ///                    {
    ///                         "charge_id"    : "1234",
    ///                         "is_success"   : "true",
    ///                         "is_canceled"  : "false",
    ///                         "errors"       : null
    ///                    }
    ///     "next_actions" :
    ///                    [
    ///                         {
    ///                             "action"     : "3ds",
    ///                             "parameters" : { "field" : "value" } 
    ///                         }
    ///                    ] 
    /// }
    [Serializable]
    public class ChargeResponse
    {
        public ChargeResultData  result       { get; set; }
        public PaymentAction[]   next_actions { get; set; } = new PaymentAction[0];
    }
    
    
    [Serializable]
    public class ChargeResultData
    {
        public string   charge_id       { get; set; } = "";
        public bool     is_success      { get; set; }
        public bool     is_canceled     { get; set; }
        public string[] errors          { get; set; } = new string[0];
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Helper Types
    
    [Serializable]
    public class Card
    {
        public string number   { get; set; } = "";
        public string expMonth { get; set; } = "";
        public string expYear  { get; set; } = "";
        public string cvc      { get; set; } = "";
    }
    
    [Serializable]
    public class PaymentMethodDetails
    {
        public string                     id   { get; set; }
        public Dictionary<string, object> data { get; set; }
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

