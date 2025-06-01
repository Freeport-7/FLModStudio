namespace FreelancerModStudio
{
    using System.Windows.Forms;

    using FreelancerModStudio.SystemDesigner;

    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            this.InitializeComponent();
            this.propertyGrid.SelectedObject = Helper.Settings.Data.Data.General;
            this.propertyGrid.ExpandAllGridItems();
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.Settings.Save();
            SharedGeometries.LoadColors(Helper.Settings.Data.Data.General.ColorBox);
            MainForm form = (MainForm) this.Owner;
            MessageBox.Show("Some settings will require a restart in order to take place.", "Settings Saved.", MessageBoxButtons.OK);
        }
    }
}
