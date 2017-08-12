using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipaySecurityProdXwbtestPurchaseModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipaySecurityProdXwbtestPurchaseModel : AopObject
    {
        /// <summary>
        /// 参数描述必须通俗易懂、无错别字、完整。描述的内容请按此格式填写：参数名+是否唯一(如需)+应用场景+枚举值(如有)+如何获取+特殊说明(如有)。如不符合标准终审会驳回，影响上线时间。
        /// </summary>
        [XmlElement("add")]
        public string Add { get; set; }

        /// <summary>
        /// 西湖区文三路国际大厦
        /// </summary>
        [XmlElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// 参数描述必须通俗易懂、无错别字、完整。描述的内容请按此格式填写：参数名+是否唯一(如1需)+应用场景+枚举值(如有)+如何获取+特殊说明(如有)。如不符合标准终审会驳回，影响上线时间。
        /// </summary>
        [XmlElement("idcard")]
        public string Idcard { get; set; }

        /// <summary>
        /// s
        /// </summary>
        [XmlElement("sdf")]
        public AlipayKeyanClass Sdf { get; set; }

        /// <summary>
        /// 1 1
        /// </summary>
        [XmlArray("sdfx")]
        [XmlArrayItem("string")]
        public List<string> Sdfx { get; set; }

        /// <summary>
        /// 蚂蚁统一会员ID
        /// </summary>
        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}
