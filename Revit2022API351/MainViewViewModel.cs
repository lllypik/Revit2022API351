using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Prism.Commands;
using RevitAPILibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit2022API351
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;


        public DelegateCommand CalculateNumberОfPipes { get; private set; }
        public DelegateCommand CalculateVolumeОfWalls { get; private set; }
        public DelegateCommand CalculateNumberОfDoors { get; private set; }

        public List<Pipe> PipeList { get; private set; } = new List<Pipe>();
        public List<FamilyInstance> DoorsList { get; private set; } = new List<FamilyInstance>();
        public List<Element> WallElementsList { get; private set; } = new List<Element>();
        public double VolumeWallsValue { get; private set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            CalculateNumberОfPipes = new DelegateCommand(OnCalculateNumberОfPipes);
            CalculateVolumeОfWalls = new DelegateCommand(OnCalculateVolumeОfWalls);
            CalculateNumberОfDoors = new DelegateCommand(OnCalculateNumberОfDoors);
        }


        public event EventHandler HideRequest;
        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }


        public event EventHandler ShowRequest;
        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }


        private void OnCalculateNumberОfPipes()
        {
            RaiseHideRequest();

            PipeList = SelectionUtils.PickAllPipes(_commandData);

            TaskDialog.Show("Итог", $"Итого трубопроводов: {PipeList.Count.ToString()}");

            RaiseShowRequest();
        }


        private void OnCalculateNumberОfDoors()
        {
            RaiseHideRequest();

            DoorsList = SelectionUtils.PickAllDoors(_commandData);

            TaskDialog.Show("Итог", $"Итого дверей: {DoorsList.Count.ToString()}");

            RaiseShowRequest();
        }


        private void OnCalculateVolumeОfWalls()
        {
            RaiseHideRequest();

            WallElementsList = SelectionUtils.PickAllElementsWall(_commandData);

            VolumeWallsValue = CalculationUtils.CalculationVolumeWalls(WallElementsList);

            TaskDialog.Show("Итог", $"Итого объем стен: {VolumeWallsValue}");

            RaiseShowRequest();
        }


    }
}
