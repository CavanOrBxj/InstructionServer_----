using System;

namespace InstructionServer
{
    public class AdminAccountEventArgs : EventArgs
    {
        private bool adminAccount;
        /// <summary>
        /// 是否为管理员账户
        /// </summary>
        public bool AdminAccount { get { return adminAccount; } }

        public AdminAccountEventArgs(bool isAdminAccount)
        {
            adminAccount = isAdminAccount;
        }
    }
}
