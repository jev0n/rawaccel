﻿using grapher.Layouts;
using grapher.Models.Options;
using grapher.Models.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grapher
{
    public class AccelTypeOptions
    {
        #region Fields

        public static readonly Dictionary<string, LayoutBase> AccelerationTypes = new List<LayoutBase>
        {
            new LinearLayout(),
            new ClassicLayout(),
            new NaturalLayout(),
            new PowerLayout(),
            new NaturalGainLayout(),
            new SigmoidGainLayout(),
            new OffLayout()
        }.ToDictionary(k => k.Name);

        #endregion Fields

        #region Constructors

        public AccelTypeOptions(
            ComboBox accelDropdown,
            Option acceleration,
            CapOptions cap,
            Option weight,
            OffsetOptions offset,
            Option limitOrExponent,
            Option midpoint,
            Button writeButton,
            ActiveValueLabel activeValueLabel)
        {
            AccelDropdown = accelDropdown;
            AccelDropdown.Items.Clear();
            AccelDropdown.Items.AddRange(AccelerationTypes.Keys.ToArray());
            AccelDropdown.SelectedIndexChanged += new System.EventHandler(OnIndexChanged);

            Acceleration = acceleration;
            Cap = cap;
            Weight = weight;
            Offset = offset;
            LimitOrExponent = limitOrExponent;
            Midpoint = midpoint;
            WriteButton = writeButton;
            ActiveValueLabel = activeValueLabel;

            Options = new List<OptionBase>
            {
                Acceleration,
                Cap,
                Offset,
                Weight,
                LimitOrExponent,
                Midpoint,
            };

            Layout("Off");
            ShowingDefault = true;
        }

        #endregion Constructors

        #region Properties

        public Button WriteButton { get; }

        public ComboBox AccelDropdown { get; }

        public int AccelerationIndex
        {
            get
            {
                return AccelerationType.Index;
            }
        }

        public LayoutBase AccelerationType { get; private set; }

        public ActiveValueLabel ActiveValueLabel { get; }

        public Option Acceleration { get; }

        public CapOptions Cap { get; }

        public Option Weight { get; }

        public OffsetOptions Offset { get; }

        public Option LimitOrExponent { get; }

        public Option Midpoint { get; }

        private IEnumerable<OptionBase> Options { get; }

        public int Top 
        {
            get
            {
                return AccelDropdown.Top;
            } 
            set
            {
                AccelDropdown.Top = value;
                Layout(value + AccelDropdown.Height + Constants.OptionVerticalSeperation);
            }
        }

        public int Height
        {
            get
            {
                return AccelDropdown.Height;
            } 
            set
            {
                AccelDropdown.Height = value;
            }
        }

        public int Left
        {
            get
            {
                return AccelDropdown.Left;
            } 
            set
            {
                AccelDropdown.Left = value;
            }
        }

        public int Width
        {
            get
            {
                return AccelDropdown.Width;
            }
            set
            {
                AccelDropdown.Width = value;
            }
        }

        private bool ShowingDefault { get; set; }

        #endregion Properties

        #region Methods

        public void Hide()
        {
            AccelDropdown.Hide();

            Acceleration.Hide();
            Cap.Hide();
            Weight.Hide();
            Offset.Hide();
            LimitOrExponent.Hide();
            Midpoint.Hide();
        }

        public void Show()
        {
            AccelDropdown.Show();
            Layout();
        }

        public void SetActiveValues(int index, AccelArgs args)
        {
            var name = AccelerationTypes.Where(t => t.Value.Index == index).FirstOrDefault().Value.Name;
            ActiveValueLabel.SetValue(name);

            Weight.SetActiveValue(args.weight);
            Cap.SetActiveValues(args.gainCap, args.scaleCap, args.gainCap > 0);
            Offset.SetActiveValue(args.offset, args.legacy_offset);
            Acceleration.SetActiveValue(args.accel);
            LimitOrExponent.SetActiveValue(args.exponent);
            Midpoint.SetActiveValue(args.midpoint);
        }

        public void ShowFull()
        {
            if (ShowingDefault)
            {
                AccelDropdown.Text = Constants.AccelDropDownDefaultFullText;
            }

            Left = Acceleration.Left;
            Width = Acceleration.Width;
        }

        public void ShowShortened()
        {
            if (ShowingDefault)
            {
                AccelDropdown.Text = Constants.AccelDropDownDefaultShortText;
            }

            Left = Acceleration.Field.Left;
            Width = Acceleration.Field.Width;
        }

        public void SetArgs(ref AccelArgs args)
        {
            args.accel = Acceleration.Field.Data;
            args.rate = Acceleration.Field.Data;
            args.powerScale = Acceleration.Field.Data;
            args.gainCap = Cap.VelocityGainCap;
            args.scaleCap = Cap.SensitivityCap;
            args.limit = LimitOrExponent.Field.Data;
            args.exponent = LimitOrExponent.Field.Data;
            args.powerExponent = LimitOrExponent.Field.Data;
            args.offset = Offset.Offset;
            args.legacy_offset = Offset.LegacyOffset;
            args.midpoint = Midpoint.Field.Data;
            args.weight = Weight.Field.Data;
        }

        public AccelArgs GenerateArgs()
        {
            AccelArgs args = new AccelArgs();
            SetArgs(ref args);
            return args;
        }

        private void OnIndexChanged(object sender, EventArgs e)
        {
            var accelerationTypeString = AccelDropdown.SelectedItem.ToString();
            Layout(accelerationTypeString);
            ShowingDefault = false;
        }

        private void Layout(string type)
        {
            AccelerationType = AccelerationTypes[type];
            Layout();
        }

        private void Layout(int top = -1)
        {
            if (top < 0)
            {
                top = Acceleration.Top;
            }

            AccelerationType.Layout(
                Acceleration,
                Cap,
                Weight,
                Offset,
                LimitOrExponent,
                Midpoint,
                WriteButton,
                top);
        }

        #endregion Methods
    }
}
