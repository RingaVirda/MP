using System;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;

namespace MaybeForms.ViewModel
{
    public class ChartsViewModel : ViewModelBase
    {
        private int _active = 0;
        private PlotModel _chartModel;
        private PlotModel _funcModel;
        private PlotModel _pieModel;

        public ChartsViewModel()
        {
            PageTitle = "Charts";

            Plot = new Command(OnPlot);
            Diagram = new Command(OnDiagram);
            _funcModel = CreateFuncModel();
            _pieModel = CreatePieModel();
            ChartModel = _funcModel;
        }

        public Command Plot { get; set; }
        public Command Diagram { get; set; }

        public PlotModel ChartModel
        {
            get => _chartModel;
            set => SetProperty(ref _chartModel, value);
        }

        public void OnPlot()
        {
            ChartModel = _funcModel;
        }

        public void OnDiagram()
        {
            ChartModel = _pieModel;
        }

        private PlotModel CreateFuncModel()
        {
            double Func(double x) => Math.Pow(Math.E, x);
            var model = new PlotModel();
            model.Series.Add(new FunctionSeries(Func, -6, 6, 0.01));
            model.Axes.Add(new LinearAxis {MaximumPadding = 0.1, MinimumPadding = 0.1});
            model.Axes.Add(new LinearAxis {MaximumPadding = 0.1, MinimumPadding = 0.1});
            return  model;
        }

        private PlotModel CreatePieModel()
        {
            var model = new PlotModel();
            var pieSeries = new PieSeries { InnerDiameter = 0.4, Diameter = 0.8};
            pieSeries.Slices.Add(new PieSlice("", 35) {Fill = OxyColors.Brown});
            pieSeries.Slices.Add(new PieSlice("", 40) {Fill = OxyColors.Yellow});
            pieSeries.Slices.Add(new PieSlice("", 25) {Fill = OxyColors.Red});
            model.Series.Add(pieSeries);
            return model;
        }
    }
}