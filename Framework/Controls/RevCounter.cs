using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BikeCommander.Framework.Controls
{
#pragma warning disable CS0108
    public partial class RevCounter : ProgressBar
    {
        #region Enums

        public enum ProgessShape
        {
            Round,
            Flat
        }

        public enum Textmode
        {
            None,
            Value,
            Percentage,
            Custom
        }

        #endregion

        #region Private Variables

        private long _Value;
        private long _Maximum = 100;
        private int _LineWitdh = 2;
        private float _BarWidth = 14f;

        private Color _ProgressColor1 = Color.Orange;
        private Color _ProgressColor2 = Color.Orange;
        private Color _LineColor = Color.Silver;
        private LinearGradientMode _GradientMode = LinearGradientMode.ForwardDiagonal;
        private ProgessShape ProgressShapeVal;
        private Textmode ProgressTextMode;

        #endregion

        #region Contructor

        public RevCounter()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = SystemColors.Control;
            this.ForeColor = Color.DimGray;

            this.Size = new Size(130, 130);
            this.Font = new Font("Segoe UI", 15);
            this.MinimumSize = new Size(100, 100);
            this.DoubleBuffered = true;

            this.LineWidth = 1;
            this.LineColor = Color.DimGray;

            Value = 57;
            ProgressShape = ProgessShape.Flat;
            TextMode = Textmode.Percentage;
        }

        #endregion

        #region Public Custom Properties

        public long Value
        {
            get { return _Value; }
            set
            {
                if (value > _Maximum)
                    value = _Maximum;
                _Value = value;
                Invalidate();
            }
        }

        public long Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value < 1)
                    value = 1;
                _Maximum = value;
                Invalidate();
            }
        }

        public Color BarColor1
        {
            get { return _ProgressColor1; }
            set
            {
                _ProgressColor1 = value;
                Invalidate();
            }
        }

        public Color BarColor2
        {
            get { return _ProgressColor2; }
            set
            {
                _ProgressColor2 = value;
                Invalidate();
            }
        }

        public float BarWidth
        {
            get { return _BarWidth; }
            set
            {
                _BarWidth = value;
                Invalidate();
            }
        }

        public LinearGradientMode GradientMode
        {
            get { return _GradientMode; }
            set
            {
                _GradientMode = value;
                Invalidate();
            }
        }

        public Color LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                Invalidate();
            }
        }

        public int LineWidth
        {
            get { return _LineWitdh; }
            set
            {
                _LineWitdh = value;
                Invalidate();
            }
        }

        public ProgessShape ProgressShape
        {
            get { return ProgressShapeVal; }
            set
            {
                ProgressShapeVal = value;
                Invalidate();
            }
        }

        public Textmode TextMode
        {
            get { return ProgressTextMode; }
            set
            {
                ProgressTextMode = value;
                Invalidate();
            }
        }

        public override string Text { get; set; }

        #endregion

        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetStandardSize();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetStandardSize();
        }

        protected override void OnPaintBackground(PaintEventArgs p)
        {
            base.OnPaintBackground(p);
        }

        #endregion

        #region Methods

        private void SetStandardSize()
        {
            int _Size = Math.Max(Width, Height);
            Size = new Size(_Size, _Size);
        }

        public void Increment(int Val)
        {
            this._Value += Val;
            Invalidate();
        }

        public void Decrement(int Val)
        {
            this._Value -= Val;
            Invalidate();
        }
        #endregion

        #region Events

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Bitmap bitmap = new Bitmap(this.Width, this.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    
                    PaintTransparentBackground(this, e);

                    using (Brush mBackColor = new SolidBrush(this.BackColor))
                    {
                        graphics.FillEllipse(mBackColor,
                                18, 18,
                                (this.Width - 0x30) + 12,
                                (this.Height - 0x30) + 12);
                    }

                    using (Pen pen2 = new Pen(LineColor, this.LineWidth))
                    {
                        graphics.DrawEllipse(pen2,
                            18, 18,
                          (this.Width - 0x30) + 12,
                          (this.Height - 0x30) + 12);
                    }

                    using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                        this._ProgressColor1, this._ProgressColor2, this.GradientMode))
                    {
                        using (Pen pen = new Pen(brush, this.BarWidth))
                        {
                            switch (this.ProgressShapeVal)
                            {
                                case ProgessShape.Round:
                                    pen.StartCap = LineCap.Round;
                                    pen.EndCap = LineCap.Round;
                                    break;

                                case ProgessShape.Flat:
                                    pen.StartCap = LineCap.Flat;
                                    pen.EndCap = LineCap.Flat;
                                    break;
                            }

                            graphics.DrawArc(pen,
                                0x12, 0x12,
                                (this.Width - 0x23) - 2,
                                (this.Height - 0x23) - 2,
                                -90,
                                (int)Math.Round((double)((360.0 / ((double)this._Maximum)) * this._Value)));
                        }
                    }

                    #region Dibuja el Texto de Progreso

                    switch (this.TextMode)
                    {
                        case Textmode.None:
                            this.Text = string.Empty;
                            break;

                        case Textmode.Value:
                            this.Text = _Value.ToString();
                            break;

                        case Textmode.Percentage:
                            this.Text = Convert.ToString(Convert.ToInt32((100 / _Maximum) * _Value));
                            break;

                        default:
                            break;
                    }

                    if (this.Text != string.Empty)
                    {
                        using (Brush FontColor = new SolidBrush(this.ForeColor))
                        {
                            int ShadowOffset = 2;
                            SizeF MS = graphics.MeasureString(this.Text, this.Font);
                            SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(100, this.ForeColor));

                            graphics.DrawString(this.Text, this.Font, shadowBrush,
                                Convert.ToInt32(Width / 2 - MS.Width / 2) + ShadowOffset,
                                Convert.ToInt32(Height / 2 - MS.Height / 2) + ShadowOffset
                            );

                            graphics.DrawString(this.Text, this.Font, FontColor,
                                Convert.ToInt32(Width / 2 - MS.Width / 2),
                                Convert.ToInt32(Height / 2 - MS.Height / 2));
                        }
                    }

                    #endregion

                    e.Graphics.DrawImage(bitmap, 0, 0);
                    graphics.Dispose();
                    bitmap.Dispose();
                }
            }
        }

        private static void PaintTransparentBackground(Control c, PaintEventArgs e)
        {
            if (c.Parent == null || !Application.RenderWithVisualStyles)
                return;

            ButtonRenderer.DrawParentBackground(e.Graphics, c.ClientRectangle, c);
        }
        
        #endregion

    }
#pragma warning restore CS0108
}
