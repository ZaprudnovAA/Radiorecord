using System;
using System.Drawing;
using System.Windows.Forms;
using Radio.Properties;
using WMPLib;

namespace Radio
{
    public partial class Form1 : Form
    {
        private readonly WindowsMediaPlayer _wmPlayer = new WindowsMediaPlayer();
        private static readonly Hook Hook = new Hook(0x91);

        public Form1()
        {
            Vars.ListOfStations();
            CreateButtons();

            Hook.KeyPressed += _hook_KeyPressed;
            Hook.SetHook();

            InitializeComponent();
            Show();
            WindowState = FormWindowState.Normal;

            _wmPlayer.settings.volume = 80;
            KeyPreview = true;
            label_volume.Text = Resources.Form1_trackBar1_Scroll_Volume_ + _wmPlayer.settings.volume;
            trackBar1.Value = _wmPlayer.settings.volume / 10;

            Funks.PlayJingle();
        }

        private void CreateButtons()
        {
            const int buttonsInLine = 8;
            const int buttonWidth = 104;
            const int buttonHeight = 125;
            var lineBufer = 0;
            var elementNumber = 0;

            for (var id = 1; id < Vars.StationList.Count + 1; id++)
            {

                var lineNumber = (id-1) / buttonsInLine;

                int rowPoint;
                if (lineNumber == lineBufer)
                {
                    rowPoint = elementNumber * buttonWidth;
                    elementNumber++;
                }
                else
                {
                    lineBufer = lineNumber;
                    elementNumber = 0;
                    rowPoint = elementNumber * buttonWidth;
                    elementNumber++;
                }

                var button = new Button
                {
                    BackColor = Color.Transparent,
                    BackgroundImage = Vars.StationList.Find(x => x.Id == id).Image,
                    BackgroundImageLayout = ImageLayout.Center,
                    Location = new Point(rowPoint, lineNumber * buttonHeight),
                    Name = "button" + id,
                    Size = new Size(buttonWidth, buttonHeight),
                    TabIndex = id,
                    UseVisualStyleBackColor = false,
                    Visible = true
                };
                button.Click += StartPlayer;

                if (button != null)
                {
                    Controls.Add(button);
                }

                ClientSize = new Size(921, (lineNumber + 1) * buttonHeight);
            }
        }

        private void _hook_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (Visible)
            {
                if (Vars.WhoIsPlaying == 0)
                {
                    ((Button) Controls["button1"]).PerformClick();
                    Vars.WhoIsPlaying = 1;
                }
                else
                {
                    ((Button) Controls["button" + Vars.WhoIsPlaying]).PerformClick();
                }
            }
            else
            {
                if (Vars.WhoIsPlaying == 0)
                {
                    Vars.WhoIsPlaying = 1;
                }

                StartPlay(Vars.WhoIsPlaying);
            }
        }

        public void StartPlay(int id)
        {
            if (id == Vars.PlayPauseButtonId) return;
            if (Vars.WhoIsPlaying != id)
            {
                var bitrate = Vars.DefaultBitrate;
                for (var i = 1; i <= 3; i++)
                {
                    if (((RadioButton) Controls["radioButton" + i]).Checked)
                    {
                        bitrate = Convert.ToInt32(((RadioButton) Controls["radioButton" + i]).Text);
                    }

                    ((RadioButton) Controls["radioButton" + i]).Enabled = false;
                }
                _wmPlayer.controls.stop();
                _wmPlayer.URL = Vars.radio_url_bitrate(bitrate, id);
                _wmPlayer.controls.play();
                Vars.WhoIsPlaying = id;
                Text = string.Format("{0} - {1} now playing", Vars.AName, Vars.StationList.Find(x => x.Id == id).Name);
            }
            else
            {
                PausePlay(id);
            }
        }

        private void PausePlay(int id)
        {
            if (_wmPlayer.playState == WMPPlayState.wmppsPlaying)
            {
                _wmPlayer.controls.pause();
                Text = string.Format("{0} - {1} is paused", Vars.AName, Vars.StationList.Find(x => x.Id == id).Name);
                for (var i = 1; i <= 3; i++)
                {
                    ((RadioButton) Controls["radioButton" + i]).Enabled = false;
                }
            }
            else
            {
                _wmPlayer.controls.play();
                Text = string.Format("{0} - {1} now playing", Vars.AName, Vars.StationList.Find(x => x.Id == id).Name);
                for (var i = 1; i <= 3; i++)
                {
                    ((RadioButton) Controls["radioButton" + i]).Enabled = false;
                }
            }
        }

        private void StartPlayer(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            StartPlay(Convert.ToInt32(btn.TabIndex));
        }

        private void StopPlayer(object sender, EventArgs e)
        {
            _wmPlayer.controls.stop();
            Text = string.Format("{0}", Vars.AName);
            for (var i = 1; i <= 3; i++)
            {
                ((RadioButton) Controls["radioButton" + i]).Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var panel1 = new Panel { Dock = DockStyle.Fill, Name = "panel1", BackColor = Color.Transparent };
            Controls.Add(panel1);
            Controls["panel1"].SuspendLayout();
            Controls["panel1"].ResumeLayout();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            Hide();
            notifyIcon1.Visible = true;
            new Funks.AddNotifyUserDelegate(Funks.NotifyUser).BeginInvoke("Радио продолжит работать в трее.\n\rСтарт/Стоп проигрывания - Scroll Lock", null, null);
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
                MessageBox.Show(Resources.Form1_Form1_KeyDown_, Resources.Form1_Form1_KeyDown_микроHelp, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label_volume.Text = Resources.Form1_trackBar1_Scroll_Volume_ + trackBar1.Value * 10;
            _wmPlayer.settings.volume = trackBar1.Value * 10;
        }
    }
}
