#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Collections.Generic;

namespace doremi.wwwroot.content.ejservices.wcf.PivotGauge
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRelationalGauge" in both code and config file together.
    [ServiceContract]
    public interface IRelational
    {
        [OperationContract]
        Dictionary<string, object> Initialize(string action, string customObject);
    }
}
