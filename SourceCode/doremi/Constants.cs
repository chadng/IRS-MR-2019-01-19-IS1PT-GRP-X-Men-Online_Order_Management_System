using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doremi
{
    public static class OrderProgressStatus
    {
        public const int DRAFT = 1;
        public const int CONFIRMED = 2;
        public const int APPROVED = 3;
        public const int SHIPPING = 4;
        public const int CLOSED = 5;
        public const int CANCELLED = 6;
        public const int REDEEMED = 7;
        public const int PACKING = 8;
        public const int PRINTING = 9;
        public const int BALANCE_VERIFIED = 10;
    }

    public static class Groups
    {
        public const String WAREHOUST = "Warehouse";
        public const String SALES = "Sales";
        public const String OPERATION = "Operation";
        public const String ACCOUNT = "Account";
    }

    public static class ApiAction
    {
        public const String INSESRT = "created";
        public const String UPDATED = "updated";
        public const String REMOVED = "deleted";
    }
}
