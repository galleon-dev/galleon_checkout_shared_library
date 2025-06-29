using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Galleon.Checkout.Shared
{
    public class PaymentMethodsJsonHelper
    {
        public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { Converters = new List<JsonConverter>()
                                                                                            {
                                                                                                new PaymentMethodData          .PMJsonConverter (),
                                                                                                new PaymentMethodDefinitionData.PMDJsonConverter(),
                                                                                            },
                                                                                            TypeNameHandling = TypeNameHandling.None,
                                                                                         };
    }
    
    public partial class PaymentMethodDefinitionData
    {
        public class PMDJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) => typeof(PaymentMethodDefinitionData).IsAssignableFrom(objectType);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var jo   = JObject.Load(reader);
                var type = jo["type"]?.ToString();

                PaymentMethodDefinitionData obj = type switch
                                                {
                                                    "credit_card" => new CreditCardPaymentMethodDefinitionData(),
                                                    "gpay"        => new GooglePayPaymentMethodDefinitionData(),
                                                    "paypal"      => new PaypalPaymentMethodDefinitionData(),
                                                    _             => throw new Exception($"Unknown type: {type}")
                                                };

                serializer.Populate(jo.CreateReader(), obj);
                return obj;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                JObject jo = JObject.FromObject(value, serializer);
                jo.WriteTo(writer);
            }
        }
    }
    
    public partial class PaymentMethodData
    {
        public class PMJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) => typeof(PaymentMethodData).IsAssignableFrom(objectType);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var jo   = JObject.Load(reader);
                var type = jo["type"]?.ToString();

                PaymentMethodData obj = type switch
                                      {
                                          "credit_card" => new CreditCardPaymentMethodData(),
                                          "gpay"        => new GooglePayPaymentMethodData(),
                                          "paypal"      => new PaypalPaymentMethodData(),
                                          _             => throw new Exception($"Unknown type: {type}")
                                      };

                serializer.Populate(jo.CreateReader(), obj);
                return obj;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                JObject jo = JObject.FromObject(value, serializer);
                jo.WriteTo(writer);
            }
        }
    }
}