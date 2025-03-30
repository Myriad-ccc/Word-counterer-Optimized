using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WordCounterVisual
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;

            Dictionary<string, int> wordFrequency =
                File.ReadAllText(path).
                Split().
                Where(x => !string.IsNullOrEmpty(x) && x.All(char.IsLetter)).
                Select(x => x.ToLower()).
                GroupBy(word => word).
                ToDictionary(group => group.Key, group => group.Count());

            int defaultWidth = -100;
            int width = defaultWidth, height = 200, index = 1;

            foreach (var entry in wordFrequency.
                OrderByDescending(value => value.Value).
                Take(16))
            {
                width += 180;

                Label newLabel = new Label()
                {
                    Font = new Font("Sitka Small", 14f),
                    Text = $"{index}.\"{entry.Key}\": {entry.Value}",
                    Location = new Point(width, height),
                    AutoSize = true
                };

                if (ClientSize.Width - width <= 300)
                {
                    width = defaultWidth;
                    height += 65;
                }
                index++;

                Controls.Add(newLabel);
            }
        }
    }
}
