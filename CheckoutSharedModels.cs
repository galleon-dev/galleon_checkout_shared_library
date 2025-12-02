using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

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
///        public string AppID;  
///        public string ID;     
///        public string Device; 
///    }
///
///    [Serializable]
///    public class AuthenticateResponse
///    {
///        public string accessToken; 
///        public string appID;       
///        public string id;          
///        public string externalId;  
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
        public string app_id = "";
        public string id     = "";
        public string device = "";
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
        public string access_token = "";
        public string app_id       = "";
        public string id           = "";
        public string external_id  = "";
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Initialization

    [Serializable]
    public class InitializationRequest
    {
        public string access_token = "";
    }
    
    [Serializable]
    public class InitializationResponse
    {
        public ConfigurationData configuration = new ConfigurationData();
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Config

    [Serializable]
    public class ConfigurationData
    {
        public Dictionary<string, object> values = new Dictionary<string, object>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Payment Method Definitions

    
    [Serializable]
    public class PaymentMethodDefinitionsRequest
    {
        public string currency_code; 
        public string country;       
    }
    
    /// Response Example :
    /// {
    ///     "definitions" :
    ///     [
    ///         {
    ///             "type"                   : "credit_card",
    ///             "supported_card_types"   : [ "visa", "master_card", "..." ],
    ///             "icon_url"               : "http://blablabla.png"
    ///             "logo_url"               : "http://blablabla.png"
    ///             "initialization_actions" : null,
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
    ///             "charge_actions"         :
    ///                                      [
    ///                                          { "action" : "charge" }
    ///                                      ],
    ///         },
    ///         {
    ///             "type"                   : "google_pay",
    ///             "icon_url"               : "http://blablabla.png"
    ///             "logo_url"               : "http://blablabla.png"
    ///             "initialization_actions" : [ { "action" : "check_google_pay_availability" } ],
    ///             "vaulting_actions"       : null,
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
        public List<PaymentMethodDefinitionData> definitions = new List<PaymentMethodDefinitionData>();
    }
    
    [Serializable]
    public partial class PaymentMethodDefinitionData
    {
        public string                type                   = "";
        
        public List<PaymentProvider> providers              = new List<PaymentProvider>();
        
        public string                icon_url               = "";
        public string                logo_url               = "";
        
        public List<PaymentAction>   initialization_actions = new List<PaymentAction>();
        public List<PaymentAction>   vaulting_actions       = new List<PaymentAction>();
        public List<PaymentAction>   charge_actions         = new List<PaymentAction>();
    }

    [Serializable]
    public class CreditCardPaymentMethodDefinitionData : PaymentMethodDefinitionData
    {
        public string[] supported_card_types = new string[0];
    }
    
    [Serializable]
    public class PaypalPaymentMethodDefinitionData : PaymentMethodDefinitionData
    {
        
    }
    
    [Serializable]
    public class GooglePayPaymentMethodDefinitionData : PaymentMethodDefinitionData
    {
        
    }
    
    
    [Serializable]
    public class PaymentProvider
    {
        public string                     provider = "";
        public Dictionary<string, object> config   = new();
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
        public TokenizerData tokenizer_data;
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
    
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Vaulting
    
    /// ADD-CREDIT-CARD Example :
    /// {
    ///     "payment_method_definition_type" : "credit_card",
    ///     "credit_card_token"              : "1234"
    /// }
    /// ADD-OTHER-PAYMENT-METHOD Example :
    /// {
    ///     "payment_method_definition_type" : "paypal",
    ///     "credit_card_token"              : null
    /// }
    [Serializable]
    public class AddPaymentMethodRequest
    {
        public string payment_method_definition_type = "";
        public string credit_card_token              = "";
    }
    
    [Serializable]
    public class AddPaymentMethodResponse
    {
        public UserPaymentMethodData created_payment_method;
    }
    
    [Serializable]
    public class RemovePaymentMethodRequest
    {
        public string payment_method_id = "";
    }
    
    [Serializable]
    public class RemovePaymentMethodResponse
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
    ///                               "id"               : "424563487tukviu6rc5l875vl87i",
    ///                               "display_name"     : "Master Card - **** - 1234",
    ///
    ///                               "credit_card_type" : "master_card",
    ///                               "expiration_date"  : "2025-12-31T23:59:59"
    ///                           },
    ///                           {
    ///                               "type"             : "google_pay",
    ///                               "id"               : "5i764vio764oi75fo75co765fi765vo",
    ///                               "display_name"     : "GPay - jhon.doe@gmail.com"
    ///                           },
    ///                       ]
    /// }
    [Serializable]
    public class UserPaymentMethodsResponse
    {
        public UserPaymentMethodData[] payment_methods = new UserPaymentMethodData[0];
    }
    
    [Serializable]
    public partial class UserPaymentMethodData
    {
        public string type             = "";
        public string id               = "";
        public string display_name     = "";
        public string credit_card_type = "";
    }
    
    [Serializable]
    public class CreditCardUserPaymentMethodData : UserPaymentMethodData
    {
        public string   credit_card_type;
        public DateTime expiration_date;
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
        public string                     action     = "";
        public Dictionary<string, object> parameters = new Dictionary<string, object>();
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Session
    
    /// Request Example
    /// {
    ///     "order"      :
    ///                  {
    ///                      "sku"      : "1234",
    ///                      "currency" : "USD",
    ///                      "amount"   : "4.99"
    ///                  },
    ///     "expires_at" : "2025-12-31T23:59:59",
    ///     "metadata"   :
    ///                  {
    ///                      "my_field_1" : "my_value",
    ///                      "my_field_2" : "my_value",
    ///                      "my_field_3" : "my_value"
    ///                  }
    /// }
    [Serializable]
    public class CheckoutSessionRequest
    {
        public OrderDetails               order;      
        public DateTime                   expires_at; 
        public Dictionary<string, string> metadata;   
    }

    /// Response Example :
    /// {
    ///     "session_id" : "1234",
    ///     "price_data" :
    ///                  {
    ///                      "total_price"     : 9.99,
    ///                      "subtotal_price"  : 5.99,
    ///                      "tax"             :
    ///                                        {
    ///                                            should_display_taxes : true,
    ///                                            taxes                :
    ///                                                                 {
    ///                                                                     "Tax" : { 4.99, true  }, // true  = "inclusive"     = this item is pat of the subtotal
    ///                                                                     "IRS" : { 2,    false }  // false = "not inclusive" = outside of subtotal
    ///                                                                 }
    ///                                        }
    ///                  },
    /// 
    ///     "order"      :
    ///                  {
    ///                      "sku"      : "1234",
    ///                      "currency" : "USD",
    ///                      "amount"   : "4.99"
    ///                  },
    ///     "expires_at" : "2025-12-31T23:59:59",
    ///     "status"     : "created" (/"in_progress"/"cancelled"/"whatever")
    /// }
    [Serializable]
    public class CheckoutSessionResponse
    {
        public string       session_id    = "";
        public PriceData    price_data    = new PriceData();
        
        public OrderDetails order;         
        public DateTime     expires_at;    
        public string       status         = "";
    }
    
    [Serializable]
    public class CancelCheckoutSessionRequest
    {
        public string session_id = "";
    }
    [Serializable]
    public class CancelCheckoutSessionResponse
    {
    }
    
    [Serializable]
    public class OrderDetails
    {
        public string  sku;      
        public string  currency; 
        public decimal amount;   
    }
    
    
    [Serializable]
    public class PriceData
    {
        public decimal total_price;    
        public decimal subtotal_price; 
        public TaxData tax;            
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tax Helper Classes

    [Serializable]
    public class TaxData
    {
        public bool                        should_display_taxes;
        public Dictionary<string, TaxItem> taxes                 = new();
    }
    
    [Serializable]
    public class TaxItem
    {
        public decimal tax_amount;
        public bool    inclusive;
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
    ///                                            "type"  : "credit_card"      // type of the payment method definition
    ///                                            "token" : "3o84756hb3975f"
    ///                                        }
    ///                             },
    ///     "metadata"              :
    ///                             {
    ///                                 "my_field" : "my_value"
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
    ///                             },
    ///     "metadata"              :
    ///                             {
    ///                                 "my_field" : "my_value"
    ///                             }
    /// }
    [Serializable]
    public class ChargeRequest
    {
        public string                     session_id             = "";
        public bool                       save_payment_method;   
        public bool                       is_new_payment_method; 
        public PaymentMethodDetails       payment_method         = new PaymentMethodDetails();
        public Dictionary<string, string> metadata;
        public string                     success_redirect_url;
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
    ///                             "action"     : "open_url",        // (3ds)
    ///                             "parameters" :
    ///                                          {
    ///                                              "url"            : "https://blablabla.com"
    ///                                              "socket_address" : "https://socket_address" (optional)
    ///                                          }
    ///                             }
    ///                    ] 
    /// }
    [Serializable]
    public class ChargeResponse
    {
        public ChargeResultData  result;      
        public PaymentAction[]   next_actions = new PaymentAction[0];
    }
    
    
    [Serializable]
    public class ChargeResultData
    {
        public string   charge_id        = "";
        public bool     is_success;     
        public bool     is_canceled;     
        public string[] errors           = new string[0];
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Helper Types
    
    [Serializable]
    public class PaymentMethodDetails
    {
        public string                     id;   
        public Dictionary<string, object> data; 
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Email
    
    [Serializable]
    public class UpdateEmailRequest
    {
        public string session_id = "";
        public string email      = "";
    }
    
    [Serializable]
    public class UpdateEmailResponse
    {
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// User Data
    
    [Serializable]
    public class UserInfo
    {
        public string email = "";
    }
}

