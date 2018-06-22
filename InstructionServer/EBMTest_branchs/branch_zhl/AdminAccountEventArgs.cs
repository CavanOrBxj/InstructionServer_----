using System;

namespace EBMTest
{
    public class AdminAccountEventArgs : EventArgs
    {
        /// <summary>
        /// 是否为管理员账户
        /// </summary>
        public bool AdminAccount { get; }

        public AdminAccountEventArgs(bool isAdminAccount)
        {
            AdminAccount = isAdminAccount;
        }
    }
}
