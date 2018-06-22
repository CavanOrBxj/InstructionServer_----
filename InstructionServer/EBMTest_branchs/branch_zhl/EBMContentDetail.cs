using EBMTable;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMContentDetail : Form
    {
        private List<EBMContent.Auxiliary> auxiliaryDataList;

        public EBMContentDetail(List<EBMContent.Auxiliary> list)
        {
            InitializeComponent();
            auxiliaryDataList = list;
            InitPanelLayout();
        }

        private void InitPanelLayout()
        {
            Size = new Size(pnlAuxiliaryData.Width + 18, pnlAuxiliaryData.Height + 90);
            if (auxiliaryDataList != null)
            {
                pnlAuxiliaryData.InitData(auxiliaryDataList);
            }
        }

        public List<EBMContent.Auxiliary> GetData()
        {
            return pnlAuxiliaryData.GetData();
        }

    }
}
