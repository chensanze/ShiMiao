using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Aop.Api.Domain;

namespace Aop.Api.Response
{
    /// <summary>
    /// KoubeiMarketingToolIsvMerchantQueryResponse.
    /// </summary>
    public class KoubeiMarketingToolIsvMerchantQueryResponse : AopResponse
    {
        /// <summary>
        /// 商户信息列表
        /// </summary>
        [XmlArray("merchant_infos")]
        [XmlArrayItem("isv_merchant_info")]
        public List<IsvMerchantInfo> MerchantInfos { get; set; }
    }
}
