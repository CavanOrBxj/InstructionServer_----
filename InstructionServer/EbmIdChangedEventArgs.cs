using System;
using System.Collections.Generic;

namespace InstructionServer
{
    public class EbmIdChangedEventArgs : EventArgs
    {
        private List<string> listEbmId;
        /// <summary>
        /// EbmId列表
        /// </summary>
        public List<string> ListEbmId { get { return listEbmId; } }

        public EbmIdChangedEventArgs(List<string> listEbmId)
        {
            this.listEbmId = listEbmId;
        }
    }
}
