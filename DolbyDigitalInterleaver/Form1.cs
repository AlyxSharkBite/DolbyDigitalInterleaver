namespace DolbyDigitalInterleaver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ac3Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                AutoUpgradeEnabled = true,
                Filter = "AC3 Files (*.ac3)|*.ac3|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            ac3TextBox.Text = openFileDialog.FileName;
        }

        private void mlpButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                AutoUpgradeEnabled = true,
                Filter = "TrueHD Files (*.mlp, *.thd)|*.mlp;*.thd|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            mlpTextBox.Text = openFileDialog.FileName;
        }

        private void InterleaveButton_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(ac3TextBox.Text) ||
               String.IsNullOrWhiteSpace(mlpTextBox.Text))
            {
                MessageBox.Show("Must select an AC3 & TrueHD file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var interleaver = InterleaveLib.Factory.InterleaveFactory.GetInterleaver();

            try
            {
                var bytes = interleaver.InterleaveBitStreams(ac3TextBox.Text, mlpTextBox.Text);

                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    AddExtension = true,
                    CheckPathExists = true,
                    AutoUpgradeEnabled = true,
                    Filter = "Interleaved Bitstream (*.thd+ac3)|*.thd+ac3",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    File.WriteAllBytes(saveFileDialog.FileName, bytes.ToArray());

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
