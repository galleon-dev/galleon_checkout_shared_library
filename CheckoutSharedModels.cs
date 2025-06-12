using System.Collections;
using System.Collections.Generic;

namespace Galleon.Checkout.Shared
{
    public class GenericData
    {
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
        ///     "accessToken" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InBheWVyIiwiaXNzIjoiR2FsbGVvbiIsImF1ZCI6InRlc3QuYXBwIn0.xdg3d7xMIVAhDm_9JpSl5MENnmWAaZDHRjApWUMj-t8",
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
    }
}
