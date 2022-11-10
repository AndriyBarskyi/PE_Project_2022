using System.Drawing;
using System.Windows.Controls;
using ScottPlot;

namespace PamyatajkaUI.Views
{
    public partial class StatisticsView : UserControl
    {
        public StatisticsView()
        {
            InitializeComponent();
            var plt = WordsLearningBars.Plot;

// create sample data
            double[] valuesA = { 1, 2, 3, 2, 1, 2, 1 };
            double[] valuesB = { 3, 3, 2, 1, 3, 2, 1 };
            double[] valuesC = { 4, 1, 4, 2, 7, 1, 2 };
            double[] valuesD = { 7, 1, 2, 6, 2, 8, 5 };
            
            //string[] xs = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Нд" };
            double[] xs = { 1, 2, 3, 4, 5, 6, 7 };
            string[] legends = { "Нові", "Повторені", "Вивчені", "Відомі" };

// to simulate stacking B on A, shift B up by A
            double[] valuesB2 = new double[valuesB.Length];
            double[] valuesC2 = new double[valuesC.Length];
            double[] valuesD2 = new double[valuesC.Length];
            for (int i = 0; i < valuesB.Length; i++)
            {
                valuesB2[i] = valuesA[i] + valuesB[i];
                valuesC2[i] = valuesA[i] + valuesB[i] + valuesC[i];
                valuesD2[i] = valuesA[i] + valuesB[i] + valuesC[i] + valuesD[i];
            }
// plot the bar charts in reverse order (highest first)
            /*plt.AddBar(valuesD2, Color.Orange);
            plt.AddBar(valuesC2, Color.Yellow);
            plt.AddBar(valuesB2, Color.Green);
            plt.AddBar(valuesA, Color.Fuchsia);*/
            plt.PlotBar(xs, valuesD2, label: legends[0], fillColor: Color.Orange);
            plt.PlotBar(xs, valuesC2, label: legends[1], fillColor: Color.Yellow);
            plt.PlotBar(xs, valuesB2, label: legends[2], fillColor: Color.Green);
            plt.PlotBar(xs, valuesA, label: legends[3], fillColor: Color.Fuchsia);

// improve the styling
            plt.Legend(true, Alignment.UpperRight);

// adjust axis limits so there is no padding below the bar graph
            plt.SetAxisLimits(yMin: 0, yMax:16);
            plt.SetAxisLimits(xMin: 0.5, xMax:7.5);
            plt.XLabel("Час");
            plt.YLabel("Кількість слів");
            plt.Legend(true, Alignment.UpperRight);
            plt.Palette = Palette.Amber;
        }
    }
}