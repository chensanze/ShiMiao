using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Common
{
    public sealed class Constants
    {
        public const string ResourceVersion = "20170720004";
        public enum StatusCode : int
        {
            Succeed,
            Failed = 10000,
            UnLogined
        }

        /// <summary>
        /// 分页
        /// </summary>
        public static class Pages
        {
            public const int BigPageSize = 20;
            /// <summary>
            /// 每页显示的记录数（默认值）
            /// </summary>
            public const int PageSize = 10;

            public const int SmallPageSize = 5;

            /// <summary>
            /// 最多显示几页的数据（默认值）
            /// </summary>
            public const int pageNumShown = 5;
        }

        public enum DonationType
        {
            /// <summary>
            /// 日行一善1
            /// </summary>
            RXYS1 = 1,
            /// <summary>
            /// 功能箱
            /// </summary>
            GDX1 = 2,
           /// <summary>
           /// 项目捐款
           /// </summary>
            XMJK1 = 3,
            /// <summary>
            /// 商城
            /// </summary>
            Shop1 = 4,
        }

        public class DonationID
        {
            public const string RXYS1 = "6a76ea9f-3c7d-43ea-a74d-fccf696b1674";
            public const string GDX1 = "c9430f2a-dd62-41ee-953b-e8433237486b";
        }

        public static class PayCode
        {
            public const string Shop = "11";
            public const string Donation = "15";
        }

        public static class PayType
        {
            /// <summary>
            /// 余额支付
            /// </summary>
            public const int Balance = 1;
            /// <summary>
            /// 微信
            /// </summary>
            public const int WeiXin = 2;
            /// <summary>
            /// 支付宝
            /// </summary>
            public const int ZhiFuBao = 3;
            /// <summary>
            /// 货到付款
            /// </summary>
            public const int Freight = 4;
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public static class OrderStatus
        {
            /// <summary>
            /// 待支付
            /// </summary>
            public const int WaitPay = 1;
            /// <summary>
            /// 待发货
            /// </summary>
            public const int WaitSend = 2;
            /// <summary>
            /// 待收货
            /// </summary>
            public const int WaitReceive = 3;
            /// <summary>
            /// 待评价
            /// </summary>
            public const int WaitEvaluate = 4;
            /// <summary>
            /// 已完成
            /// </summary>
            public const int Completed = 5;
            /// <summary>
            /// 已取消
            /// </summary>
            public const int Cancel = 6;
        }
    }
}
