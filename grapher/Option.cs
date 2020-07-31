﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grapher
{
    public class Option
    {
        public Option(Field field, Label label)
        {
            Field = field;
            Label = label;
        }

        public Field Field { get; }

        public Label Label { get; }

        public void SetName(string name)
        {
            Label.Text = name;
            Label.Left = Convert.ToInt32((Field.Box.Left / 2.0) - (Label.Width / 2.0));
        }

        public void Hide()
        {
            Field.Box.Hide();
            Label.Hide();
        }

        public void Show()
        {
            Field.Box.Show();
            Label.Show();
        }

        public void Show(string name)
        {
            SetName(name);

            Field.Box.Show();
            Label.Show();
        }
    }
}
