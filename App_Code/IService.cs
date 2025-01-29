using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

// NOTE: If you change the interface name "IService" here, you must also update the reference to "IService" in Web.config.

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        DataSet eTransactionCount_Registration(string QryDate, string UserID, string Password);
        [OperationContract]
        DataSet eTransactionCount_Application(string QryDate, string UserID, string Password);
        [OperationContract]
        DataSet eTransactionCount_Fee(string QryDate, string UserID, string Password);

        // TODO: Add your service operations here
    }



