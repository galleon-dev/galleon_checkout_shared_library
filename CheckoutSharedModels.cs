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
    public class AuthenticateRequest
    {
        public string AppID;
        public string ID;
        public string Device;
    }
    
    /// /authenticate Response example :
    /// {
    ///     "accessToken" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.........xdg3d7xMIVAhDm_9JpSl5MENnmWAaZDHRjApWUMj-t8",
    ///     "appId"       : "test.app",
    ///     "id"          : 1,
    ///     "externalId"  : "user id 1"
    /// }
    public class AuthenticateResponse
    {
        public string accessToken;
        public string appID;
        public string id;
        public string externalId;
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tax
    
    public class TaxData
    {
        public bool                      should_display_price_including_tax;
        public Dictionary<string, float> taxes;
    }
}
