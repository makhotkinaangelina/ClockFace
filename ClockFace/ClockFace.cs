//#define ANGLES_LABELS

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace ClockFace
{
    public partial class ClockFace : Form
    {
        private float hourLength;
        private Pen hourPen;
        private float minuteLength;
        private Pen minutePen;
        private float secondLength;
        private Pen secondPen;

        private Thread hourThread;
        private Thread minuteThread;
        private Thread secondThread;

        private int hourAngle = 0;
        private int minuteAngle = 90;
        private int secondAngle = 180;

        const int minHourSleepTime = 80;
        const int minMinuteSleepTime = 20;
        const int minSecondSleepTime = 110;

        private int hourSleepTime = minHourSleepTime;
        private int minuteSleepTime = minMinuteSleepTime;
        private int secondSleepTime = minSecondSleepTime;

        private bool hourDirection = true;
        private bool minuteDirection = false;
        private bool secondDirection = true;

        string collisionInfo = "";

        private object angleLock = new object();

#if ANGLES_LABELS
        Label hourLabel;
        Label minuteLabel;
        Label secondLabel;
#endif

        public ClockFace()
        {
            InitializeComponent();

            Font font = new Font("Arial", 12, FontStyle.Regular);
            hourLength = 50;
            hourPen = new Pen(Color.Purple, 2);
            minuteLength = 80;
            minutePen = new Pen(Color.Orange, 2);
            secondLength = 100;
            secondPen = new Pen(Color.Blue, 2);

#if ANGLES_LABELS
            hourLabel = new Label();
            hourLabel.Font = font;
            hourLabel.AutoSize = true;
            Controls.Add(hourLabel);

            minuteLabel = new Label();
            minuteLabel.Font = font;
            minuteLabel.AutoSize = true;
            Controls.Add(minuteLabel);

            secondLabel = new Label();
            secondLabel.Font = font;
            secondLabel.AutoSize = true;
            Controls.Add(secondLabel);
#endif
        }

        private void ClockFace_Load(object sender, EventArgs e)
        {
            InitializeClock();
        }

        private void InitializeClock()
        {
            hourThread = new Thread(UpdateHourAngle);
            minuteThread = new Thread(UpdateMinuteAngle);
            secondThread = new Thread(UpdateSecondAngle);

            hourThread.Start();
            minuteThread.Start();
            secondThread.Start();
        }

        private void UpdateHourAngle()
        {
            while (true)
            {
                lock (angleLock)
                {
                    if (hourDirection) hourAngle += 1;
                    else hourAngle -= 1;

                    hourAngle %= 360;

                    if (CheckCollision(hourAngle, secondAngle))
                    {
                        hourDirection = !hourDirection;

                        collisionInfo = "Столкновение: Часовая стрелка и Секундная стрелка\n" +
                                        "Угол часовой стрелки: " + hourAngle + "\n" +
                                        "Угол секундной стрелки: " + secondAngle;
                        //MessageBox.Show(collisionInfo);
                    }
                }

                if (this.IsHandleCreated) this.BeginInvoke(new Action(() => { Invalidate(); }));
                Thread.Sleep(hourSleepTime);
            }

        }


        private void UpdateMinuteAngle()
        {
            while (true)
            {
                lock (angleLock)
                {
                    if (minuteDirection) minuteAngle += 1;
                    else minuteAngle -= 1;

                    minuteAngle %= 360;

                    if (CheckCollision(minuteAngle, hourAngle))
                    {
                        hourDirection = !hourDirection;
                        minuteDirection = !minuteDirection;
                        collisionInfo = "Столкновение: Минутная стрелка и Часовая стрелка\n" +
                                        "Угол минутной стрелки: " + minuteAngle + "\n" +
                                        "Угол часовой стрелки: " + hourAngle;
                        //MessageBox.Show(collisionInfo);
                    }
                    else
                    {
                        if (CheckCollision(minuteAngle, secondAngle))
                        {
                            minuteDirection = !minuteDirection;
                            collisionInfo = "Столкновение: Минутная стрелка и Секундная стрелка\n" +
                                            "Угол минутной стрелки: " + minuteAngle + "\n" +
                                            "Угол секундной стрелки: " + secondAngle;
                            //MessageBox.Show(collisionInfo);
                        }
                    }

                }

                if (this.IsHandleCreated) this.BeginInvoke(new Action(() => { Invalidate(); }));
                Thread.Sleep(minuteSleepTime);
            }

        }

        private void UpdateSecondAngle()
        {
            while (true)
            {
                lock (angleLock)
                {
                    if (secondDirection) secondAngle += 1;
                    else secondAngle -= 1;

                    secondAngle %= 360;
                }

                if (this.IsHandleCreated) this.BeginInvoke(new Action(() => { Invalidate(); }));
                Thread.Sleep(secondSleepTime);
            }
        }


        private bool CheckCollision(int angle1, int angle2)
        {
            int angleDifference = Math.Abs(angle1 - angle2);
            int tolerance = 0;

            if (angleDifference <= tolerance || Math.Abs(angleDifference - 360) <= tolerance)
            {
                return true;
            }

            return false;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            lock (angleLock)
            {
                DrawClockHands(e.Graphics);

#if ANGLES_LABELS
                hourLabel.Location = new Point((int)(Width / 2 + hourLength * Math.Sin(hourAngle * Math.PI / 180)), (int)(Height / 2 - hourLength * Math.Cos(hourAngle * Math.PI / 180)));
                minuteLabel.Location = new Point((int)(Width / 2 + minuteLength * Math.Sin(minuteAngle * Math.PI / 180)), (int)(Height / 2 - minuteLength * Math.Cos(minuteAngle * Math.PI / 180)));
                secondLabel.Location = new Point((int)(Width / 2 + secondLength * Math.Sin(secondAngle * Math.PI / 180)), (int)(Height / 2 - secondLength * Math.Cos(secondAngle * Math.PI / 180)));

                hourLabel.Text = "Hour: " + hourAngle.ToString();
                minuteLabel.Text = "Minute: " + minuteAngle.ToString();
                secondLabel.Text = "Second: " + secondAngle.ToString();
#endif
            }
        }

        private void DrawClockHands(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float hourAngleInRadians = hourAngle * (float)Math.PI / 180;
            float hourX = (float)(Width / 2 + hourLength * Math.Sin(hourAngleInRadians));
            float hourY = (float)(Height / 2 - hourLength * Math.Cos(hourAngleInRadians));
            g.DrawLine(hourPen, Width / 2, Height / 2, hourX, hourY);

            float minuteAngleInRadians = minuteAngle * (float)Math.PI / 180;
            float minuteX = (float)(Width / 2 + minuteLength * Math.Sin(minuteAngleInRadians));
            float minuteY = (float)(Height / 2 - minuteLength * Math.Cos(minuteAngleInRadians));
            g.DrawLine(minutePen, Width / 2, Height / 2, minuteX, minuteY);

            float secondAngleInRadians = secondAngle * (float)Math.PI / 180;
            float secondX = (float)(Width / 2 + secondLength * Math.Sin(secondAngleInRadians));
            float secondY = (float)(Height / 2 - secondLength * Math.Cos(secondAngleInRadians));
            g.DrawLine(secondPen, Width / 2, Height / 2, secondX, secondY);

            // Отрисовка цифр на циферблате
            /*   
                   for (int i = 1; i <= 12; i++)
                   {
                       float angle = i * 30 * (float)Math.PI / 180;
                       float x = (float)(Width / 2 + (secondLength + 20) * Math.Sin(angle));
                       float y = (float)(Height / 2 - (secondLength + 20) * Math.Cos(angle));
                       g.DrawString(i.ToString(), font, Brushes.Black, x, y);
                   }*/

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hourThread != null)
            {
                hourThread.Abort();
                hourThread = null;
            }

            if (minuteThread != null)
            {
                minuteThread.Abort();
                minuteThread = null;
            }

            if (secondThread != null)
            {
                secondThread.Abort();
                secondThread = null;
            }
        }

        private void secondTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int selectedValue = (secondTrackBar.Value + 1);

            lock (angleLock)
            {
                secondSleepTime = minSecondSleepTime / selectedValue;
            }

        }

        private void minuteTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int selectedValue = (minuteTrackBar.Value + 1);

            lock (angleLock)
            {
                minuteSleepTime = minMinuteSleepTime / selectedValue;
            }

        }

        private void hourTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int selectedValue = (hourTrackBar.Value + 1);

            lock (angleLock)
            {
                hourSleepTime = minHourSleepTime / selectedValue;
            }

        }
    }

}