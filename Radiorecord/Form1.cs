using System;
using System.Resources;
using System.Windows.Forms;
using WMPLib;

namespace Radio
{
    public partial class Form1 : Form
    {
        private Hook _hook;
        private WindowsMediaPlayer wmPlayer = new WindowsMediaPlayer();

        public Form1()
        {
            vars.ListOfStations();
            CreateButtons();

            InitializeComponent();
            Show();
            WindowState = FormWindowState.Normal;

            wmPlayer.settings.volume = 80;
            label_volume.Text = "Volume " + wmPlayer.settings.volume.ToString();
            trackBar1.Value = (wmPlayer.settings.volume / 10);

            KeyPreview = true;

            _hook = new Hook(0x91); //Scroll Lock
            _hook.KeyPressed += new KeyPressEventHandler(_hook_KeyPressed);
            _hook.SetHook();
        }

        private void CreateButtons()
        {
            int buttonsInLine = 8;
            int buttonWidth = 104;
            int buttonHeight = 125;
            int lineBufer = 0;
            int rowPoint = 0;
            int elementNumber = 0;
            int lineNumber = 0;
            ResourceManager rm = new ResourceManager("Radio.Properties.Resources", typeof(Radio.Properties.Resources).Assembly);

            for (int id = 1; id < (vars.StationList.Count + 1); id++)
            {

                lineNumber = (id-1) / buttonsInLine;

                if (lineNumber == lineBufer)
                {
                    rowPoint = elementNumber * buttonWidth;
                    elementNumber++;
                }
                else
                {
                    lineBufer = lineNumber;
                    rowPoint = 0;
                    elementNumber = 0;
                    rowPoint = elementNumber * buttonWidth;
                    elementNumber++;
                }

                Button button = new Button();
                button.BackColor = System.Drawing.Color.Transparent;
                button.BackgroundImage = vars.StationList.Find(x => x._id == id)._image;
                button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                button.Location = new System.Drawing.Point(rowPoint, lineNumber * buttonHeight);
                button.Name = "button" + id;
                button.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                button.TabIndex = id;
                button.UseVisualStyleBackColor = false;
                button.Visible = true;
                button.Click += new System.EventHandler(this.StartPlayer);

                if (button != null)
                {
                    this.Controls.Add(button);
                }

                this.ClientSize = new System.Drawing.Size(921, ((lineNumber + 1) * buttonHeight));
                this.Refresh();
            }
        }

        private void _hook_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (Visible)
            {
                if (vars.WhoIsPlaying == 0)
                {
                    (Controls["button1"] as Button).PerformClick();
                    vars.WhoIsPlaying = 1;
                }
                else
                {
                    (Controls["button" + vars.WhoIsPlaying.ToString()] as Button).PerformClick();
                }
            }
            else
            {
                if (vars.WhoIsPlaying == 0)
                {
                    vars.WhoIsPlaying = 1;
                }

                StartPlay(vars.WhoIsPlaying);
            }
        }

        public void StartPlay(int id)
        {
            if (id != 50)
            {
                if (vars.WhoIsPlaying != id)
                {
                    int bitrate = 128;
                    for (int i = 1; i <= 3; i++)
                    {
                        if ((Controls["radioButton" + i.ToString()] as RadioButton).Checked)
                        {
                            bitrate = Convert.ToInt32((Controls["radioButton" + i.ToString()] as RadioButton).Text);
                        }

                        (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = false;
                    }
                    wmPlayer.controls.stop();
                    wmPlayer.URL = vars.radio_url_bitrate(bitrate, id);
                    wmPlayer.controls.play();
                    vars.WhoIsPlaying = id;
                    Text = string.Format("{0} - {1} now playing", vars.aName, vars.StationList.Find((x) => x._id == id)._name);
                }
                else
                {
                    PausePlay(id);
                }
            }
        }

        private void PausePlay(int id)
        {
            if (wmPlayer.playState == WMPPlayState.wmppsPlaying)
            {
                wmPlayer.controls.pause();
                Text = string.Format("{0} - {1} is paused", vars.aName, vars.StationList.Find((x) => x._id == id)._name);
                for (int i = 1; i <= 3; i++)
                {
                    (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = false;
                }
            }
            else
            {
                wmPlayer.controls.play();
                Text = string.Format("{0} - {1} now playing", vars.aName, vars.StationList.Find((x) => x._id == id)._name);
                for (int i = 1; i <= 3; i++)
                {
                    (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = false;
                }
            }
        }

        private void StartPlayer(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            StartPlay(Convert.ToInt32(btn.TabIndex));
        }

        private void StopPlayer(object sender, EventArgs e)
        {
            wmPlayer.controls.stop();
            Text = string.Format("{0}", vars.aName);
            for (int i = 1; i <= 3; i++)
            {
                (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Panel panel1 = new Panel() { Dock = DockStyle.Fill, Name = "panel1", BackColor = System.Drawing.Color.Transparent };
            Controls.Add(panel1);
            Controls["panel1"].SuspendLayout();
            Controls["panel1"].ResumeLayout();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                new funks.AddNotifyUserDelegate(new funks().NotifyUser).BeginInvoke("Радио продолжит работать в трее.\n\rСтарт/Стоп проигрывания - Scroll Lock", null, null);
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Пользоваться программой очень просто!\r\n- Выбрать понравившуюся станцию\r\n- Нажать кнопку проигрывания у выбранной станции\r\n- PROFIT\r\n\r\nГорячие клавиши:\r\nENTER\t- запуск первой станции\r\nESC\t- остановить проигрывание\r\nScrollLock\t- остановить/воспроизвести выбранную станцию\r\n\r\nAutor: ZaprudnovAA", "микроHelp", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label_volume.Text = "Volume " + (trackBar1.Value * 10).ToString();
            wmPlayer.settings.volume = (trackBar1.Value * 10);
        }
    }
}
