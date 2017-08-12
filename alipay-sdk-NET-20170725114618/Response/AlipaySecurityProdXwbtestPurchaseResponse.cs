using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Aop.Api.Response
{
    /// <summary>
    /// AlipaySecurityProdXwbtestPurchaseResponse.
    /// </summary>
    public class AlipaySecurityProdXwbtestPurchaseResponse : AopResponse
    {
        /// <summary>
        /// 参数描述必须通俗易懂、无错别字、完整。描述的内容请按此格式填写：参数名+是否唯一(如需)+应用场景+枚举值(如有)+如何获取+特殊说明(如有)。如不符合标准终审会驳回，影响上线时间。
        /// </summary>
        [XmlElement("add")]
        public string Add { get; set; }

        /// <summary>
        /// 1
        /// </summary>
        [XmlElement("as")]
        public string As { get; set; }

        /// <summary>
        /// 参数描述必须通俗易懂、无错别字、完整。描述的内容请按此格式填写：参数名+是否唯一(如需)+应用场景+枚举值(如有)+如何获取+特殊说明(如有)。如不符合标准终审会驳回，影响上线时间。
        /// </summary>
        [XmlArray("del")]
        [XmlArrayItem("boolean")]
        public List<bool> Del { get; set; }

        /// <summary>
        /// 参数描述必须1通俗易懂、无错别字、完整。描述的内容请按此格式填写：参数名+是否唯一(如需)+应用场景+枚举值(如有)+如何获取+特殊说明(如有)。如不符合标准终审会驳回，影响上线时间。
        /// </summary>
        [XmlElement("idcard")]
        public string Idcard { get; set; }
    }
}
